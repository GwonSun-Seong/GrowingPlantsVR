using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BedInteraction : MonoBehaviour
{
	public SimpleFadeScreen fadeScreen;
	public WristStat wristStat;
	public Dialog dialog; // Dialog 스크립트에 대한 참조 추가


	private bool isSleeping = false;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !isSleeping)
		{
			// 플래그 설정하여 중복 실행 방지
			isSleeping = true;

			dialog.SetText("Zzz...");
			dialog.ShowDialog();

			// 페이드 인 시작 후 스테미너 회복 및 페이드 아웃 처리
			StartCoroutine(FadeAndRecoverRoutine());
		}
	}

	private IEnumerator FadeAndRecoverRoutine()
	{
		// 스테미너 회복
		wristStat.ChangeStamina(30);

		// 페이드 아웃 실행
		fadeScreen.FadeIn();

		// 페이드 아웃이 완료될 때까지 대기
		yield return new WaitForSeconds(fadeScreen.fadeduration);

		dialog.HideDialog();

		// 중복 실행 방지 플래그 재설정
		isSleeping = false;
	}
}
