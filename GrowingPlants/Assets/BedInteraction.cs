using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BedInteraction : MonoBehaviour
{
	public SimpleFadeScreen fadeScreen;
	public WristStat wristStat;
	public Dialog dialog; // Dialog ��ũ��Ʈ�� ���� ���� �߰�


	private bool isSleeping = false;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !isSleeping)
		{
			// �÷��� �����Ͽ� �ߺ� ���� ����
			isSleeping = true;

			dialog.SetText("Zzz...");
			dialog.ShowDialog();

			// ���̵� �� ���� �� ���׹̳� ȸ�� �� ���̵� �ƿ� ó��
			StartCoroutine(FadeAndRecoverRoutine());
		}
	}

	private IEnumerator FadeAndRecoverRoutine()
	{
		// ���׹̳� ȸ��
		wristStat.ChangeStamina(30);

		// ���̵� �ƿ� ����
		fadeScreen.FadeIn();

		// ���̵� �ƿ��� �Ϸ�� ������ ���
		yield return new WaitForSeconds(fadeScreen.fadeduration);

		dialog.HideDialog();

		// �ߺ� ���� ���� �÷��� �缳��
		isSleeping = false;
	}
}
