using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaderOff : MonoBehaviour
{
	public GameObject fadeScreenobj;
	private void OnTriggerEnter(Collider other)
	{
		// Ư�� �±� �Ǵ� ������ ����Ͽ� ���ϴ� ������Ʈ�� Ʈ���ſ� ��Ҵ��� Ȯ��
		// ���� ���, 'Player' �±׸� ���� ������Ʈ���� �����ϵ��� ����
		if (other.CompareTag("Player"))
		{
			fadeScreenobj.SetActive(false);
		}
	}
}