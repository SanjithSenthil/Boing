using UnityEngine;

public class BoxController : MonoBehaviour
{
    private Animator animator;
    private int i = 1000;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        i--;
        if (i == 0)
        {
            animator.SetBool("isHit", true);
        }
        Debug.Log(i);
    }
}
