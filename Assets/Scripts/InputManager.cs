using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour {
    public UnityEvent<float> OnMoveHorizontal = new UnityEvent<float>();
    public UnityEvent OnJump = new UnityEvent();
    public UnityEvent OnDownThrust = new UnityEvent();
    public UnityEvent OnPauseToggle = new UnityEvent(); 

    void Update() {
        // Get horizontal movement using Unity's input system
        float horizontalInput = Input.GetAxis("Horizontal");
        OnMoveHorizontal?.Invoke(horizontalInput);

        // Handle jump input
        if (Input.GetButtonDown("Jump")) {
            OnJump?.Invoke();
        }

        // Handle downward thrust input
        if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0) {
            OnDownThrust?.Invoke();
        }

        // Pause toggle
        if (Input.GetKeyDown(KeyCode.Escape)) {
            OnPauseToggle?.Invoke(); 
        }
    }
}