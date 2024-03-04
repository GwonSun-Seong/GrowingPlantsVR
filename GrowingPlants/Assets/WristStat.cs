using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WristStat : MonoBehaviour
{
	public int stamina = 30; // 초기 스테미너 값
	public int hp = 100; // 초기 HP 값
	public int money = 0; // 초기 money 값
	private float staminaDecreaseTimer = 10f; // 30초마다 스테미너 감소
	private float hpDecreaseTimer = 2f; // 3초마다 HP 증가
	public TextMeshProUGUI staminaText; // HP와 스테미너를 모두 표시할 TextMeshProUGUI 컴포넌트 참조
	public SimpleFadeScreen fadeScreen;
	public GameObject fadeScreenobj;
	public Dialog dialog;
	public bool isAlreadyDead = false;
	public ActionBasedContinuousMoveProvider moveProvider;

	private bool isLowStaminaWarningShown = false; // 스테미너 경고가 이미 표시되었는지 여부

	void Start()
	{
		UpdateStaminaAndHpText();
	}

	void Update()
	{
		// 스테미너 감소 타이머
		staminaDecreaseTimer -= Time.deltaTime;
		if (staminaDecreaseTimer <= 0f)
		{
			ChangeStamina(-1); // 스테미너 1 감소
			staminaDecreaseTimer = 10f; // 타이머 재설정
		}

		if (stamina <= 10 && !isLowStaminaWarningShown)
		{
			ShowLowStaminaWarning();
			isLowStaminaWarningShown = true; // 경고가 표시되었다고 표시
		}
		else if (stamina > 10 && isLowStaminaWarningShown)
		{
			isLowStaminaWarningShown = false; // 스테미너가 회복되면 플래그 리셋
		}

		if (stamina <= 10)
		{
			if (moveProvider != null)
			{
				moveProvider.moveSpeed = 2; // 속도 감소
			}
		}
		// 스테미너가 11 이상일 때
		else
		{
			if (moveProvider != null)
			{
				moveProvider.moveSpeed = 4; // 기본 속도로 복원
			}
		}

		// HP 감소 타이머
		hpDecreaseTimer -= Time.deltaTime;
		if (hpDecreaseTimer <= 0f && !isAlreadyDead)
		{
			ChangeHp(+1); // HP 1 증가
			hpDecreaseTimer = 2f; // 타이머 재설정
		}
	}

	public void ChangeStamina(int amount)
	{
		stamina += amount;
		stamina = Mathf.Clamp(stamina, 0, 100); // 스테미너 범위 조절
		UpdateStaminaAndHpText();
	}

	public void ChangeHp(int amount)
	{
		hp += amount;
		hp = Mathf.Clamp(hp, 0, 100); // HP 범위 조절
		UpdateStaminaAndHpText();

		if (hp <= 0 && !isAlreadyDead)
		{
			isAlreadyDead = true; // 플레이어가 죽었다고 표시
			fadeScreenobj.SetActive(true);
			fadeScreen.FadeToColor(Color.black, 2f);
			dialog.SetText("Game Over\nPress A to re-start");
			dialog.ShowDialog();
			fadeScreenobj.SetActive(false);
		}
		else if(!isAlreadyDead)
		{
			dialog.HideDialog();
		}

		}

	public void ChangeMoney(int amount)
	{
		money += amount;

		// 돈이 음수가 되지 않도록 체크
		if (money < 0)
		{
			money = 0;
		}
	}

	private void UpdateStaminaAndHpText()
	{
		string staminaStatus;
		if (stamina < 30) staminaStatus = "Bad";
		else if (stamina <= 70) staminaStatus = "Normal";
		else staminaStatus = "Good";

		staminaText.text = $"HP: {hp}\nStamina: {staminaStatus}\n{stamina}\nMoney: {money}";
	}

	void ShowLowStaminaWarning()
	{
		dialog.SetText("I'm feeling tired"); // 텍스트 변경
		dialog.ShowDialog(); // 다이얼로그 표시
		StartCoroutine(HideDialogAfterDelay(2)); // 2초 후에 다이얼로그 숨기기
	}

	IEnumerator HideDialogAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		dialog.HideDialog(); // 다이얼로그 숨기기
	}
}
