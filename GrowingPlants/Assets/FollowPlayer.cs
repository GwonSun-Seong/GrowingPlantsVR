using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
	public Transform playerTransform; // �÷��̾��� Transform�� ������ ����
	public float xOffset; // X�� ������ ������ ���� public ����
	public float yOffset; // Y�� ������ ������ ���� public ����
	public float zOffset; // Z�� ������ ������ ���� public ����

	// Update is called once per frame
	void Update()
	{
		// �㸮 ��ü�� ��ġ�� �÷��̾��� ��ġ�� �����ϵ�, Y�࿡ �������� ����
		transform.position = new Vector3(playerTransform.position.x - xOffset, playerTransform.position.y - yOffset, playerTransform.position.z - zOffset);
		transform.rotation = Quaternion.Euler(playerTransform.eulerAngles.x, playerTransform.eulerAngles.y, playerTransform.eulerAngles.z);
	}
}
