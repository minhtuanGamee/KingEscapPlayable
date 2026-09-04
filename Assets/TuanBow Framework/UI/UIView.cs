using UnityEngine;
namespace TuanBowFramework.UI
{
	public abstract class UIView : MonoBehaviour
	{
		public string Id;
		public UIAnimation uiAnimation;
        [SerializeField] protected GameObject content;
        protected virtual void Start()
		{
			UIManager.Instance.Register(Id, this);

		}
		public virtual void Show()
		{
            content.gameObject.SetActive(true);
			if (uiAnimation != null)
			{
				uiAnimation.PlayShow();
			}
		}
		public virtual void Hide()
		{
			if (uiAnimation != null)
			{
				// Gọi PlayHide và truyền vào hành động SetActive(false)
				uiAnimation.PlayHide(() =>
				{
                    content.gameObject.SetActive(false);
				});
			}
			else
			{
                // Nếu không có animation thì ẩn luôn
                content.gameObject.SetActive(false);
			}
		}
	}
}