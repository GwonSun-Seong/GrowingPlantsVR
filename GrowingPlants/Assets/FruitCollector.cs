using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCollector : MonoBehaviour
{
	private List<Fruit> collectedFruits = new List<Fruit>();

	private AudioSource audioSource; // AudioSource 컴포넌트 참조
	public AudioClip sellSoundClip; // 판매 시 재생할 오디오 클립

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		if (audioSource == null)
		{
			// AudioSource 컴포넌트가 없다면, 콘솔에 경고 메시지 출력
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

		// 오디오 클립 재생
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
