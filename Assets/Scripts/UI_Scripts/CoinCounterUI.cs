using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;

public class CoinCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI current;               // Displayed score
    [SerializeField] private TextMeshProUGUI toUpdate;              // Animated score
    [SerializeField] private Transform coinTextContainer;           // The whole text container
    [SerializeField] private float duration = 0.3f;                 // Duration of animation
    [SerializeField] private float moveOffset = 30f;               // How much it moves upward
    [SerializeField] private Ease animationCurve = Ease.OutQuad;   // DOTween ease

    private float initialY;

    private void Start()
    {
        initialY = coinTextContainer.localPosition.y;
        UpdateDisplayScore();
    }

    public void UpdateScore(int score)
    {
        // Update GameData
        toUpdate.SetText($"{score}");
        coinTextContainer.DOLocalMoveY(initialY + moveOffset, duration)
                     .SetEase(animationCurve);

        StartCoroutine(ResetCoinContainer());
    }

    private IEnumerator ResetCoinContainer()
    {
        yield return new WaitForSeconds(duration);

        UpdateDisplayScore();
        coinTextContainer.localPosition = new Vector3(coinTextContainer.localPosition.x, initialY, coinTextContainer.localPosition.z);
    }

    private void UpdateDisplayScore()
    {
        current.SetText($"{GameData.Instance.levelScores[GameData.Instance.currentLevelIndex]}");
    }
}