using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabPhysics : MonoBehaviour
{
    public InputActionProperty grabInputSource;
    public float radius = 0.1f;
    public LayerMask grabLayer;

	private FixedJoint fixedjoint;
	private bool isGrabbing = false;
	// Start is called before the first frame update
	private void FixedUpdate()
	{
		bool isGrabButtonPressed = grabInputSource.action.ReadValue<float>() > 0.1f;

		if (isGrabButtonPressed && !isGrabbing)
		{
			Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, radius, grabLayer, QueryTriggerInteraction.Ignore);

			if (nearbyColliders.Length > 0)
			{

				Rigidbody nearbyRigidbody = nearbyColliders[0].attachedRigidbody;
				fixedjoint = gameObject.AddComponent<FixedJoint>();
				fixedjoint.autoConfigureConnectedAnchor = false;

				if (nearbyRigidbody)
				{
					fixedjoint.connectedBody = nearbyRigidbody;
					fixedjoint.connectedAnchor = nearbyRigidbody.transform.InverseTransformPoint(transform.position);

				}
				else
				{
					fixedjoint.connectedAnchor = transform.position;
				}

				isGrabbing = true;

			}
		}
		else if (!isGrabButtonPressed && isGrabbing)
		{
			isGrabbing = false;

			if (fixedjoint)
			{
				Destroy(fixedjoint);
			}


		}

	}
}
