using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    public Animator transitionAnimator;
    private bool isTransitioning = false;

    private void Awake()
    {
        // if (instance == null)
        // {
        //     instance = this;
        //     DontDestroyOnLoad(gameObject);
        //     SceneManager.sceneLoaded += OnSceneLoaded; // Listen for scene changes

        // }
        // else
        // {
        //     Destroy(gameObject);
        // }
        instance = this;
    }
    void Start()
    {
        // Try to find the Animator in the current scene
        transitionAnimator = GameObject.Find("SceneTransitionPanel")?.GetComponent<Animator>();

        if (transitionAnimator == null)
        {
            Debug.LogWarning("No Animator found in the current scene! Scene transitions may not work.");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Try to find the new Animator in the current scene
        transitionAnimator = GameObject.Find("SceneTransitionPanel")?.GetComponent<Animator>();

        if (transitionAnimator == null)
        {
            Debug.LogWarning("No Animator found in the new scene! Scene transitions may not work.");
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key was pressed");
            LoadNextScene();
        }
    }
    
    /**
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    **/

    public void LoadNextScene()
    {
        if (!isTransitioning) // Ensure only one transition happens at a time
        {
            StartCoroutine(PlayTransition());
        }
    }

    IEnumerator PlayTransition()
    {
        isTransitioning = true;

        if (transitionAnimator == null)
        {
            Debug.LogError("SceneLoader: No transitionAnimator assigned!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            isTransitioning = false;
            yield break; // Exit coroutine early to avoid errors
        }

        Debug.Log("Starting scene transition animation...");
        transitionAnimator.SetTrigger("StartTransition"); // Start animation
        //Debug.Log("Finished scene transition animation...");

        /**yield return new WaitForSeconds(1f); // Wait for animation to complete

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        isTransitioning = false;
        */

        yield return new WaitForSeconds(0.45f);

        // Load the next scene asynchronously so it loads in the background while the transition animation is happening
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        asyncLoad.allowSceneActivation = false; // Prevent the scene from activating until the transition is complete

        // Wait for the scene to load
        while (!asyncLoad.isDone)
        {
            // Check if the scene is loaded but not activated yet
            if (asyncLoad.progress >= 0.9f)
            {
                // Allow the scene to activate after the transition animation completes
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }

        // Wait for the transition to complete (black panel should be off the screen now)
        yield return new WaitForSeconds(0.5f);

        // Reset the transition state
        isTransitioning = false;
    }
    public void LoadSceneByName(string sceneName)
    {
        if (!isTransitioning) // Ensure only one transition happens at a time
        {
            StartCoroutine(PlayTransition(sceneName));
        }
    }

    IEnumerator PlayTransition(string sceneName)
    {
        isTransitioning = true;

        if (transitionAnimator == null)
        {
            SceneManager.LoadScene(sceneName);  // Load scene immediately if no animator
            yield break; // Exit coroutine early to avoid errors
        }

        transitionAnimator.SetTrigger("StartTransition"); // Start animation
        //yield return new WaitForSeconds(1f); // Wait for animation to complete
        //SceneManager.LoadScene(sceneName); // Load the specified scene
        //isTransitioning = false;

        yield return new WaitForSeconds(1f);

        // Load the next scene asynchronously so it loads in the background while the transition animation is happening
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; // Prevent the scene from activating until the transition is complete

        // Wait for the scene to load
        while (!asyncLoad.isDone)
        {
            // Check if the scene is loaded but not activated yet
            if (asyncLoad.progress >= 0.9f)
            {
                // Allow the scene to activate after the transition animation completes
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }

        // Wait for the transition to complete (black panel should be off the screen now)
        yield return new WaitForSeconds(0.5f);

        // Reset the transition state
        isTransitioning = false;
    }

}
