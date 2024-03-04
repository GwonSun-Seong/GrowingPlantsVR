using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticOnGrab : MonoBehaviour
{
	public Watering wateringCanScript; // ���Ѹ��� ��ũ��Ʈ ����
	public XRGrabInteractable grabInteractable; // ��ü�� �پ� �ִ� XRGrabInteractable ����
	public float strength = 0.2f; // ���� ����
	public float duration = 0.1f; // ���� ���� �ð�
	private Coroutine hapticCoroutine;

	void Start()
	{
		grabInteractable = GetComponent<XRGrabInteractable>();
		grabInteractable.selectEntered.AddListener(OnGrabbed);
		grabInteractable.selectExited.AddListener(OnReleased);
	}

	private void OnGrabbed(SelectEnterEventArgs args)
	{
		if (args.interactorObject is XRDirectInteractor)
		{
			hapticCoroutine = StartCoroutine(HapticFeedbackRoutine(args.interactorObject.transform.GetComponent<XRBaseController>()));
		}
	}

	private void OnReleased(SelectExitEventArgs args)
	{
		if (hapticCoroutine != null)
		{
			StopCoroutine(hapticCoroutine);
		}
	}

	private IEnumerator HapticFeedbackRoutine(XRBaseController controller)
	{
		while (true)
		{
			if (wateringCanScript != null)
			{
				float xAngle = wateringCanScript.transform.eulerAngles.x;
				if (xAngle >= 20.0f && xAngle <= 170.0f && controller != null)
				{
					controller.SendHapticImpulse(strength, duration);
				}
			}
			yield return new WaitForSeconds(0.14f);
		}
	}
}
