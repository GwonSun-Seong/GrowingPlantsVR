using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawnManager : MonoBehaviour
{
	public Transform[] positions; // ���� ��ġ �迭
	public GameObject[] prefabs;  // ���� ������ �迭
	private List<GameObject>[] pools; // �� ������ ������Ʈ Ǯ

	void Start()
	{
		InitializePools();
		StartCoroutine(SpawnRandomAnimal());
	}

	// ������Ʈ Ǯ �ʱ�ȭ
	void InitializePools()
	{
		pools = new List<GameObject>[prefabs.Length];

		for (int i = 0; i < prefabs.Length; i++)
		{
			pools[i] = new List<GameObject>();

			// �� ���� �����պ��� �ϳ��� ���纻�� ����
			GameObject newAnimal = Instantiate(prefabs[i]);
			newAnimal.SetActive(false);
			pools[i].Add(newAnimal);
		}
	}

	// ���� ��ġ ��ȯ
	Transform GetRandomPosition()
	{
		return positions[Random.Range(0, positions.Length)];
	}

	// ������Ʈ Ǯ���� ���� ��ü ��������
	GameObject GetAnimalFromPool(int prefabIndex)
	{
		// ��� ������ ��ü ã��
		foreach (var animal in pools[prefabIndex])
		{
			if (!animal.activeInHierarchy)
			{
				return animal;
			}
		}

		// ��� ������ ��ü�� ������ ���� ����
		GameObject newAnimal = Instantiate(prefabs[prefabIndex]);
		newAnimal.SetActive(false);
		pools[prefabIndex].Add(newAnimal);
		return newAnimal;
	}

	// ���� ���� ����
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

	// Ȯ���� ���� ���� ���� ����
	GameObject ChooseRandomAnimalByProbability()
	{
		float randomValue = Random.value; // 0�� 1 ������ ���� ��
		float cumulativeProbability = 0.0f;

		// ����� (45% Ȯ��)
		cumulativeProbability += 0.45f;
		if (randomValue <= cumulativeProbability)
		{
			return GetAnimalFromPool(0); // �����
		}

		// ȣ���� (25% Ȯ��)
		cumulativeProbability += 0.25f;
		if (randomValue <= cumulativeProbability)
		{
			return GetAnimalFromPool(1); // ȣ����
		}

		// ���� (30% Ȯ��)
		// ������ Ȯ���� ����� ����
		return GetAnimalFromPool(2); // ����
	}
	public void ReturnObjectToPool(GameObject animal)
	{
		StartCoroutine(DeactivateAfterDelay(animal, 4.0f)); // 4�� ������ �� ��Ȱ��ȭ
	}

	private IEnumerator DeactivateAfterDelay(GameObject animal, float delay)
	{
		yield return new WaitForSeconds(delay);

		// ���� �ʱ�ȭ
		var hostileAnimal = animal.GetComponent<HostileTiger>(); // �Ǵ� �ش� ������ ��ũ��Ʈ Ÿ��
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