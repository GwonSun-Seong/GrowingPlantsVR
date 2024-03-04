using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorAct : MonoBehaviour
{
	private XRGrabInteractable grabInteractable; // XR Grab Interactable ������Ʈ
	private Vector3 lastPosition; // ������ ��ġ
	private float shakeAmount; // ��鸲 ��
	public float shakeThreshold = 1.5f; // ��鸲 ���� �Ӱ谪
	public float activationThreshold = 5.0f; // Ȱ��ȭ �Ӱ谪

	public Transform doorTransform; // ���� Transform ������Ʈ
	private bool isDoorRotating = false; // ���� ȸ�� ������ ����
	private float targetAngle = 90.0f; // ��ǥ ȸ�� ����
	private float rotateSpeed = 10.0f; // �ʴ� ȸ�� �ӵ�

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
