using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCollector : MonoBehaviour
{
	private List<Fruit> collectedFruits = new List<Fruit>();

	private AudioSource audioSource; // AudioSource ������Ʈ ����
	public AudioClip sellSoundClip; // �Ǹ� �� ����� ����� Ŭ��

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		if (audioSource == null)
		{
			// AudioSource ������Ʈ�� ���ٸ�, �ֿܼ� ��� �޽��� ���
			Debug.LogWarning("AudioSource component not found on the GameObject");
		}
	}

	private float CalculateItemValue(Fruit fruit)
	{
		float sweetnessMultiplier = 1.0f;
		switch (fruit.sweetness)
		{
			case Fruit.SweetnessLevel.Low:
				sweetnessMultiplier = 0.5f;
				break;
			case Fruit.SweetnessLevel.Medium:
				sweetnessMultiplier = 1.0f;
				break;
			case Fruit.SweetnessLevel.High:
				sweetnessMultiplier = 1.5f;
				break;
		}

		return fruit.size * sweetnessMultiplier * 10.0f;
	}

	private void SellFruit(Fruit fruit)
	{
		float itemValue = CalculateItemValue(fruit);
		int moneyIncrease = Mathf.RoundToInt(itemValue);
		FindObjectOfType<WristStat>().ChangeMoney(moneyIncrease);

		// ����� Ŭ�� ���
		if (sellSoundClip != null && audioSource != null)
		{
			audioSource.PlayOneShot(sellSoundClip);
		}

		Destroy(fruit.gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		Fruit fruit = other.GetComponent<Fruit>();
		if (fruit != null)
		{
			SellFruit(fruit);
		}
	}
}
