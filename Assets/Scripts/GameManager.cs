using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI References")]
    [SerializeField] private CoinCounterUI coinCounter;
    [SerializeField] private GameObject[] lifeHearts;
    [SerializeField] GameObject explosion;

    [Header("Player Stats")]
    public int score = 0;
    public int lives = 3;
    private GameObject player;
    private CameraShake cameraShake;
    private bool isCooldown = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateHUD();
        player = GameObject.FindWithTag("Player");
        cameraShake = FindFirstObjectByType<CameraShake>();
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        UpdateScoreUI();
    }

    public void LoseLife()
    {
        if (isCooldown || lives <= 0) return;

        lives--;
        UpdateHearts();
        StartCoroutine(LoseLifeCooldown());
        cameraShake.StartShake();

        if (lives <= 0)
        {
            Time.timeScale = 1f; 
            GameData.finalScore = score;
            StartCoroutine(GameOverTransition());
            Destroy(player);
            GameObject effect = Instantiate(explosion, player.transform.position, Quaternion.identity);
            Destroy(effect, 1.5f);
        }

    }

    private void UpdateHUD()
    {
        UpdateScoreUI();
        UpdateHearts();
    }

    private void UpdateScoreUI()
    {
        if (coinCounter != null)
            coinCounter.UpdateScore(score);
    }

    private void UpdateHearts()
    {
        if (lifeHearts == null || lifeHearts.Length == 0) return;

        for (int i = 0; i < lifeHearts.Length; i++)
        {
            lifeHearts[i].SetActive(i < lives);
        }
    }

    private IEnumerator LoseLifeCooldown() {
        isCooldown = true;
        yield return new WaitForSeconds(1f);
        isCooldown = false;
    }

    private IEnumerator GameOverTransition() {
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
}
