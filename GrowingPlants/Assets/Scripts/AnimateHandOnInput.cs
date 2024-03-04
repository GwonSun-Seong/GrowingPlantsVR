using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchAniamationAction;
	public InputActionProperty gripAniamationAction;
	public Animator handAnimator;
	

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float triggerV = pinchAniamationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerV);

		float gripV = gripAniamationAction.action.ReadValue<float>();
		handAnimator.SetFloat("Grip", gripV);
	}
}
