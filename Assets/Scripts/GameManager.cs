using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent startTimer = new UnityEvent();
    public UnityEvent resetTimer = new UnityEvent();
    public UnityEvent pauseTimer = new UnityEvent();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startTimer?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            resetTimer?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseTimer?.Invoke();
        }
    }
}
