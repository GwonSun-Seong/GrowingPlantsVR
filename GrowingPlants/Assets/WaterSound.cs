using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSound : MonoBehaviour
{
	public AudioClip enterWaterSound; // ���� �� ���� �Ҹ�
	public AudioClip exitWaterSound;  // ������ ���� ���� �Ҹ�
	public AudioClip ambientWaterSound; // ���� �������� ��� �Ҹ�
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

		// 3D ���� ����
		audioSource.spatialBlend = 1.0f; // ������ 3D ����� ����
		audioSource.minDistance = 1f; // �ּ� �Ÿ� ����
		audioSource.maxDistance = 30f; // �ִ� �Ÿ� ����

		// ��� �Ҹ� ��� ����
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
			// ������� �ٽ� ���
			if (audioSource.clip != ambientWaterSound)
			{
				audioSource.spatialBlend = 1.0f; // �ٽ� 3D ������� ����
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
			audioSource.loop = true; // �� �ӿ����� �Ҹ��� ������ ���
			audioSource.Play();
		}
	}

	void PlaySound(AudioClip clip)
	{
		if (clip != null)
		{
			audioSource.spatialBlend = 0.0f; // ������ 2D ������� ����
			audioSource.PlayOneShot(clip);
			// ������� ����
			if (audioSource.clip == ambientWaterSound)
			{
				audioSource.Stop();
			}
		}
	}
}
