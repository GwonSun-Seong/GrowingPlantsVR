using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSound : MonoBehaviour
{
	public AudioClip enterWaterSound; // 물에 들어갈 때의 소리
	public AudioClip exitWaterSound;  // 물에서 나갈 때의 소리
	public AudioClip ambientWaterSound; // 물의 지속적인 배경 소리
	public AudioClip insideWaterSound;

	private AudioSource audioSource;
	private bool isPlayerInside = false;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		if (audioSource == null)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
		}

		// 3D 사운드 설정
		audioSource.spatialBlend = 1.0f; // 완전한 3D 사운드로 설정
		audioSource.minDistance = 1f; // 최소 거리 설정
		audioSource.maxDistance = 30f; // 최대 거리 설정

		// 배경 소리 재생 설정
		audioSource.clip = ambientWaterSound;
		audioSource.playOnAwake = true;
		audioSource.loop = true;

		audioSource.Play();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			audioSource.spatialBlend = 0.0f;
			PlaySound(enterWaterSound);
			isPlayerInside = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			audioSource.spatialBlend = 0.0f;
			PlaySound(exitWaterSound);
			isPlayerInside = false;
			// 배경음을 다시 재생
			if (audioSource.clip != ambientWaterSound)
			{
				audioSource.spatialBlend = 1.0f; // 다시 3D 오디오로 변경
				audioSource.clip = ambientWaterSound;
				audioSource.loop = true;
				audioSource.Play();
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player") && isPlayerInside && !audioSource.isPlaying)
		{
			audioSource.clip = insideWaterSound;
			audioSource.loop = true; // 물 속에서의 소리를 루프로 재생
			audioSource.Play();
		}
	}

	void PlaySound(AudioClip clip)
	{
		if (clip != null)
		{
			audioSource.spatialBlend = 0.0f; // 완전한 2D 오디오로 설정
			audioSource.PlayOneShot(clip);
			// 배경음을 중지
			if (audioSource.clip == ambientWaterSound)
			{
				audioSource.Stop();
			}
		}
	}
}
