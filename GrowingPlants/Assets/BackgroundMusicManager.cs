using UnityEngine;
using UnityEngine.Rendering;

public class BackgroundMusicManager : MonoBehaviour
{
	public AudioClip[] morningMusic; // 아침 음악
	public AudioClip[] noonMusic; // 점심 음악
	public AudioClip[] nightMusic; // 저녁 음악
	public AudioClip[] bgmClips; // 기본 BGM 클립 배열

	public AudioSource audioSource;
	public AudioSource defaultBGMSource; // 기본 BGM용 오디오 소스
	private SunMovement sunMovement;

	private SunMovement.TimeOfDay lastTimeOfDay;

	void Start()
	{
		audioSource = gameObject.AddComponent<AudioSource>();
		defaultBGMSource = gameObject.AddComponent<AudioSource>();

		sunMovement = GameObject.FindObjectOfType<SunMovement>(); // SunMovement 스크립트 찾기

		defaultBGMSource.loop = false;
		defaultBGMSource.playOnAwake = false;
		audioSource.volume = 0.2f; // 초기 볼륨 설정
		defaultBGMSource.volume = 0.2f; // 초기 볼륨 설정

		if (sunMovement != null)
		{
			lastTimeOfDay = sunMovement.currentTimeOfDay;
			PlayCurrentTimeOfDayMusic();
		}
		else
		{
			Debug.LogError("SunMovement not found!");
		}

		SetupDefaultBGM(); // 기본 BGM 설정
	}

	void Update()
	{
		if (sunMovement != null && lastTimeOfDay != sunMovement.currentTimeOfDay)
		{
			lastTimeOfDay = sunMovement.currentTimeOfDay;
			if (!defaultBGMSource.isPlaying)
			{
				PlayCurrentTimeOfDayMusic();
			}
		}

		// 현재 재생 중인 음악이 끝났을 경우, 새로운 음악을 랜덤으로 선택하고 재생
		if (!audioSource.isPlaying)
		{
			PlayCurrentTimeOfDayMusic();
		}

		// 기본 BGM 재생 로직
		if (!defaultBGMSource.isPlaying)
		{
			// 기본 BGM을 다음 랜덤 클립으로 변경
			if (bgmClips.Length > 0)
			{
				defaultBGMSource.loop = false; // 루프 비활성화
				defaultBGMSource.clip = bgmClips[Random.Range(0, bgmClips.Length)];
				defaultBGMSource.Play();
			}
			else
			{
				Debug.LogError("No BGM clips assigned!");
			}
		}

	}

	void PlayCurrentTimeOfDayMusic()
	{
		AudioClip[] timeOfDayClips = null;

		if (sunMovement != null)
		{
			switch (sunMovement.currentTimeOfDay)
			{
				case SunMovement.TimeOfDay.Morning:
					timeOfDayClips = morningMusic;
					break;
				case SunMovement.TimeOfDay.Noon:
					timeOfDayClips = noonMusic;
					break;
				case SunMovement.TimeOfDay.Night:
					timeOfDayClips = nightMusic;
					break;
			}
		}
		else
		{
			Debug.LogError("SunMovement not found!");
		}

		if (timeOfDayClips != null && timeOfDayClips.Length > 0)
		{
			audioSource.clip = timeOfDayClips[Random.Range(0, timeOfDayClips.Length)];
			audioSource.volume = 0.5f; // 볼륨을 0.5로 설정
			audioSource.Play();
		}
	}

	void SetupDefaultBGM()
	{
		if (bgmClips.Length > 0)
		{
			defaultBGMSource.clip = bgmClips[Random.Range(0, bgmClips.Length)];
			defaultBGMSource.loop = false;
			defaultBGMSource.volume = 0.2f; // 기본 BGM 볼륨 설정
			defaultBGMSource.Play();
		}
		else
		{
			Debug.LogError("No BGM clips assigned for default BGM!");
		}
	}
	public void SetMainVolume(float volume)
	{
		audioSource.volume = volume;
		defaultBGMSource.volume = volume;
	}
	public void PlayBgm(AudioClip clip)
	{
		Debug.Log("백그라운드 Selected BGM: " + clip.name);
		defaultBGMSource.Stop();

		defaultBGMSource.clip = clip;
		defaultBGMSource.Play();
	}
}
