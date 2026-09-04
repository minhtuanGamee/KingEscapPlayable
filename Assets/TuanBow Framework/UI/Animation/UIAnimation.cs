using DG.Tweening;
using System;
using UnityEngine;

namespace TuanBowFramework.UI
{
	public class UIAnimation : MonoBehaviour
	{
		[Header("Animation")]
		[SerializeField] private UIAnimationType showAnimation;
		[SerializeField] private UIAnimationType hideAnimation;

		[SerializeField] private float duration = 0.25f;
		[SerializeField] private float moveDistance = 300f;

		[Header("References")]
		[SerializeField] private CanvasGroup canvasGroup;

		private RectTransform rectTransform;
		private Vector2 originalPosition;

		private void Awake()
		{
			rectTransform = GetComponent<RectTransform>();

			if (canvasGroup == null)
				canvasGroup = GetComponent<CanvasGroup>();

			originalPosition = rectTransform.anchoredPosition;
		}
		private void PrepareShow()
		{
			if (showAnimation == UIAnimationType.Fade && canvasGroup != null) canvasGroup.alpha = 0;
			if (showAnimation == UIAnimationType.Scale) transform.localScale = Vector3.zero;
		}
		public void PlayShow()
		{
			KillTween();
			PrepareShow();
			switch (showAnimation)
			{
				case UIAnimationType.Fade: FadeIn(); break;
				case UIAnimationType.MoveLeft: MoveShow(Vector2.left); break;
				case UIAnimationType.MoveRight: MoveShow(Vector2.right); break;
				case UIAnimationType.MoveUp: MoveShow(Vector2.up); break;
				case UIAnimationType.MoveDown: MoveShow(Vector2.down); break;
				case UIAnimationType.Scale: ScaleIn(); break;

				default:
					break;
			}
		}

		public void PlayHide(Action onComplete)
		{
			KillTween();

			if (hideAnimation == UIAnimationType.None)
			{
				onComplete?.Invoke();
				return;
			}

			// Thực hiện animation và gọi callback khi xong
			Tween tween = null;
			switch (hideAnimation)
			{
				case UIAnimationType.Fade: tween = FadeOut(); break;
				case UIAnimationType.MoveLeft: tween = MoveHide(Vector2.left); break;
				case UIAnimationType.MoveRight: tween = MoveHide(Vector2.right); break;
				case UIAnimationType.MoveUp: tween = MoveHide(Vector2.up); break;
				case UIAnimationType.MoveDown: tween = MoveHide(Vector2.down); break;
				case UIAnimationType.Scale: tween = ScaleOut(); break;
			}

			if (tween != null)
				tween.OnComplete(() => onComplete?.Invoke());
			else
				onComplete?.Invoke();
		}

		private Tween FadeIn() => canvasGroup?.DOFade(1f, duration).SetEase(Ease.OutQuad);
		private Tween FadeOut() => canvasGroup?.DOFade(0f, duration).SetEase(Ease.InQuad);

		private Tween MoveShow(Vector2 direction)
		{
			rectTransform.anchoredPosition = originalPosition + direction * moveDistance;
			return rectTransform.DOAnchorPos(originalPosition, duration).SetEase(Ease.OutCubic);
		}

		private Tween MoveHide(Vector2 direction) =>
			rectTransform.DOAnchorPos(originalPosition + direction * moveDistance, duration).SetEase(Ease.InCubic);

		private Tween ScaleIn() => transform.DOScale(Vector3.one, duration).SetEase(Ease.OutBack);
		private Tween ScaleOut() => transform.DOScale(Vector3.zero, duration).SetEase(Ease.InBack);

		private void KillTween()
		{
			rectTransform.DOKill();
			transform.DOKill();

			if (canvasGroup != null)
				canvasGroup.DOKill();
		}
	}

	public enum UIAnimationType
	{
		None,
		Fade,
		MoveLeft,
		MoveRight,
		MoveUp,
		MoveDown,
		Scale
	}
}