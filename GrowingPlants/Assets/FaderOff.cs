using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaderOff : MonoBehaviour
{
	public GameObject fadeScreenobj;
	private void OnTriggerEnter(Collider other)
	{
		// 특정 태그 또는 조건을 사용하여 원하는 오브젝트가 트리거에 닿았는지 확인
		// 예를 들어, 'Player' 태그를 가진 오브젝트에만 반응하도록 설정
		if (other.CompareTag("Player"))
		{
			fadeScreenobj.SetActive(false);
		}
	}
}