using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorAct : MonoBehaviour
{
	private XRGrabInteractable grabInteractable; // XR Grab Interactable 컴포넌트
	private Vector3 lastPosition; // 마지막 위치
	private float shakeAmount; // 흔들림 양
	public float shakeThreshold = 1.5f; // 흔들림 감지 임계값
	public float activationThreshold = 5.0f; // 활성화 임계값

	public Transform doorTransform; // 문의 Transform 컴포넌트
	private bool isDoorRotating = false; // 문이 회전 중인지 여부
	private float targetAngle = 90.0f; // 목표 회전 각도
	private float rotateSpeed = 10.0f; // 초당 회전 속도

	void Start()
	{
		grabInteractable = GetComponent<XRGrabInteractable>();
		lastPosition = transform.position;
	}

	void Update()
	{
		if (grabInteractable.isSelected)
		{
			Vector3 velocity = (transform.position - lastPosition) / Time.deltaTime;
			shakeAmount += velocity.magnitude;
			lastPosition = transform.position;

			if (shakeAmount >= activationThreshold && !isDoorRotating)
			{
				isDoorRotating = true;
				StartCoroutine(RotateDoor());
				shakeAmount = 0f;
			}
		}
		else
		{
			shakeAmount = 0f;
			lastPosition = transform.position;
		}
	}

	private IEnumerator RotateDoor()
	{
		float currentAngle = doorTransform.eulerAngles.y;
		float endAngle = currentAngle + targetAngle;

		while (currentAngle < endAngle)
		{
			currentAngle += rotateSpeed * Time.deltaTime;
			doorTransform.rotation = Quaternion.Euler(0, currentAngle, 0);
			yield return null;
		}

		isDoorRotating = false;
	}
}
