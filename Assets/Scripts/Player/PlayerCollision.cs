using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollision : MonoBehaviour
{
    private Patrol patrol;
    private Idle idle;
    private bool isFrozen = false;
    public UnityEvent OnFreeze = new UnityEvent();

    void Start()
    {
        patrol = FindFirstObjectByType<Patrol>();
        patrol.EndFreezeEnemy.AddListener(FinishFreeze);
        idle = FindFirstObjectByType<Idle>();
        idle.EndFreezeObstacle.AddListener(FinishFreeze);
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle")) {
            if (!isFrozen) {
                GameManager.instance.LoseLife();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Snowflake")) {
            if (!isFrozen) {
                OnFreeze?.Invoke();
                isFrozen = true;
            }
        }
    }

    void FinishFreeze() {
        isFrozen = false;
    }
}
