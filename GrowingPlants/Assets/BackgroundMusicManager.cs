using UnityEngine;
using UnityEngine.Rendering;

public class BackgroundMusicManager : MonoBehaviour
{
	public AudioClip[] morningMusic; // ��ħ ����
	public AudioClip[] noonMusic; // ���� ����
	public AudioClip[] nightMusic; // ���� ����
	public AudioClip[] bgmClips; // �⺻ BGM Ŭ�� �迭

	public AudioSource audioSource;
	public AudioSource defaultBGMSource; // �⺻ BGM�� ����� �ҽ�
	private SunMovement sunMovement;

	private SunMovement.TimeOfDay lastTimeOfDay;

	void Start()
	{
		audioSource = gameObject.AddComponent<AudioSource>();
		defaultBGMSource = gameObject.AddComponent<AudioSource>();

		sunMovement = GameObject.FindObjectOfType<SunMovement>(); // SunMovement ��ũ��Ʈ ã��

		defaultBGMSource.loop = false;
		defaultBGMSource.playOnAwake = false;
		audioSource.volume = 0.2f; // �ʱ� ���� ����
		defaultBGMSource.volume = 0.2f; // �ʱ� ���� ����

		if (sunMovement != null)
		{
			lastTimeOfDay = sunMovement.currentTimeOfDay;
			PlayCurrentTimeOfDayMusic();
		}
		else
		{
			Debug.LogError("SunMovement not found!");
		}

		SetupDefaultBGM(); // �⺻ BGM ����
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

		// ���� ��� ���� ������ ������ ���, ���ο� ������ �������� �����ϰ� ���
		if (!audioSource.isPlaying)
		{
			PlayCurrentTimeOfDayMusic();
		}

		// �⺻ BGM ��� ����
		if (!defaultBGMSource.isPlaying)
		{
			// �⺻ BGM�� ���� ���� Ŭ������ ����
			if (bgmClips.Length > 0)
			{
				defaultBGMSource.loop = false; // ���� ��Ȱ��ȭ
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
			audioSource.volume = 0.5f; // ������ 0.5�� ����
			audioSource.Play();
		}
	}

	void SetupDefaultBGM()
	{
		if (bgmClips.Length > 0)
		{
			defaultBGMSource.clip = bgmClips[Random.Range(0, bgmClips.Length)];
			defaultBGMSource.loop = false;
			defaultBGMSource.volume = 0.2f; // �⺻ BGM ���� ����
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
		Debug.Log("��׶��� Selected BGM: " + clip.name);
		defaultBGMSource.Stop();

		defaultBGMSource.clip = clip;
		defaultBGMSource.Play();
	}
}
