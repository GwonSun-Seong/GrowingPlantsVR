using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intothehouse : MonoBehaviour
{
	public Transform playerTransform; // �÷��̾��� Transform�� �������־�� �մϴ�.
	public Transform destinationPosition; // ��ǥ ������

	private void OnCollisionEnter(Collision collision)
	{
		// �÷��̾ �ݶ��̴��� �浹���� ��
		if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("MainCamera"))
		{
			// �÷��̾��� ��ġ�� ��ǥ ���������� �ű��
			playerTransform.position = destinationPosition.position;
		}
	}
}
