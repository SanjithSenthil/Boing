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
        // Store the starting Y position for resetting later
        initialY = coinTextContainer.localPosition.y;
    }

    public void UpdateScore(int score)
    {
        toUpdate.SetText($"{score}");

        // Animate movement upwards with ease
        coinTextContainer.DOLocalMoveY(initialY + moveOffset, duration)
                         .SetEase(animationCurve);

        // Reset after animation
        StartCoroutine(ResetCoinContainer(score));
    }

    private IEnumerator ResetCoinContainer(int score)
    {
        yield return new WaitForSeconds(duration);

        // Update the actual displayed score
        current.SetText($"{score}");

        // Reset the container’s position
        Vector3 pos = coinTextContainer.localPosition;
        coinTextContainer.localPosition = new Vector3(pos.x, initialY, pos.z);
    }
}
