using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySecondInventory : MonoBehaviour
{
	private List<Transform> trackedObjects = new List<Transform>();

    // Update is called once per frame
    void Update()
    {
		foreach (Transform obj in trackedObjects)
		{
			obj.position = this.transform.position; // �κ��丮 ��ġ�� �̵�
			obj.rotation = Quaternion.Euler(0, 0, 0); // ȸ�� ����
		}
	}
	private void OnTriggerEnter(Collider other)
	{

		if (trackedObjects.Count > 0)
		{
			return;
		}

		CustomTwoHandGrab twoHandedGrabbable = other.GetComponent<CustomTwoHandGrab>();

		if (twoHandedGrabbable != null && twoHandedGrabbable.isGrabbed)
		{
			Rigidbody rb = other.GetComponent<Rigidbody>();
			if (rb != null)
			{
				rb.isKinematic = true;
				rb.useGravity = false;
				trackedObjects.Add(other.transform); // ���� ����Ʈ�� �߰�
			}
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (trackedObjects.Contains(other.transform))
		{
			Rigidbody rb = other.GetComponent<Rigidbody>();
			if (rb != null)
			{
				CustomTwoHandGrab twoHandedGrabbable = other.GetComponent<CustomTwoHandGrab>();
				if (twoHandedGrabbable != null && twoHandedGrabbable.isGrabbed)
				{
					rb.useGravity = true;
					rb.isKinematic = false;
					trackedObjects.Remove(other.transform); // ���� ����Ʈ���� ����
				}
			}
		}
	}
}