using Unity.VisualScripting;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private bool isHit = true;
    public float hitcooldown = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isHit) return;
        if (collision.collider.CompareTag("Obstacle"))
        {
            GameManager.instance.LoseLife();
            UpdateLivesInHUD(); // TODO

            Debug.Log("Hit " + collision.collider.name); 
            StartCoroutine(HitCooldown());
        }
    }

    private System.Collections.IEnumerator HitCooldown()
    {
        isHit = false;
        yield return new WaitForSeconds(hitcooldown);
        isHit = true;
    }

    public void UpdateLivesInHUD()
    {
        // Placeholder for HUD update logic
        Debug.Log("Update HUD with new life count");
    }
}
