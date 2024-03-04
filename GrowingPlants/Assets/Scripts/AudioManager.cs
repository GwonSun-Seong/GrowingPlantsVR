using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance { get; private set; }
	public AudioSource audioSource; // 일반 사운드용 오디오 소스
	public AudioSource battleAudioSource; // 전투 상태 음악용 오디오 소스
	private int chasingEnemies = 0;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			audioSource = GetComponent<AudioSource>();
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void PlayEatingSound(AudioClip clip)
	{
		if (!audioSource.isPlaying)
			{
				audioSource.PlayOneShot(clip);
			}
	}

	public void StartChase()
	{
		chasingEnemies++;
		if (chasingEnemies >= 1 && battleAudioSource != null && !battleAudioSource.isPlaying)
		{
			battleAudioSource.Play();
		}
	}

	public void StopChase()
	{
		if (chasingEnemies > 0)
		{
			chasingEnemies--;
		}

		if (chasingEnemies == 0 && battleAudioSource != null)
		{
			battleAudioSource.Stop();
		}
	}

}
