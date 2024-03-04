using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SimpleXRButton : MonoBehaviour
{
	private XRSimpleInteractable interactable;
	private Vector3 originalPosition;
	private Vector3 pressedPosition;
	private float pressedDistance = 0.0261f; // 눌렸을 때 이동할 거리
	private float returnSpeed = 5.0f; // 복귀 속도

	void Start()
	{
		interactable = GetComponent<XRSimpleInteractable>();
		originalPosition = transform.localPosition;
		pressedPosition = new Vector3(originalPosition.x, originalPosition.y - pressedDistance, originalPosition.z);

		interactable.selectEntered.AddListener(HandleButtonPressed);
		interactable.selectExited.AddListener(HandleButtonReleased);
	}

	private void HandleButtonPressed(SelectEnterEventArgs args)
	{
		// 버튼을 눌렀을 때의 위치로 이동
		transform.localPosition = pressedPosition;

		GameObject[] bulletHoles = GameObject.FindGameObjectsWithTag("BulletHoleImage");
		foreach (GameObject bulletHole in bulletHoles)
		{
			Destroy(bulletHole); // 오브젝트 삭제
		}
	}

	private void HandleButtonReleased(SelectExitEventArgs args)
	{
		// 버튼을 뗐을 때 원래 위치로 복귀
		StartCoroutine(ReturnToOriginalPosition());
	}

	private IEnumerator ReturnToOriginalPosition()
	{
		while (Vector3.Distance(transform.localPosition, originalPosition) > 0.001f)
		{
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, originalPosition, returnSpeed * Time.deltaTime);
			yield return null;
		}
	}

	private void OnDestroy()
	{
		if (interactable)
		{
			interactable.selectEntered.RemoveListener(HandleButtonPressed);
			interactable.selectExited.RemoveListener(HandleButtonReleased);
		}
	}
}
