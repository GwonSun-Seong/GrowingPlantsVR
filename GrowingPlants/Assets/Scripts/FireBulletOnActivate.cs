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
	public AudioClip[] fireSounds; // ���� ����� Ŭ���� ���� �迭
	private AudioSource audioSource; // ����� �ҽ� ������Ʈ
	public TextMeshProUGUI scoreText;

	private bool shouldFire = false; // �߻����� ���θ� �����ϴ� �÷���

	void Start()
	{
		XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
		grabbable.activated.AddListener(SetFireFlag);

		// AudioSource ������Ʈ�� �߰��ϰų� �����ɴϴ�.
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
			shouldFire = false; // �߻� �� �÷��׸� �缳��
		}
	}

	public void SetFireFlag(ActivateEventArgs args)
	{
		shouldFire = true; // �߻� �÷��� ����
	}

	public void FireBullet()
	{
		// ���⼭ Raycast�� ����Ͽ� �߻� ��θ� ������ �� �ֽ��ϴ�.

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

		// ���� ����� Ŭ���� ����մϴ�.
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
	public AudioClip[] fireSounds; // ���� ����� Ŭ���� ���� �迭
	private AudioSource audioSource; // ����� �ҽ� ������Ʈ
	public TextMeshProUGUI scoreText;

	void Start()
	{
		XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
		grabbable.activated.AddListener(FireBullet);

		// AudioSource ������Ʈ�� �߰��ϰų� �����ɴϴ�.
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

		// ���� ����� Ŭ���� ����մϴ�.
		if (audioSource != null && fireSounds.Length > 0)
		{
			int randomIndex = Random.Range(0, fireSounds.Length);
			AudioClip randomSound = fireSounds[randomIndex];
			audioSource.PlayOneShot(randomSound);
		}
	}
}
*/