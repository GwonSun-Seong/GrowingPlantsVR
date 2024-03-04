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
		isGrabbed = true; // 객체가 잡혔음을 표시
	}

	protected override void OnSelectExited(SelectExitEventArgs args)
	{
		base.OnSelectExited(args);
		isGrabbed = false; // 객체가 놓아졌음을 표시
	}
}
