using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
	private ConfigurableJoint joint;
	private float originalPositionY;
	private float pressedPositionY = 0.06f;

	void Start()
	{
		joint = GetComponent<ConfigurableJoint>();
		originalPositionY = transform.localPosition.y;
	}

	void Update()
	{
		if (transform.localPosition.y < originalPositionY - pressedPositionY)
		{
			StartCoroutine(ResetPosition());
		}
	}

	private IEnumerator ResetPosition()
	{
		yield return new WaitForSeconds(1);
		var targetPosition = new Vector3(0, originalPositionY, 0);
		joint.targetPosition = targetPosition;
	}
}