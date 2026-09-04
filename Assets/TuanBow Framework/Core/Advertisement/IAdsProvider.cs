using System;

namespace TuanBowFramework.Core
{
	public interface IAdsProvider
	{
		void Initialize(Action onInitComplete);

		bool IsInterstitialReady();
		void ShowInterstitial(Action onClosed = null);
		bool IsRewardedReady();

		void ShowRewarded(Action onReward, Action onFailure = null);
		void ShowBanner();
		void HideBanner();
	}
}