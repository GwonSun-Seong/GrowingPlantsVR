using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomGrabbable : XRGrabInteractable
{
	public bool isGrabbed = false;

	protected override void OnSelectEntered(SelectEnterEventArgs args)
	{
		base.OnSelectEntered(args);
		isGrabbed = true; // ��ü�� �������� ǥ��
	}

	protected override void OnSelectExited(SelectExitEventArgs args)
	{
		base.OnSelectExited(args);
		isGrabbed = false; // ��ü�� ���������� ǥ��
	}
}
