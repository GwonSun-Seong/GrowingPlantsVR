using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorHandleInteraction : XRGrabInteractable
{
	private Transform doorTransform; // ���� Transform
	private Quaternion initialRotation; // �ʱ� ȸ��
	private float initialGrabAngle; // �׷� ���� ���� ����
	private Vector3 pivotOffset; // �ǹ� ������

	protected override void OnSelectEntered(SelectEnterEventArgs args)
	{
		base.OnSelectEntered(args);

		doorTransform = transform.parent; // �������� �θ� ������Ʈ�� ���̶�� ����
		initialRotation = doorTransform.rotation; // �ʱ� ȸ�� ����
		pivotOffset = doorTransform.position - transform.position; // �ǹ� ������ ���

		// �׷� ���� ���� ���� ���
		Vector3 grabPointInDoorSpace = doorTransform.InverseTransformPoint(args.interactorObject.transform.position);
		initialGrabAngle = Mathf.Atan2(grabPointInDoorSpace.x, grabPointInDoorSpace.z) * Mathf.Rad2Deg;
	}

	protected override void OnSelectExited(SelectExitEventArgs args)
	{
		base.OnSelectExited(args);
		doorTransform = null; // ���� ����
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

				// �� ȸ��
				Quaternion targetRotation = Quaternion.Euler(0, initialGrabAngle - currentGrabAngle, 0);
				doorTransform.rotation = initialRotation * targetRotation;

				// �ǹ� �������� ȸ���� ����
				doorTransform.position = transform.position + pivotOffset;
			}
		}
	}
}
