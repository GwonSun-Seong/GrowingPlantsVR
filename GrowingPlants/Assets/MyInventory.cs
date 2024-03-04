using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInventory : MonoBehaviour
{
	private List<Transform> trackedObjects = new List<Transform>();
	public int objcount;

	private void Update()
	{
		foreach (Transform obj in trackedObjects)
		{
			obj.position = this.transform.position; // �κ��丮 ��ġ�� �̵�
			obj.rotation = Quaternion.Euler(0, 0, 0); // ȸ�� ����
		}
	}

	private void OnTriggerEnter(Collider other)
	{

		CustomGrabbable grabbable = other.GetComponent<CustomGrabbable>();
		if (grabbable != null && grabbable.isGrabbed && (objcount == 0))
		{
			Rigidbody rb = other.GetComponent<Rigidbody>();
			if (rb != null)
			{
				rb.isKinematic = true;
				rb.useGravity = false;
				trackedObjects.Add(other.transform); // ���� ����Ʈ�� �߰�

				++objcount;
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
				CustomGrabbable grabbable = other.GetComponent<CustomGrabbable>();
				if (grabbable != null && grabbable.isGrabbed)
				{
					rb.useGravity = true;
					rb.isKinematic = false;
					trackedObjects.Remove(other.transform); // ���� ����Ʈ���� ����
					
					--objcount;
				}
			}
		}
	}
}
