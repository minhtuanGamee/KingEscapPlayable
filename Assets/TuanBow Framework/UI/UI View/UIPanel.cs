using TuanBowFramework.UI;
using UnityEngine;
namespace TuanBowFramework.UI
{
    public class UIPanel : UIView
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
