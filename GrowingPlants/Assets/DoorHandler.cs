using System.Collections;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
	public GameObject door; // �� ������Ʈ
	public AudioClip doorSound; // �� ���� ���� ����
	private AudioSource audioSource;
	public GameObject doorParent;
	public float rotationSpeed = 120f; // ���� ������ ������ �ӵ�
	public float openAngle = 90f; // ���� ���� �ִ� ����

	void Start()
	{
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = doorSound;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			StopAllCoroutines(); // �̹� ���� ���� �ڷ�ƾ�� ����
			StartCoroutine(RotateDoor(openAngle));
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			StopAllCoroutines(); // �̹� ���� ���� �ڷ�ƾ�� ����
			StartCoroutine(RotateDoor(0)); // ���� ����
		}
	}

	IEnumerator RotateDoor(float targetAngle)
	{
		audioSource.Play(); // ���� ���

		Quaternion startRotation = doorParent.transform.localRotation; // ���� ȸ��
		Quaternion endRotation = Quaternion.Euler(0, -179.998f, targetAngle); // Z�� ȸ��, Y�� ����

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
