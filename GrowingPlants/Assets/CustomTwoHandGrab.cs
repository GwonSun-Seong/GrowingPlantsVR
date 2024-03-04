using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomTwoHandGrab : XRGrabInteractable
{
	private List<XRBaseInteractor> interactors = new List<XRBaseInteractor>();
	public bool isGrabbed = false;

	protected override void OnSelectEntered(SelectEnterEventArgs args)
	{
		base.OnSelectEntered(args);
		interactors.Add(args.interactorObject as XRBaseInteractor);

		// ������Ʈ�� ��� ���ͷ��Ϳ� ���ؼ��� ������ ��
		isGrabbed = true;
	}

	protected override void OnSelectExited(SelectExitEventArgs args)
	{
		base.OnSelectExited(args);
		interactors.Remove(args.interactorObject as XRBaseInteractor);

		// ��� ���ͷ��Ͱ� ������Ʈ�� ������ ��
		if (interactors.Count == 0)
		{
			isGrabbed = false;
		}
	}
}
