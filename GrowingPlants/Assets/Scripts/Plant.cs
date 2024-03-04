using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
	public GameObject fruitPrefab; // 열매 프리팹
	public Transform spawnPoints; // 생성될 위치
	public GameObject spawnedFruit; // 식물 객체의 참조를 저장할 변수

	private void OnTriggerEnter(Collider other)
	{
		// 충돌한 물체의 태그가 "WaterCube"인지 확인
		if (other.CompareTag("WaterCube"))
		{

			Spawnplant();

		}
	}
	private void Spawnplant()
	{

		if (spawnedFruit != null)
		{
			Destroy(spawnedFruit);
		}

		Vector3 spawnPosition = spawnPoints.position;
		spawnedFruit = Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);

		Rigidbody rb = spawnedFruit.GetComponent<Rigidbody>();
		if (rb != null)
		{
			rb.isKinematic = true; // 처음에는 고정
		}

		// 랜덤한 크기, 당도, 무게 설정
		float randomScaleFactor = Random.Range(0.5f, 2.0f);
		spawnedFruit.transform.localScale *= randomScaleFactor;

		Fruit fruitScript = spawnedFruit.GetComponent<Fruit>();
		if (fruitScript != null)
		{
			fruitScript.size = spawnedFruit.transform.localScale.x;
			fruitScript.weight = fruitScript.size * 1.5f;
			fruitScript.sweetness = (Fruit.SweetnessLevel)Random.Range(0, 3);
		}



	}
}

