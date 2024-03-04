using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrownPumpkinPlant : MonoBehaviour
{
	void Start()
	{
		// Pumpkin ���� ��ü ã��
		Transform pumpkinTransform = transform.Find("Pumpkin_Fruit");

		// ���� �絵 �� ���԰� ����
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
