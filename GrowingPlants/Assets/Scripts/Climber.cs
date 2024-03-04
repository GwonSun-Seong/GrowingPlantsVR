using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Climber : MonoBehaviour
{
	private CharacterController characterController;
	private XROrigin xrOrigin; // XRRig ��� XROrigin ���

	public bool isGrabbing;
	public Transform hand;
	private Vector3 previousPosition;

	private void Start()
	{
		characterController = GetComponent<CharacterController>();
		xrOrigin = GetComponent<XROrigin>(); // XRRig ��� XROrigin�� ã��
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

	// �� �޼ҵ���� public���� ����
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
