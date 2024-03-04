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
	private Coroutine rainCoroutine; // 비 재생 코루틴

	void Start()
	{
		grabInteractable = GetComponent<XRGrabInteractable>();
		lastPosition = transform.position;

		rainAudioSource = gameObject.AddComponent<AudioSource>();
		rainAudioSource.clip = rainSound;
		rainAudioSource.loop = true;

		// 흔들림 소리를 위한 AudioSource
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
				shakeAudioSource.Play(); // 흔들림 소리 재생
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
		rainAudioSource.Play(); // 비 소리 재생

		yield return new WaitForSeconds(30); // 파티클 지속 시간을 30초로 설정

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
