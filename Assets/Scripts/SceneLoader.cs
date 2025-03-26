using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key was pressed");
            LoadNextScene();
        }
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
