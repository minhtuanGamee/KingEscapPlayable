using System;
using System.Collections;
using System.Collections.Generic;
using TuanBowFramework.Core;
using UnityEngine;
using UnityEngine.Audio;

namespace TuanBowFramework.Core
{
	public class AudioManager : PersistentSingleton<AudioManager>
	{
		[Header("Audio Sources")]
		[SerializeField] private AudioSource musicSource;
		[SerializeField] private AudioSource sfxSource;

		[Header("Audio Mixer")]
		[SerializeField] private AudioMixer audioMixer;

		[Header("Sounds")]
		[SerializeField] private List<SoundData> sfx;
		[SerializeField] private List<SoundData> music;
		// Các hằng số chuẩn đặt tên Parameter trong AudioMixer
		private const string MASTER_VOLUME_PARAM = "MasterVolume";
		private const string MUSIC_VOLUME_PARAM = "MusicVolume";
		private const string SFX_VOLUME_PARAM = "SFXVolume";

		public bool IsMusicEnabled => musicVolume > 0f;
		public bool IsSFXEnabled => sfxVolume > 0f;

		private float musicVolume;
		private float sfxVolume;
		protected override void OnInit()
		{
			base.OnInit();

			musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_PARAM, 1f);
			sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_PARAM, 1f);
			float masterVolume = PlayerPrefs.GetFloat(MASTER_VOLUME_PARAM, 1f);

			StartCoroutine(InitMixerDelayed(masterVolume, musicVolume, sfxVolume));
		}

		private IEnumerator InitMixerDelayed(float master, float music, float sfx)
		{
			yield return new WaitForEndOfFrame();

			// Áp dụng vào mixer sau khi nó đã sẵn sàng
			SetMixerVolume(MASTER_VOLUME_PARAM, master);
			SetMixerVolume(MUSIC_VOLUME_PARAM, music);
			SetMixerVolume(SFX_VOLUME_PARAM, sfx);
		}
		#region Nhạc nền (Music)
		public void PlayMusic(string name, bool loop = true)
		{
			SoundData sound = music.Find(x => x.name == name);

			if (sound == null || sound.clip == null)
			{
				Debug.LogWarning($"Music not found: {name}");
				return;
			}

			PlayMusic(sound.clip, loop);
		}
		public void PlayMusic(AudioClip clip, bool loop = true)
		{
			if (clip == null || musicSource.clip == clip) return;

			musicSource.clip = clip;
			musicSource.loop = loop;
			musicSource.Play();
		}

		public void StopMusic() => musicSource.Stop();
		public void PauseMusic() => musicSource.Pause();
		public void UnPauseMusic() => musicSource.UnPause();
		#endregion

		#region Hiệu ứng âm thanh (SFX)
		public void PlaySFX(string name, float volume = 1f)
		{
			SoundData sound = sfx.Find(x => x.name == name);

			if (sound == null || sound.clip == null)
			{
				Debug.LogWarning($"SFX not found: {name}");
				return;
			}

			PlaySFX(sound.clip, volume);
		}
		public void PlaySFX(AudioClip clip, float volume = 1f)
		{
			if (clip == null) return;
			sfxSource.PlayOneShot(clip, volume);
		}
		public void PlaySFXAtPosition(AudioClip clip, Vector3 position, float volume = 1f)
		{
			if (clip == null) return;
			AudioSource.PlayClipAtPoint(clip, position, volume);
		}
		#endregion

		#region Quản lý Âm lượng (Volume Control)
		public void SetMasterVolume(float volume) => SetMixerVolume(MASTER_VOLUME_PARAM, volume);
		public void SetMusicVolume(float volume)
		{
			musicVolume = volume;
			Debug.Log("Music Volume " + musicVolume);
			PlayerPrefs.SetFloat(MUSIC_VOLUME_PARAM, volume);
			PlayerPrefs.Save();

			SetMixerVolume(MUSIC_VOLUME_PARAM, volume);
		}
		public void SetSFXVolume(float volume)
		{
			sfxVolume = volume;
			Debug.Log("sfxVolume " + sfxVolume);
			PlayerPrefs.SetFloat(SFX_VOLUME_PARAM, volume);
			PlayerPrefs.Save();

			SetMixerVolume(SFX_VOLUME_PARAM, volume);
		}

		private void SetMixerVolume(string parameterName, float volume)
		{
			if (audioMixer == null) return;

			float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
			audioMixer.SetFloat(parameterName, dB);
		}
		#endregion
	}
	[System.Serializable]
	public class SoundData
	{
		public string name;
		public AudioClip clip;
	}
}

