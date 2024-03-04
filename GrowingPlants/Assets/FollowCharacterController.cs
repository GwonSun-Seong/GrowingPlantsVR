using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FollowCharacterController : MonoBehaviour
{
	public CharacterController characterController; // ĳ���� ��Ʈ�ѷ� ����

	private CapsuleCollider capsuleCollider; // ĸ�� �ݶ��̴� ����

	void Start()
	{
		// ĸ�� �ݶ��̴� ������Ʈ �������� �Ǵ� �߰��ϱ�
		capsuleCollider = GetComponent<CapsuleCollider>();
		if (capsuleCollider == null)
		{
			capsuleCollider = gameObject.AddComponent<CapsuleCollider>();
		}

		// ĸ�� �ݶ��̴��� ���̿� �߽��� ĳ���� ��Ʈ�ѷ��� �����ϰ� ����
		capsuleCollider.height = characterController.height;
		capsuleCollider.center = characterController.center;
	}

	void Update()
	{
		if (characterController != null)
		{
			// ĳ���� ��Ʈ�ѷ��� �߽� ��ġ ��������
			Vector3 characterCenter = characterController.bounds.center;

			// ���Ǿ� �ݶ��̴� ��ġ�� ĳ���� ��Ʈ�ѷ� �߽� ��ġ�� ����
			transform.position = characterCenter;
		}
	}
}
