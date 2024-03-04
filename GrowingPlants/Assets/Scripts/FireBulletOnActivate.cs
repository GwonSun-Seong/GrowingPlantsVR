using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class FireBulletOnActivate : MonoBehaviour
{
	public GameObject bullet;
	public GameObject hitSpritePrefab;
	public Transform spawnPoint;
	public float firespeed = 30;
	public AudioClip[] fireSounds; // 여러 오디오 클립을 위한 배열
	private AudioSource audioSource; // 오디오 소스 컴포넌트
	public TextMeshProUGUI scoreText;

	private bool shouldFire = false; // 발사할지 여부를 결정하는 플래그

	void Start()
	{
		XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
		grabbable.activated.AddListener(SetFireFlag);

		// AudioSource 컴포넌트를 추가하거나 가져옵니다.
		audioSource = GetComponent<AudioSource>();
		if (audioSource == null)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
		}
	}

	void FixedUpdate()
	{
		if (shouldFire)
		{
			FireBullet();
			shouldFire = false; // 발사 후 플래그를 재설정
		}
	}

	public void SetFireFlag(ActivateEventArgs args)
	{
		shouldFire = true; // 발사 플래그 설정
	}

	public void FireBullet()
	{
		// 여기서 Raycast를 사용하여 발사 경로를 예측할 수 있습니다.

		Vector3 fireDirection = spawnPoint.TransformDirection(Vector3.forward);
		GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, Quaternion.LookRotation(fireDirection));
		spawnedBullet.GetComponent<Rigidbody>().velocity = fireDirection * firespeed;

		Bullet bulletComponent = spawnedBullet.GetComponent<Bullet>();
		if (bulletComponent != null)
		{
			bulletComponent.SetScoreText(scoreText);
			bulletComponent.hitSpritePrefab = hitSpritePrefab;
		}

		Destroy(spawnedBullet, 5);

		// 랜덤 오디오 클립을 재생합니다.
		if (audioSource != null && fireSounds.Length > 0)
		{
			int randomIndex = Random.Range(0, fireSounds.Length);
			AudioClip randomSound = fireSounds[randomIndex];
			audioSource.PlayOneShot(randomSound);
		}
	}
}
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro; 


public class FireBulletOnActivate : MonoBehaviour
{
	public GameObject bullet;
	public GameObject hitSpritePrefab;
	public Transform spawnPoint;
	public float firespeed = 30;
	public AudioClip[] fireSounds; // 여러 오디오 클립을 위한 배열
	private AudioSource audioSource; // 오디오 소스 컴포넌트
	public TextMeshProUGUI scoreText;

	void Start()
	{
		XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
		grabbable.activated.AddListener(FireBullet);

		// AudioSource 컴포넌트를 추가하거나 가져옵니다.
		audioSource = GetComponent<AudioSource>();
		if (audioSource == null)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
		}
	}

	public void FireBullet(ActivateEventArgs args)
	{
		GameObject spawnedBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
		spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * firespeed;

		Bullet bulletComponent = spawnedBullet.GetComponent<Bullet>();
		if (bulletComponent != null)
		{
			bulletComponent.SetScoreText(scoreText);
			bulletComponent.hitSpritePrefab = hitSpritePrefab;
		}



		Destroy(spawnedBullet, 5);

		// 랜덤 오디오 클립을 재생합니다.
		if (audioSource != null && fireSounds.Length > 0)
		{
			int randomIndex = Random.Range(0, fireSounds.Length);
			AudioClip randomSound = fireSounds[randomIndex];
			audioSource.PlayOneShot(randomSound);
		}
	}
}
*/