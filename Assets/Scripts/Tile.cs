using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    bool isClicked = false;
    private Vector3 originalScale;

    [SerializeField] private float scaleUpDuration = 0.1f;
    [SerializeField] private float scaleDownDuration = 0.1f;
    [SerializeField] private float scaleUp = 1.5f;
    private void Awake()
    {
        originalScale = transform.localScale;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isClicked)
            return;
        OnClick();
    }
    public void OnClick()
    {
        isClicked = true;
        transform.DOKill();
        Sequence sequence = DOTween.Sequence();

        sequence.Append(
             transform.DOScale(originalScale * scaleUp, scaleUpDuration)
        );
        sequence.Append(
            transform.DOScale(originalScale * 0.9f, scaleDownDuration)
        );
        sequence.OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void OnSetUp()
    {
        transform.DOKill();
        Sequence sequence = DOTween.Sequence();

        sequence.Append(
             transform.DOScale(originalScale * scaleUp, scaleUpDuration)
        );
        sequence.Append(
            transform.DOScale(originalScale * 0.9f, scaleDownDuration)
        );
    }
}