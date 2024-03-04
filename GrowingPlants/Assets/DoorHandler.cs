using System.Collections;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
	public GameObject door; // 문 오브젝트
	public AudioClip doorSound; // 문 열릴 때의 사운드
	private AudioSource audioSource;
	public GameObject doorParent;
	public float rotationSpeed = 120f; // 문이 열리고 닫히는 속도
	public float openAngle = 90f; // 문이 열릴 최대 각도

	void Start()
	{
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = doorSound;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			StopAllCoroutines(); // 이미 실행 중인 코루틴을 멈춤
			StartCoroutine(RotateDoor(openAngle));
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			StopAllCoroutines(); // 이미 실행 중인 코루틴을 멈춤
			StartCoroutine(RotateDoor(0)); // 문을 닫음
		}
	}

	IEnumerator RotateDoor(float targetAngle)
	{
		audioSource.Play(); // 사운드 재생

		Quaternion startRotation = doorParent.transform.localRotation; // 현재 회전
		Quaternion endRotation = Quaternion.Euler(0, -179.998f, targetAngle); // Z축 회전, Y축 고정

		float elapsed = 0.0f;
		float duration = Mathf.Abs(targetAngle - startRotation.eulerAngles.z) / rotationSpeed;

		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			doorParent.transform.localRotation = Quaternion.Lerp(startRotation, endRotation, elapsed / duration);
			yield return null;
		}

		doorParent.transform.localRotation = endRotation;
	}
}
