using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button2VR : MonoBehaviour
{
	public GameObject button;
	public UnityEvent onPress;
	public UnityEvent onRelease;
	public GameObject[] objectsToMove; // 위치를 변경할 객체 배열
	public GameObject referenceObject;

	GameObject presser;
	AudioSource sound;
	bool isPressed;

	void Start()
	{
		sound = GetComponent<AudioSource>();
		isPressed = false;

		// 새 이벤트 메소드 추가
		onPress.AddListener(MoveObjectsToReference);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!isPressed)
		{
			button.transform.localPosition = new Vector3(-0.442f, 0.003f, 0.534f);
			presser = other.gameObject;
			onPress.Invoke(); // 버튼 눌림 이벤트 호출
			sound.Play();
			isPressed = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject == presser)
		{
			button.transform.localPosition = new Vector3(-0.442f, 0.015f, 0.534f);
			onRelease.Invoke(); // 버튼 떼어짐 이벤트 호출
			isPressed = false;
		}
	}

	// 객체들의 위치를 변경하는 메소드
	public void MoveObjectsToReference()
	{
		Vector3 referencePosition = referenceObject.transform.position;

		foreach (GameObject obj in objectsToMove)
		{
			obj.transform.position = referencePosition;
		}
	}
}
