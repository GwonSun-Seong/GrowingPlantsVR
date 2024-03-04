using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuyMenu : MonoBehaviour
{
	public GameObject panel; // 패널 참조
	public Transform spawnPosition; // 아이템 생성 위치
	public WristStat playerStats; // 플레이어 상태 스크립트 참조

	public GameObject[] seedPrefabs; // 씨앗 프리팹 배열
	public Button[] buyButtons; // 버튼 프리팹 배열

	public AudioClip purchaseSound; // 구매 성공 소리
	public AudioClip failSound; // 구매 실패 소리
	private AudioSource audioSource;

	public int upgradeCost = 5; // 업그레이드 비용
	public int maxUpgradeLevel = 10; // 최대 업그레이드 레벨
	private int currentUpgradeLevel = 0; // 현재 업그레이드 레벨
	public TMP_Text upgradeLevelText;
	public Button upgradeButton;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();

		for (int i = 0; i < buyButtons.Length; i++)
		{
			int itemIndex = i; // 현재 인덱스 캡처
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
		// 씨앗 가격 배열
		int[] itemPrices = { 5, 4, 3, 4, 5 };

		// 선택된 아이템의 가격
		int price = itemPrices[itemIndex];

		// 플레이어의 돈을 확인하고 씨앗 구매 처리
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
			Bullet.damage += 1; // 데미지 증가
			upgradeCost += 5; // 다음 업그레이드 비용 증가
			audioSource.PlayOneShot(purchaseSound);
			UpdateUpgradeText();
		}
		else
		{
			audioSource.PlayOneShot(failSound);
		}
	}

	// 업그레이드 레벨 텍스트 업데이트
	private void UpdateUpgradeText()
	{
		upgradeLevelText.text = $"Damage Level: {currentUpgradeLevel}/{maxUpgradeLevel}";
	}

}
