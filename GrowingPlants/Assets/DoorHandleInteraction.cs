using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorHandleInteraction : XRGrabInteractable
{
	private Transform doorTransform; // 문의 Transform
	private Quaternion initialRotation; // 초기 회전
	private float initialGrabAngle; // 그랩 시작 시의 각도
	private Vector3 pivotOffset; // 피벗 오프셋

	protected override void OnSelectEntered(SelectEnterEventArgs args)
	{
		base.OnSelectEntered(args);

		doorTransform = transform.parent; // 손잡이의 부모 오브젝트가 문이라고 가정
		initialRotation = doorTransform.rotation; // 초기 회전 저장
		pivotOffset = doorTransform.position - transform.position; // 피벗 오프셋 계산

		// 그랩 시작 시의 각도 계산
		Vector3 grabPointInDoorSpace = doorTransform.InverseTransformPoint(args.interactorObject.transform.position);
		initialGrabAngle = Mathf.Atan2(grabPointInDoorSpace.x, grabPointInDoorSpace.z) * Mathf.Rad2Deg;
	}

	protected override void OnSelectExited(SelectExitEventArgs args)
	{
		base.OnSelectExited(args);
		doorTransform = null; // 참조 해제
	}

	public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
	{
		base.ProcessInteractable(updatePhase);

		if (isSelected && interactorsSelecting.Count > 0)
		{
			XRBaseInteractor interactor = interactorsSelecting[0] as XRBaseInteractor;
			if (interactor != null)
			{
				Vector3 grabPointInDoorSpace = doorTransform.InverseTransformPoint(interactor.transform.position);
				float currentGrabAngle = Mathf.Atan2(grabPointInDoorSpace.x, grabPointInDoorSpace.z) * Mathf.Rad2Deg;

				// 문 회전
				Quaternion targetRotation = Quaternion.Euler(0, initialGrabAngle - currentGrabAngle, 0);
				doorTransform.rotation = initialRotation * targetRotation;

				// 피벗 기준으로 회전을 조정
				doorTransform.position = transform.position + pivotOffset;
			}
		}
	}
}
