using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawnManager : MonoBehaviour
{
	public Transform[] positions; // 스폰 위치 배열
	public GameObject[] prefabs;  // 동물 프리팹 배열
	private List<GameObject>[] pools; // 각 동물별 오브젝트 풀

	void Start()
	{
		InitializePools();
		StartCoroutine(SpawnRandomAnimal());
	}

	// 오브젝트 풀 초기화
	void InitializePools()
	{
		pools = new List<GameObject>[prefabs.Length];

		for (int i = 0; i < prefabs.Length; i++)
		{
			pools[i] = new List<GameObject>();

			// 각 동물 프리팹별로 하나의 복사본을 생성
			GameObject newAnimal = Instantiate(prefabs[i]);
			newAnimal.SetActive(false);
			pools[i].Add(newAnimal);
		}
	}

	// 랜덤 위치 반환
	Transform GetRandomPosition()
	{
		return positions[Random.Range(0, positions.Length)];
	}

	// 오브젝트 풀에서 동물 객체 가져오기
	GameObject GetAnimalFromPool(int prefabIndex)
	{
		// 사용 가능한 객체 찾기
		foreach (var animal in pools[prefabIndex])
		{
			if (!animal.activeInHierarchy)
			{
				return animal;
			}
		}

		// 사용 가능한 객체가 없으면 새로 생성
		GameObject newAnimal = Instantiate(prefabs[prefabIndex]);
		newAnimal.SetActive(false);
		pools[prefabIndex].Add(newAnimal);
		return newAnimal;
	}

	// 랜덤 동물 스폰
	private IEnumerator SpawnRandomAnimal()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(35f, 90f));

			if (positions.Length > 0 && prefabs.Length > 0)
			{
				GameObject animalToSpawn = ChooseRandomAnimalByProbability();

				if (animalToSpawn != null)
				{
					animalToSpawn.transform.position = GetRandomPosition().position;
					animalToSpawn.SetActive(true);
				}
			}
		}
	}

	// 확률에 따라 랜덤 동물 선택
	GameObject ChooseRandomAnimalByProbability()
	{
		float randomValue = Random.value; // 0과 1 사이의 랜덤 값
		float cumulativeProbability = 0.0f;

		// 멧돼지 (45% 확률)
		cumulativeProbability += 0.45f;
		if (randomValue <= cumulativeProbability)
		{
			return GetAnimalFromPool(0); // 멧돼지
		}

		// 호랑이 (25% 확률)
		cumulativeProbability += 0.25f;
		if (randomValue <= cumulativeProbability)
		{
			return GetAnimalFromPool(1); // 호랑이
		}

		// 늑대 (30% 확률)
		// 나머지 확률은 늑대로 간주
		return GetAnimalFromPool(2); // 늑대
	}
	public void ReturnObjectToPool(GameObject animal)
	{
		StartCoroutine(DeactivateAfterDelay(animal, 4.0f)); // 4초 딜레이 후 비활성화
	}

	private IEnumerator DeactivateAfterDelay(GameObject animal, float delay)
	{
		yield return new WaitForSeconds(delay);

		// 상태 초기화
		var hostileAnimal = animal.GetComponent<HostileTiger>(); // 또는 해당 동물의 스크립트 타입
		var hostileAnimal2 = animal.GetComponent<HostileBoar>();
		var hostileAnimal3 = animal.GetComponent<HostileWolf>();
		if (hostileAnimal != null)
		{
			hostileAnimal.ResetState();
		}
		if (hostileAnimal2 != null)
		{
			hostileAnimal2.ResetState();
		}
		if (hostileAnimal3 != null)
		{
			hostileAnimal3.ResetState();
		}

		animal.SetActive(false);
	}

}