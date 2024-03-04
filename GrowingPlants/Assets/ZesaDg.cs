using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZesaDg : MonoBehaviour
{
	public Dialog dialog; // Dialog ��ũ��Ʈ�� ���� ���� �߰�
						  // Start is called before the first frame update
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			dialog.SetText("Shaking this magic wand makes it seem like it will rain.");
			dialog.ShowDialog();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		dialog.HideDialog();
	}
}

