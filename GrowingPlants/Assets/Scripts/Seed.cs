using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
	public GameObject plantPrefab; // 식물 프리팹
	public string seedType; // 씨앗의 타입을 나타내는 문자열


	private void OnTriggerEnter(Collider other)
	{
	
		bool canGrow = false;

		// 당근과 순무는 Dirt가 필요 없음
		if ((seedType == "Carrot" || seedType == "Turnip") && other.CompareTag("Mud"))
		{
			canGrow = true;
		}
		// 다른 열매들은 Dirt 위에서만 자람
		else if (other.CompareTag("Dirt"))
		{
			canGrow = true;
		}

		if (canGrow)
		{
			InstantiatePlant();
			Destroy(gameObject); // 씨앗 제거
		}
	}

	void InstantiatePlant()
	{
		Vector3 seedPosition = transform.position;
		seedPosition.y = 0.25f;
		GameObject plant = Instantiate(plantPrefab, seedPosition, Quaternion.identity);
	}
}
