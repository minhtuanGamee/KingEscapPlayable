using System.Collections.Generic;
using TuanBowFramework.Core; // Giả sử đây là nơi chứa Singleton thường
using UnityEngine;

namespace TuanBowFramework.UI
{
	public class UIManager : Singleton<UIManager>
	{
		protected Dictionary<string, UIView> views = new();

		public virtual void Register(string id, UIView view)
		{
			if (!views.ContainsKey(id))
				views.Add(id, view);
		}

		public virtual void Unregister(string id)
		{
			if (views.ContainsKey(id))
				views.Remove(id);
		}

		public virtual void Show(string id)
		{
			if (views.TryGetValue(id, out UIView view))
			{
				view.Show();
			}
			else
			{
				Debug.LogWarning($"UI View ID '{id}' not found in current scene!");
			}
		}

		public virtual void Hide(string id)
		{
			if (views.TryGetValue(id, out UIView view))
			{
				view.Hide();
			}
		}
	}
}