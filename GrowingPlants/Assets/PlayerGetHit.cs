using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetHit : MonoBehaviour
{

	public AudioClip[] hitSounds; // 피격음 클립
	private AudioSource audioSource; // 오디오 소스 컴포넌트
	public SimpleFadeScreen fadeScreen; // 화면 페이드 처리 컴포넌트
	private WristStat wristStat; // HP를 관리하는 스크립트
	public GameObject fadeScreenobj;

	// Start is called before the first frame update
	void Start()
    {
		audioSource = GetComponent<AudioSource>();
		if (audioSource == null)
		{
			Debug.LogError("AudioSource component not found on the XR Origin");
		}

		// WristStat 스크립트 찾기
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
			// HP 감소
			wristStat.ChangeHp(-50);

			// 화면을 빨간색으로 페이드
			if (fadeScreen != null)
			{
				fadeScreenobj.SetActive(true);
				fadeScreen.FadeToColor(Color.red, 1f);
			}

			// 피격음 중 하나를 랜덤하게 재생
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
