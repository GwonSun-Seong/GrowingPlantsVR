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
			obj.position = this.transform.position; // 인벤토리 위치로 이동
			obj.rotation = Quaternion.Euler(0, 0, 0); // 회전 고정
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
				trackedObjects.Add(other.transform); // 추적 리스트에 추가

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
					trackedObjects.Remove(other.transform); // 추적 리스트에서 제거
					
					--objcount;
				}
			}
		}
	}
}
