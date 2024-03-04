using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrownPumpkinPlant : MonoBehaviour
{
	void Start()
	{
		// Pumpkin 하위 객체 찾기
		Transform pumpkinTransform = transform.Find("Pumpkin_Fruit");

		// 랜덤 당도 및 무게값 설정
		if (pumpkinTransform != null)
		{
			Fruit fruitScript = pumpkinTransform.GetComponent<Fruit>();
			if (fruitScript != null)
			{
				float randomScaleFactor = Random.Range(0.5f, 2.0f);
				pumpkinTransform.localScale *= randomScaleFactor;

				fruitScript.size = pumpkinTransform.localScale.x;
				fruitScript.weight = fruitScript.size * 1.5f;
				fruitScript.sweetness = (Fruit.SweetnessLevel)Random.Range(0, 3);
			}
		}
	}
}
