using System;
using UnityEngine;

namespace TuanBowFramework.Core
{
	public class AdsManager : PersistentSingleton<AdsManager>
	{
		private IAdsProvider _provider;
		private bool _isInitialized;

		public void SetProvider(IAdsProvider provider)
		{
			_provider = provider;
		}

		protected override void Awake()
		{
			if (_provider == null) _provider = GetComponent<IAdsProvider>();
			base.Awake();
		}
		protected override void OnInit()
		{
			base.OnInit();
			Initialize();
		}
		public void Initialize()
		{
			if (_provider == null) return;
			Debug.Log("Đang Tạo");
			_provider.Initialize(() => {
				_isInitialized = true;
				Debug.Log("AdsManager: Initialized");
				ShowBanner();
			});
		}
		public bool IsInterstitialReady() => _isInitialized && _provider.IsInterstitialReady();
		public void ShowInterstitial(Action onClosed = null)
		{
			if (IsInterstitialReady())
				_provider.ShowInterstitial(onClosed);
			else
				onClosed?.Invoke(); 
		}
		public bool IsRewardedReady() => _isInitialized && _provider.IsRewardedReady();

		public void ShowRewarded(Action onReward, Action onFailure = null)
		{
			if (_provider != null && IsRewardedReady())
				_provider.ShowRewarded(onReward, onFailure);
			else
				onFailure?.Invoke();
		}
		public void ShowBanner() => _provider?.ShowBanner();
		public void HideBanner() => _provider?.HideBanner();

	}
}