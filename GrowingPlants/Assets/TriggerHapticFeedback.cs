using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerHapticFeedback : MonoBehaviour
{
	/*private HapticInteractable hapticInteractable;
	public Haptic hapticOnTrigger;  // 트리거 버튼 누를 때 발생할 햅틱 피드백

	void Start()
	{
		hapticInteractable = GetComponent<HapticInteractable>();
	}

	void Update()
	{
		if (hapticInteractable.activeInteractor != null)
		{
			var devices = new List<InputDevice>();
			InputDevices.GetDevices(devices);

			foreach (var device in devices)
			{
				if (device.isValid && device.TryGetFeatureValue(CommonUsages.triggerButton, out bool isTriggered) && isTriggered)
				{
					// ...
					break;
				}
			}
		}
	}*/
}
