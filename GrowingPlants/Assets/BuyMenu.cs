using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuyMenu : MonoBehaviour
{
	public GameObject panel; // �г� ����
	public Transform spawnPosition; // ������ ���� ��ġ
	public WristStat playerStats; // �÷��̾� ���� ��ũ��Ʈ ����

	public GameObject[] seedPrefabs; // ���� ������ �迭
	public Button[] buyButtons; // ��ư ������ �迭

	public AudioClip purchaseSound; // ���� ���� �Ҹ�
	public AudioClip failSound; // ���� ���� �Ҹ�
	private AudioSource audioSource;

	public int upgradeCost = 5; // ���׷��̵� ���
	public int maxUpgradeLevel = 10; // �ִ� ���׷��̵� ����
	private int currentUpgradeLevel = 0; // ���� ���׷��̵� ����
	public TMP_Text upgradeLevelText;
	public Button upgradeButton;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();

		for (int i = 0; i < buyButtons.Length; i++)
		{
			int itemIndex = i; // ���� �ε��� ĸó
			buyButtons[i].onClick.AddListener(() => TryPurchase(itemIndex));
		}
		upgradeButton.onClick.AddListener(UpgradeDamage);
		UpdateUpgradeText();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			panel.SetActive(true);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			panel.SetActive(false);
		}
	}

	public void TryPurchase(int itemIndex)
	{
		// ���� ���� �迭
		int[] itemPrices = { 5, 4, 3, 4, 5 };

		// ���õ� �������� ����
		int price = itemPrices[itemIndex];

		// �÷��̾��� ���� Ȯ���ϰ� ���� ���� ó��
		if (playerStats.money >= price)
		{
			playerStats.ChangeMoney(-price);
			Instantiate(seedPrefabs[itemIndex], spawnPosition.position, Quaternion.identity);
			audioSource.PlayOneShot(purchaseSound);
		}
		else
		{
			audioSource.PlayOneShot(failSound);
		}
	}
	public void UpgradeDamage()
	{
		if (currentUpgradeLevel < maxUpgradeLevel && playerStats.money >= upgradeCost)
		{
			playerStats.ChangeMoney(-upgradeCost);
			currentUpgradeLevel++;
			Bullet.damage += 1; // ������ ����
			upgradeCost += 5; // ���� ���׷��̵� ��� ����
			audioSource.PlayOneShot(purchaseSound);
			UpdateUpgradeText();
		}
		else
		{
			audioSource.PlayOneShot(failSound);
		}
	}

	// ���׷��̵� ���� �ؽ�Ʈ ������Ʈ
	private void UpdateUpgradeText()
	{
		upgradeLevelText.text = $"Damage Level: {currentUpgradeLevel}/{maxUpgradeLevel}";
	}

}
