using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private Patrol patrol;
    [SerializeField] private Idle idle;
    private bool isFrozen = false;
    public UnityEvent OnFreeze = new UnityEvent();
    private bool isTransitioning = false;

    void Start()
    {
        patrol.EndFreezeEnemy.AddListener(FinishFreeze);
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
            Destroy(other.gameObject);
            if (!isFrozen) {
                OnFreeze?.Invoke();
                isFrozen = true;
            }
        }

        if (other.gameObject.CompareTag("Heart"))
        {
            Destroy(other.gameObject);
            GameManager.instance.IncrementLives();
        }

        if (other.CompareTag("Trophy"))
        {
            Destroy(other.gameObject);

            // Check if SceneLoader exists before calling it
            GameManager.instance.AddScore(Mathf.FloorToInt(GameManager.instance.timer.GetTimeLeft()) / 10);
            if (SceneLoader.instance != null)
            {
                if (!isTransitioning)
                {
                    Debug.Log("Loading next scene...");
                    SceneLoader.instance.LoadNextScene();
                }
            }
            else
            {
                Debug.LogError("SceneLoader instance is missing!");
            }
        }
    }

    void FinishFreeze() {
        isFrozen = false;
    }
}
