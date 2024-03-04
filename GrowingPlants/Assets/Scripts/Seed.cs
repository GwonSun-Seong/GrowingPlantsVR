using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
	public GameObject plantPrefab; // �Ĺ� ������
	public string seedType; // ������ Ÿ���� ��Ÿ���� ���ڿ�


	private void OnTriggerEnter(Collider other)
	{
	
		bool canGrow = false;

		// ��ٰ� ������ Dirt�� �ʿ� ����
		if ((seedType == "Carrot" || seedType == "Turnip") && other.CompareTag("Mud"))
		{
			canGrow = true;
		}
		// �ٸ� ���ŵ��� Dirt �������� �ڶ�
		else if (other.CompareTag("Dirt"))
		{
			canGrow = true;
		}

		if (canGrow)
		{
			InstantiatePlant();
			Destroy(gameObject); // ���� ����
		}
	}

	void InstantiatePlant()
	{
		Vector3 seedPosition = transform.position;
		seedPosition.y = 0.25f;
		GameObject plant = Instantiate(plantPrefab, seedPosition, Quaternion.identity);
	}
}
