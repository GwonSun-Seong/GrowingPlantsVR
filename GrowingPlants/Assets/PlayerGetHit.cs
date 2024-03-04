using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetHit : MonoBehaviour
{

	public AudioClip[] hitSounds; // �ǰ��� Ŭ��
	private AudioSource audioSource; // ����� �ҽ� ������Ʈ
	public SimpleFadeScreen fadeScreen; // ȭ�� ���̵� ó�� ������Ʈ
	private WristStat wristStat; // HP�� �����ϴ� ��ũ��Ʈ
	public GameObject fadeScreenobj;

	// Start is called before the first frame update
	void Start()
    {
		audioSource = GetComponent<AudioSource>();
		if (audioSource == null)
		{
			Debug.LogError("AudioSource component not found on the XR Origin");
		}

		// WristStat ��ũ��Ʈ ã��
		wristStat = GameObject.Find("Wrist").GetComponent<WristStat>();
		if (wristStat == null)
		{
			Debug.LogError("WristStat component not found on the XR Origin");
		}
	}
	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.gameObject.CompareTag("Bullet"))
		{
			// HP ����
			wristStat.ChangeHp(-50);

			// ȭ���� ���������� ���̵�
			if (fadeScreen != null)
			{
				fadeScreenobj.SetActive(true);
				fadeScreen.FadeToColor(Color.red, 1f);
			}

			// �ǰ��� �� �ϳ��� �����ϰ� ���
			if (hitSounds.Length > 0 && audioSource != null)
			{
				AudioClip randomHitSound = hitSounds[Random.Range(0, hitSounds.Length)];
				audioSource.PlayOneShot(randomHitSound);
			}
		}
	}
	public void PlayRandomHitSound()
	{
		AudioClip randomHitSound = hitSounds[Random.Range(1, hitSounds.Length)];
		audioSource.PlayOneShot(randomHitSound);
		
	}
}
