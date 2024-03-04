using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalEffectSound : MonoBehaviour
{
	public AudioClip[] animalSounds; // ���� �Ҹ� Ŭ�� �迭
	private AudioSource audioSource; // ����� �ҽ� ������Ʈ

	private float nextSoundTime = 0f; // ���� �Ҹ� ��� �ð�

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		if (!audioSource)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.spatialBlend = 1.0f; // 3D ����� ����
			audioSource.maxDistance = 10f;
		}
		SetNextSoundTime(); // �ʱ� ���� �Ҹ� ��� �ð� ����
	}

	void Update()
	{
		if (Time.time >= nextSoundTime && animalSounds.Length > 0)
		{
			PlayRandomAnimalSound();
			SetNextSoundTime(); // ���� �Ҹ� ��� �ð� ������Ʈ
		}
	}

	void PlayRandomAnimalSound()
	{
		int randomIndex = Random.Range(0, animalSounds.Length);
		audioSource.PlayOneShot(animalSounds[randomIndex]);
	}

	void SetNextSoundTime()
	{
		nextSoundTime = Time.time + Random.Range(60f, 120f); // 30�ʿ��� 45�� ���� ���� �ð� ����
	}
}
