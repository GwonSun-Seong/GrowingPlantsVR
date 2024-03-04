using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FollowCharacterController : MonoBehaviour
{
	public CharacterController characterController; // 캐릭터 컨트롤러 참조

	private CapsuleCollider capsuleCollider; // 캡슐 콜라이더 참조

	void Start()
	{
		// 캡슐 콜라이더 컴포넌트 가져오기 또는 추가하기
		capsuleCollider = GetComponent<CapsuleCollider>();
		if (capsuleCollider == null)
		{
			capsuleCollider = gameObject.AddComponent<CapsuleCollider>();
		}

		// 캡슐 콜라이더의 높이와 중심을 캐릭터 컨트롤러와 동일하게 설정
		capsuleCollider.height = characterController.height;
		capsuleCollider.center = characterController.center;
	}

	void Update()
	{
		if (characterController != null)
		{
			// 캐릭터 컨트롤러의 중심 위치 가져오기
			Vector3 characterCenter = characterController.bounds.center;

			// 스피어 콜라이더 위치를 캐릭터 컨트롤러 중심 위치로 설정
			transform.position = characterCenter;
		}
	}
}
