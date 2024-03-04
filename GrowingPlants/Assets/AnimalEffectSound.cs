using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalEffectSound : MonoBehaviour
{
	public AudioClip[] animalSounds; // 동물 소리 클립 배열
	private AudioSource audioSource; // 오디오 소스 컴포넌트

	private float nextSoundTime = 0f; // 다음 소리 재생 시간

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		if (!audioSource)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.spatialBlend = 1.0f; // 3D 오디오 설정
			audioSource.maxDistance = 10f;
		}
		SetNextSoundTime(); // 초기 다음 소리 재생 시간 설정
	}

	void Update()
	{
		if (Time.time >= nextSoundTime && animalSounds.Length > 0)
		{
			PlayRandomAnimalSound();
			SetNextSoundTime(); // 다음 소리 재생 시간 업데이트
		}
	}

	void PlayRandomAnimalSound()
	{
		int randomIndex = Random.Range(0, animalSounds.Length);
		audioSource.PlayOneShot(animalSounds[randomIndex]);
	}

	void SetNextSoundTime()
	{
		nextSoundTime = Time.time + Random.Range(60f, 120f); // 30초에서 45초 사이 랜덤 시간 설정
	}
}
