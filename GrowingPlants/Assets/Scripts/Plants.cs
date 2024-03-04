using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : MonoBehaviour
{
	public GameObject plantPrefab; // 식물 프리팹
	public Transform[] spawnPoints; // 생성될 위치 배열
	private int currentIndex = 0; // 현재 열매 생성할 위치의 인덱스
	private float timer;

	private void OnTriggerEnter(Collider other)
	{
		int maxcount = Random.Range(1, 4);
		// 충돌한 물체의 태그가 "WaterCube"인지 확인
		if (other.CompareTag("WaterCube"))
		{

			// 최대 열매 개수에 도달하지 않았을 때만 열매 생성
			for (int i = 0; i < maxcount; i++)
			{
				Spawnplant();
				
			}
		}
	}

	private void Spawnplant()
	{
		// 정해진 위치에서 열매 생성
		if (currentIndex < spawnPoints.Length)
		{
			timer += Time.deltaTime;

			// timer가 0.75초를 초과하면 열매 생성 및 초기화
			if (timer > 0.75f)
			{
				Vector3 spawnPosition = spawnPoints[currentIndex].position;
				Quaternion spawnRotation = spawnPoints[currentIndex].rotation;
				GameObject newPlant = Instantiate(plantPrefab, spawnPosition, spawnRotation);

				Rigidbody rb = newPlant.GetComponent<Rigidbody>();
				if (rb != null)
				{
					rb.isKinematic = true; // 처음에는 고정
				}

				// 랜덤한 크기 적용
				float randomScaleFactor = Random.Range(0.5f, 2.0f);
				newPlant.transform.localScale *= randomScaleFactor;

				// 랜덤한 당도 설정
				Fruit fruitScript = newPlant.GetComponent<Fruit>();
				if (fruitScript != null)
				{
					fruitScript.size = newPlant.transform.localScale.x;
					fruitScript.weight = fruitScript.size * 1.5f;
					fruitScript.sweetness = (Fruit.SweetnessLevel)Random.Range(0, 3);
				}


				//Instantiate(plantPrefab, spawnPosition, spawnRotation);



				// 다음 인덱스로 이동
				currentIndex++;

				// 타이머 초기화
				timer = 0f;
			}
		}
	}
}
