using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
	public Transform playerTransform; // 플레이어의 Transform을 참조할 변수
	public float xOffset; // X축 오프셋 설정을 위한 public 변수
	public float yOffset; // Y축 오프셋 설정을 위한 public 변수
	public float zOffset; // Z축 오프셋 설정을 위한 public 변수

	// Update is called once per frame
	void Update()
	{
		// 허리 객체의 위치를 플레이어의 위치로 설정하되, Y축에 오프셋을 적용
		transform.position = new Vector3(playerTransform.position.x - xOffset, playerTransform.position.y - yOffset, playerTransform.position.z - zOffset);
		transform.rotation = Quaternion.Euler(playerTransform.eulerAngles.x, playerTransform.eulerAngles.y, playerTransform.eulerAngles.z);
	}
}
