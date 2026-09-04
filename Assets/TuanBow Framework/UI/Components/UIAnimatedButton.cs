using UnityEngine;
using UnityEngine.UI;

namespace TuanBowFramework.UI
{
	[RequireComponent(typeof(Button))]
	public class UIAnimatedButton : MonoBehaviour
	{
		[SerializeField] private UIAnimation anim;

		private Button button;

		private void Awake()
		{
			button = GetComponent<Button>();

			button.onClick.AddListener(OnClick);
		}

		private void OnDestroy()
		{
			button.onClick.RemoveListener(OnClick);
		}

		private void OnClick()
		{
			anim.PlayShow();
		}
	}
}