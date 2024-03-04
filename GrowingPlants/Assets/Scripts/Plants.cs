using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : MonoBehaviour
{
	public GameObject plantPrefab; // �Ĺ� ������
	public Transform[] spawnPoints; // ������ ��ġ �迭
	private int currentIndex = 0; // ���� ���� ������ ��ġ�� �ε���
	private float timer;

	private void OnTriggerEnter(Collider other)
	{
		int maxcount = Random.Range(1, 4);
		// �浹�� ��ü�� �±װ� "WaterCube"���� Ȯ��
		if (other.CompareTag("WaterCube"))
		{

			// �ִ� ���� ������ �������� �ʾ��� ���� ���� ����
			for (int i = 0; i < maxcount; i++)
			{
				Spawnplant();
				
			}
		}
	}

	private void Spawnplant()
	{
		// ������ ��ġ���� ���� ����
		if (currentIndex < spawnPoints.Length)
		{
			timer += Time.deltaTime;

			// timer�� 0.75�ʸ� �ʰ��ϸ� ���� ���� �� �ʱ�ȭ
			if (timer > 0.75f)
			{
				Vector3 spawnPosition = spawnPoints[currentIndex].position;
				Quaternion spawnRotation = spawnPoints[currentIndex].rotation;
				GameObject newPlant = Instantiate(plantPrefab, spawnPosition, spawnRotation);

				Rigidbody rb = newPlant.GetComponent<Rigidbody>();
				if (rb != null)
				{
					rb.isKinematic = true; // ó������ ����
				}

				// ������ ũ�� ����
				float randomScaleFactor = Random.Range(0.5f, 2.0f);
				newPlant.transform.localScale *= randomScaleFactor;

				// ������ �絵 ����
				Fruit fruitScript = newPlant.GetComponent<Fruit>();
				if (fruitScript != null)
				{
					fruitScript.size = newPlant.transform.localScale.x;
					fruitScript.weight = fruitScript.size * 1.5f;
					fruitScript.sweetness = (Fruit.SweetnessLevel)Random.Range(0, 3);
				}


				//Instantiate(plantPrefab, spawnPosition, spawnRotation);



				// ���� �ε����� �̵�
				currentIndex++;

				// Ÿ�̸� �ʱ�ȭ
				timer = 0f;
			}
		}
	}
}
