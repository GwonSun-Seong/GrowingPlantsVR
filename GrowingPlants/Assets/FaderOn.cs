using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaderOn : MonoBehaviour
{
	// Start is called before the first frame update
	public GameObject fadeScreenobj;
	private void Start()
	{

	}
	private void OnTriggerEnter(Collider other)
	{
		// Ư�� �±� �Ǵ� ������ ����Ͽ� ���ϴ� ������Ʈ�� Ʈ���ſ� ��Ҵ��� Ȯ��
		// ���� ���, 'Player' �±׸� ���� ������Ʈ���� �����ϵ��� ����
		if (other.CompareTag("Player"))
		{
			fadeScreenobj.SetActive(true);
		}
	}
}
