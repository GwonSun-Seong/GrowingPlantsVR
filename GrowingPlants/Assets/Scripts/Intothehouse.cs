using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intothehouse : MonoBehaviour
{
	public Transform playerTransform; // 플레이어의 Transform을 설정해주어야 합니다.
	public Transform destinationPosition; // 목표 포지션

	private void OnCollisionEnter(Collision collision)
	{
		// 플레이어가 콜라이더에 충돌했을 때
		if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("MainCamera"))
		{
			// 플레이어의 위치를 목표 포지션으로 옮기기
			playerTransform.position = destinationPosition.position;
		}
	}
}
