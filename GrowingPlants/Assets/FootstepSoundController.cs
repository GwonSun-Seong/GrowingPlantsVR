using UnityEngine;

public class FootstepSoundController : MonoBehaviour
{
	public AudioClip walkClip; // �Ϲ� ���� �Ҹ�
	public AudioClip rainWalkClip; // �� ���� �� ���� �Ҹ�
	public ParticleSystem rainParticleSystem; // �� ��ƼŬ �ý���

	private AudioSource audioSource;
	private Vector3 lastPosition;
	private bool isWalking;
	private float timeSinceLastStep;
	public float stepDistance = 0.5f; // �̵� �Ÿ� �Ӱ谪
	public float footstepDuration = 0.5f; // ���� �Ҹ� ���� �ð�

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		lastPosition = transform.position;
	}

	void Update()
	{
		// �÷��̾��� �̵� ���θ� Ȯ��
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
					// �� ���� ���� �⺻ footstepDuration ���
				}
				else
				{
					audioSource.clip = walkClip;
					audioSource.volume = 0.3f; // �Ϲ� ���� �Ҹ� �� footstepDuration�� 0.5�ʷ� ����
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
