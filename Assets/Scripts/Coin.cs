using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreValue = 1;
    public AudioClip coinCollectSound;
    private AudioSource audioSource;


    private void Start()
    {

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = coinCollectSound;
        audioSource.playOnAwake = false; // Don't play on awake
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.AddScore(scoreValue);
            AudioSource.PlayClipAtPoint(coinCollectSound, transform.position);
            Destroy(gameObject);

        }
    }

}
