using UnityEditor;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject brokenBoxPrefab;
    [SerializeField] private GameObject itemPrefab;

    private Transform transform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if in collision with player and not already hit and down thrust is enabled, initiate box breaking
        if (collision.gameObject.CompareTag("Player") && !animator.GetBool("isHit") && GameManager.instance.GetDownThrust())
        {
            // play animation
            animator.SetBool("isHit", true);
            // break the box
            Invoke(nameof(BreakBox), 0.4f);
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
        // Instantiate a new random object: a heart, a coin or a snowflake
        SpawnItem();
        
        // destroy the box
        Destroy(gameObject);
    }

    private void SpawnItem()
    {
        GameObject item = Instantiate(itemPrefab, transform.localPosition + new Vector3(1f,0f, 0f), Quaternion.identity);
        if (itemPrefab.CompareTag("Heart"))
        {
            item.transform.localScale = item.transform.localScale * 0.35f;
        }

        if (itemPrefab.CompareTag("Snowflake"))
        {
            item.transform.localScale = item.transform.localScale * 0.2f;
        }
        item.AddComponent<Rigidbody2D>();
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.angularVelocity = Random.Range(-200f, 200f);
    }
}
