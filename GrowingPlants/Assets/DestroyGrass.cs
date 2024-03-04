using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGrass : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		// 들어오는 객체가 'Grass' 태그를 가지고 있는지 확인
		if (other.CompareTag("Grass"))
		{
			// 객체를 1초 후에 파괴
			Destroy(other.gameObject, 1f);
		}
	}
}
