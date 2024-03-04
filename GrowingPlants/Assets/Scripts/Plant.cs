using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
	public GameObject fruitPrefab; // ���� ������
	public Transform spawnPoints; // ������ ��ġ
	public GameObject spawnedFruit; // �Ĺ� ��ü�� ������ ������ ����

	private void OnTriggerEnter(Collider other)
	{
		// �浹�� ��ü�� �±װ� "WaterCube"���� Ȯ��
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
			rb.isKinematic = true; // ó������ ����
		}

		// ������ ũ��, �絵, ���� ����
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

