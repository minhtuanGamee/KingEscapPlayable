using UnityEngine;

namespace TuanBowFramework.UI
{
    public class UIPopup : UIView
	{
		public virtual void Open()
		{
			Show();
		}

		public virtual void Close()
		{
			Hide();
		}
	}
}
