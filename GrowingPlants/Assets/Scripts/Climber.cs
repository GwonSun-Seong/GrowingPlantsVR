using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Climber : MonoBehaviour
{
	private CharacterController characterController;
	private XROrigin xrOrigin; // XRRig 대신 XROrigin 사용

	public bool isGrabbing;
	public Transform hand;
	private Vector3 previousPosition;

	private void Start()
	{
		characterController = GetComponent<CharacterController>();
		xrOrigin = GetComponent<XROrigin>(); // XRRig 대신 XROrigin을 찾음
	}

	private void FixedUpdate()
	{
		if (isGrabbing && hand != null)
		{
			Vector3 movement = previousPosition - hand.position;
			characterController.Move(movement);
			previousPosition = hand.position;
		}
	}

	// 이 메소드들을 public으로 변경
	public void Grab(Transform handTransform)
	{
		isGrabbing = true;
		hand = handTransform;
		previousPosition = hand.position;
	}

	public void Release()
	{
		isGrabbing = false;
	}
}
