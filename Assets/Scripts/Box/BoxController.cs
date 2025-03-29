using UnityEditor;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject brokenBoxPrefab;
    [SerializeField] private GameManager gameManager;
    private Transform transform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !animator.GetBool("isHit"))
        {
            animator.SetBool("isHit", true);
            Invoke(nameof(BreakBox), 0.5f);
        }
    }

    private void BreakBox()
    {
        if (brokenBoxPrefab != null)
        {
            GameObject brokenBox = Instantiate(brokenBoxPrefab, transform.localPosition, Quaternion.identity);

            foreach (Transform piece in brokenBox.transform)
            {
                Rigidbody2D rb = piece.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = new Vector2(Random.Range(-.01f, 0.1f), Random.Range(2f, 5f));
                    rb.angularVelocity = Random.Range(-200f, 200f);
                }
            }
        }

        Destroy(gameObject);
    }
}
