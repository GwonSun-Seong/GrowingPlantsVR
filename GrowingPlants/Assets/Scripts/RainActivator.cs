using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RainActivator : MonoBehaviour
{
	public AudioClip rainSound;
	public AudioClip shakeSound;
	private AudioSource rainAudioSource;
	private AudioSource shakeAudioSource;
	public ParticleSystem rainParticleSystem;
	private XRGrabInteractable grabInteractable;
	private Vector3 lastPosition;
	private float shakeAmount;
	public float shakeThreshold = 2.5f;
	public float activationThreshold = 7.5f;
	private Coroutine rainCoroutine; // �� ��� �ڷ�ƾ

	void Start()
	{
		grabInteractable = GetComponent<XRGrabInteractable>();
		lastPosition = transform.position;

		rainAudioSource = gameObject.AddComponent<AudioSource>();
		rainAudioSource.clip = rainSound;
		rainAudioSource.loop = true;

		// ��鸲 �Ҹ��� ���� AudioSource
		shakeAudioSource = gameObject.AddComponent<AudioSource>();
		shakeAudioSource.clip = shakeSound;
		shakeAudioSource.loop = false;
	}

	void Update()
	{
		if (grabInteractable.isSelected)
		{
			Vector3 velocity = (transform.position - lastPosition) / Time.deltaTime;
			shakeAmount += velocity.magnitude;
			lastPosition = transform.position;

			if (shakeAmount >= shakeThreshold && !shakeAudioSource.isPlaying)
			{
				shakeAudioSource.Play(); // ��鸲 �Ҹ� ���
			}

			if (shakeAmount >= activationThreshold)
			{
				if (rainCoroutine == null)
				{
					rainCoroutine = StartCoroutine(PlayRainSoundAndParticle());
				}
				shakeAmount = 0f;
			}
		}
	}

	IEnumerator PlayRainSoundAndParticle()
	{
		rainParticleSystem.Play();
		rainAudioSource.Play(); // �� �Ҹ� ���

		yield return new WaitForSeconds(30); // ��ƼŬ ���� �ð��� 30�ʷ� ����

		StopRain();
	}


	void StopRain()
	{
		if (rainCoroutine != null)
		{
			StopCoroutine(rainCoroutine);
			rainCoroutine = null;
		}

		if (rainParticleSystem.isPlaying)
		{
			rainParticleSystem.Stop();
		}

		if (rainAudioSource.isPlaying)
		{
			rainAudioSource.Stop();
		}
	}
}
