using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;

public class CoinCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI current;
    [SerializeField] private TextMeshProUGUI toUpdate;
    [SerializeField] private Transform coinTextContainer;
    [SerializeField] private float duration;
    [SerializeField] private Ease AnimationCurve;

    private float containerInitPosition;
    private float moveAmount;
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