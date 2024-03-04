using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WristStat : MonoBehaviour
{
	public int stamina = 30; // �ʱ� ���׹̳� ��
	public int hp = 100; // �ʱ� HP ��
	public int money = 0; // �ʱ� money ��
	private float staminaDecreaseTimer = 10f; // 30�ʸ��� ���׹̳� ����
	private float hpDecreaseTimer = 2f; // 3�ʸ��� HP ����
	public TextMeshProUGUI staminaText; // HP�� ���׹̳ʸ� ��� ǥ���� TextMeshProUGUI ������Ʈ ����
	public SimpleFadeScreen fadeScreen;
	public GameObject fadeScreenobj;
	public Dialog dialog;
	public bool isAlreadyDead = false;
	public ActionBasedContinuousMoveProvider moveProvider;

	private bool isLowStaminaWarningShown = false; // ���׹̳� ��� �̹� ǥ�õǾ����� ����

	void Start()
	{
		UpdateStaminaAndHpText();
	}

	void Update()
	{
		// ���׹̳� ���� Ÿ�̸�
		staminaDecreaseTimer -= Time.deltaTime;
		if (staminaDecreaseTimer <= 0f)
		{
			ChangeStamina(-1); // ���׹̳� 1 ����
			staminaDecreaseTimer = 10f; // Ÿ�̸� �缳��
		}

		if (stamina <= 10 && !isLowStaminaWarningShown)
		{
			ShowLowStaminaWarning();
			isLowStaminaWarningShown = true; // ��� ǥ�õǾ��ٰ� ǥ��
		}
		else if (stamina > 10 && isLowStaminaWarningShown)
		{
			isLowStaminaWarningShown = false; // ���׹̳ʰ� ȸ���Ǹ� �÷��� ����
		}

		if (stamina <= 10)
		{
			if (moveProvider != null)
			{
				moveProvider.moveSpeed = 2; // �ӵ� ����
			}
		}
		// ���׹̳ʰ� 11 �̻��� ��
		else
		{
			if (moveProvider != null)
			{
				moveProvider.moveSpeed = 4; // �⺻ �ӵ��� ����
			}
		}

		// HP ���� Ÿ�̸�
		hpDecreaseTimer -= Time.deltaTime;
		if (hpDecreaseTimer <= 0f && !isAlreadyDead)
		{
			ChangeHp(+1); // HP 1 ����
			hpDecreaseTimer = 2f; // Ÿ�̸� �缳��
		}
	}

	public void ChangeStamina(int amount)
	{
		stamina += amount;
		stamina = Mathf.Clamp(stamina, 0, 100); // ���׹̳� ���� ����
		UpdateStaminaAndHpText();
	}

	public void ChangeHp(int amount)
	{
		hp += amount;
		hp = Mathf.Clamp(hp, 0, 100); // HP ���� ����
		UpdateStaminaAndHpText();

		if (hp <= 0 && !isAlreadyDead)
		{
			isAlreadyDead = true; // �÷��̾ �׾��ٰ� ǥ��
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

		// ���� ������ ���� �ʵ��� üũ
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
		dialog.SetText("I'm feeling tired"); // �ؽ�Ʈ ����
		dialog.ShowDialog(); // ���̾�α� ǥ��
		StartCoroutine(HideDialogAfterDelay(2)); // 2�� �Ŀ� ���̾�α� �����
	}

	IEnumerator HideDialogAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		dialog.HideDialog(); // ���̾�α� �����
	}
}
