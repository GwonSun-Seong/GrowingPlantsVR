using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class Haptic
{
    [Range(0, 1)]
    public float intensity;
    public float duration;
	public void TriggerHaptic(BaseInteractionEventArgs eventArgs)
	{
		if (eventArgs.interactorObject is XRBaseControllerInteractor controllerInteractor)
		{
			TriggerHaptic(controllerInteractor.xrController);
		}
	}

	// Update is called once per frame
	public void TriggerHaptic(XRBaseController controller)
	{
		if (intensity > 0)
		{
			controller.SendHapticImpulse(intensity, duration);
		}
	}

}

public class HapticInteractable : MonoBehaviour
{

	public XRBaseInteractor activeInteractor = null;

	public Haptic hapticOnActivated;
	public Haptic hapticHoverEntered;
	public Haptic hapticHoverExited;
	public Haptic hapticSelectEntered;
	public Haptic hapticSelectExited;


	// Start is called before the first frame update
	void Start()
    {
		XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
		interactable.hoverEntered.AddListener(hapticHoverEntered.TriggerHaptic);
		interactable.hoverExited.AddListener(hapticHoverExited.TriggerHaptic);
		interactable.selectEntered.AddListener(hapticSelectEntered.TriggerHaptic);
		interactable.selectExited.AddListener(hapticSelectExited.TriggerHaptic);

	}
	private void HandleSelectEntered(SelectEnterEventArgs args)
	{
		if (args.interactorObject is XRBaseControllerInteractor controllerInteractor)
		{
			activeInteractor = controllerInteractor;
			hapticSelectEntered?.TriggerHaptic(controllerInteractor.xrController);
		}
	}

	private void HandleSelectExited(SelectExitEventArgs args)
	{
		if (args.interactorObject is XRBaseControllerInteractor controllerInteractor)
		{
			if (activeInteractor == controllerInteractor)
			{
				activeInteractor = null;
			}
			hapticSelectExited?.TriggerHaptic(controllerInteractor.xrController);
		}
	}

	// 다른 이벤트 핸들러도 비슷한 방식으로 구현...

	public XRBaseController GetControllerFromInteractor(XRBaseInteractor interactor)
	{
		if (interactor is XRBaseControllerInteractor controllerInteractor)
		{
			return controllerInteractor.xrController;
		}
		return null;
	}
}
