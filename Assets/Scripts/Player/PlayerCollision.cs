using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private Patrol patrol;
    private Idle idle;
    private bool isFrozen = false;
    public UnityEvent OnFreeze = new UnityEvent();
    private bool isTransitioning = false;

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
            Debug.Log("Player touched the trophy! Loading next scene...");
            Destroy(other.gameObject);
            GameData.Instance.timeLeft[GameData.Instance.currentLevelIndex] += GameManager.instance.timer.GetTimeLeft();
            // Check if SceneLoader exists before calling it
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
