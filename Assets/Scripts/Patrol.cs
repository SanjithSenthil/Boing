using UnityEngine;

public class BatController : MonoBehaviour
{
    [SerializeField] private float leftPointX = 2f;
    [SerializeField] private float rightPointX = 2f;
    public float speed = 2f;
    private Vector3 leftPoint;
    private Vector3 rightPoint;
    private bool movingRight = false;

    void Start()
    {
        leftPoint = transform.position - Vector3.right * leftPointX;
        rightPoint = transform.position + Vector3.right * rightPointX;
    }

    void Update()
    {
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
}
