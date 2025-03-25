using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Idle : MonoBehaviour
{
    private PlayerCollision playerCollision;
    private Animator animator;
    public UnityEvent EndFreezeObstacle = new UnityEvent();

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
}
