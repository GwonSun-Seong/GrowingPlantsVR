using UnityEngine;

public class FootstepSoundController : MonoBehaviour
{
	public AudioClip walkClip; // 일반 걸음 소리
	public AudioClip rainWalkClip; // 비가 내릴 때 걸음 소리
	public ParticleSystem rainParticleSystem; // 비 파티클 시스템

	private AudioSource audioSource;
	private Vector3 lastPosition;
	private bool isWalking;
	private float timeSinceLastStep;
	public float stepDistance = 0.5f; // 이동 거리 임계값
	public float footstepDuration = 0.5f; // 걸음 소리 지속 시간

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		lastPosition = transform.position;
	}

	void Update()
	{
		// 플레이어의 이동 여부를 확인
		if (Vector3.Distance(transform.position, lastPosition) > stepDistance)
		{
			lastPosition = transform.position;
			isWalking = true;
			timeSinceLastStep = 0f;
		}

		if (isWalking)
		{
			timeSinceLastStep += Time.deltaTime;

			if (timeSinceLastStep > footstepDuration)
			{
				isWalking = false;
			}

			if (!audioSource.isPlaying)
			{
				if (rainParticleSystem != null && rainParticleSystem.isPlaying)
				{
					audioSource.clip = rainWalkClip;
					audioSource.volume = 1f;
					// 비가 내릴 때의 기본 footstepDuration 사용
				}
				else
				{
					audioSource.clip = walkClip;
					audioSource.volume = 0.3f; // 일반 걸음 소리 시 footstepDuration을 0.5초로 설정
				}
				audioSource.Play();
			}
		}
		else if (audioSource.isPlaying)
		{
			audioSource.Stop();
		}
	}
}
