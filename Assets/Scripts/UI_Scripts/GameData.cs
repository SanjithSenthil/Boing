using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    public int[] levelScores = new int[2]; // Level 1 and Level 2
    public float[] timeLeft = new float[2]; // Level 1 and Level 2 times 
    public int currentLevelIndex = 0;      // Track which level is being played

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int TotalScore()
    {
        return levelScores[0] * (int)timeLeft[0] + levelScores[1] + (int)timeLeft[1];
    }

    public void AddScore(int amount)
    {
        levelScores[currentLevelIndex] += amount;
    }
}
