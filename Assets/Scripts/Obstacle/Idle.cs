using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Idle : MonoBehaviour
{
    private PlayerCollision playerCollision;
    private Animator animator;
    public UnityEvent EndFreezeObstacle = new UnityEvent();
    [Header("Sound Effects")]
    public AudioClip hitSound; // Single sound per prefab
    public float soundVolume = 1.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerCollision = FindFirstObjectByType<PlayerCollision>();
        playerCollision.OnFreeze.AddListener(Freeze);
    }

    void Freeze()
    {
        StartCoroutine(FreezeForSeconds(5f));
    }

    public IEnumerator FreezeForSeconds(float seconds)
    {
        animator.enabled = false;
        yield return new WaitForSeconds(seconds);
        animator.enabled = true;
        EndFreezeObstacle?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && hitSound != null)
        {
            Debug.Log("Player hit the obstacle. Playing sound...");
            AudioSource.PlayClipAtPoint(hitSound, transform.position, soundVolume);
        }
    }
}
