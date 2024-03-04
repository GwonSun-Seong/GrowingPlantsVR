using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button2VR : MonoBehaviour
{
	public GameObject button;
	public UnityEvent onPress;
	public UnityEvent onRelease;
	public GameObject[] objectsToMove; // ��ġ�� ������ ��ü �迭
	public GameObject referenceObject;

	GameObject presser;
	AudioSource sound;
	bool isPressed;

	void Start()
	{
		sound = GetComponent<AudioSource>();
		isPressed = false;

		// �� �̺�Ʈ �޼ҵ� �߰�
		onPress.AddListener(MoveObjectsToReference);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!isPressed)
		{
			button.transform.localPosition = new Vector3(-0.442f, 0.003f, 0.534f);
			presser = other.gameObject;
			onPress.Invoke(); // ��ư ���� �̺�Ʈ ȣ��
			sound.Play();
			isPressed = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject == presser)
		{
			button.transform.localPosition = new Vector3(-0.442f, 0.015f, 0.534f);
			onRelease.Invoke(); // ��ư ������ �̺�Ʈ ȣ��
			isPressed = false;
		}
	}

	// ��ü���� ��ġ�� �����ϴ� �޼ҵ�
	public void MoveObjectsToReference()
	{
		Vector3 referencePosition = referenceObject.transform.position;

		foreach (GameObject obj in objectsToMove)
		{
			obj.transform.position = referencePosition;
		}
	}
}
