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
        if (collision.gameObject.CompareTag("Player") && !animator.GetBool("isHit") && GameManager.instance.GetDownThrust())
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
                    rb.linearVelocity = new Vector2(Random.Range(-2f, 2f), Random.Range(2f, 5f));
                    rb.angularVelocity = Random.Range(-200f, 200f);
                }
            }
        }

        SpawnItem();

        Destroy(gameObject);
    }

    private void SpawnItem()
    {
        if (itemPrefab != null)
        {
            GameObject item = Instantiate(itemPrefab, transform.localPosition + new Vector3(1f, 0, 0), Quaternion.identity);
            if (itemPrefab.CompareTag("Heart"))
            {
                item.transform.localScale = item.transform.localScale * 0.3f;
            }

            if (itemPrefab.CompareTag("Snowflake"))
            {
                item.transform.localScale = item.transform.localScale * 0.25f;
            }
            item.AddComponent<Rigidbody2D>();
            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.angularVelocity = Random.Range(-200f, 200f);
        }
    }
}