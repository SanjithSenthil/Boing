using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;
    private Animator animator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        animator = GetComponent<Animator>();
    }

    public void LoadNextScene()
    {
        StartCoroutine(PlayTransition());
    }

    private IEnumerator PlayTransition()
    {
        animator.SetTrigger("StartTransition"); // Play the closing animation
        yield return new WaitForSeconds(1f); // Wait for animation to complete

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        animator.SetTrigger("EndTransition"); // Play the opening animation
    }
}
