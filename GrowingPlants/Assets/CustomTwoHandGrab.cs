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

		// 오브젝트가 어떠한 인터랙터에 의해서든 잡혔을 때
		isGrabbed = true;
	}

	protected override void OnSelectExited(SelectExitEventArgs args)
	{
		base.OnSelectExited(args);
		interactors.Remove(args.interactorObject as XRBaseInteractor);

		// 모든 인터랙터가 오브젝트를 놓았을 때
		if (interactors.Count == 0)
		{
			isGrabbed = false;
		}
	}
}
