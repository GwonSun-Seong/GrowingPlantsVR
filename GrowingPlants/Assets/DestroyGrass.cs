using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGrass : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		// ������ ��ü�� 'Grass' �±׸� ������ �ִ��� Ȯ��
		if (other.CompareTag("Grass"))
		{
			// ��ü�� 1�� �Ŀ� �ı�
			Destroy(other.gameObject, 1f);
		}
	}
}
