using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Patrol : MonoBehaviour
{
    [SerializeField] private float leftPointX = 2f;
    [SerializeField] private float rightPointX = 2f;
    public float speed = 2f;
    private Vector3 leftPoint;
    private Vector3 rightPoint;
    private bool movingRight = false;
    private bool isFrozen = false;
    private Animator animator;
    private PlayerCollision playerCollision;
    public UnityEvent EndFreezeEnemy = new UnityEvent();
    [Header("Sound Effects")]
    public AudioClip hitSound; // Single sound per prefab
    public float soundVolume = 1.0f;

    void Start()
    {
        leftPoint = transform.position - Vector3.right * leftPointX;
        rightPoint = transform.position + Vector3.right * rightPointX;
        animator = GetComponent<Animator>();
        playerCollision = FindFirstObjectByType<PlayerCollision>();
        playerCollision.OnFreeze.AddListener(Freeze);
    }

    void Update()
    {
        if (isFrozen) return;

        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= rightPoint.x) {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x <= leftPoint.x) {
                movingRight = true;
                Flip();
            }
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Freeze()
    {
        StartCoroutine(FreezeForSeconds(5f));
    }

    public IEnumerator FreezeForSeconds(float seconds)
    {
        isFrozen = true;
        animator.enabled = false;
        yield return new WaitForSeconds(seconds);
        isFrozen = false;
        animator.enabled = true;
        EndFreezeEnemy?.Invoke();
    }
    // sound effect when the player collides with the enemy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && hitSound != null)
        {
            Debug.Log("Player hit the enemy. Playing sound...");
            AudioSource.PlayClipAtPoint(hitSound, transform.position, soundVolume);
        }
    }
}
