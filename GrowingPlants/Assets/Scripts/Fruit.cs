using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class Fruit : MonoBehaviour
{
	public enum SweetnessLevel { Low, Medium, High }
	public SweetnessLevel sweetness; // 당도
	public float size; // 크기
	public float weight; // 무게
	private XRGrabInteractable grabInteractable;
	public AudioClip eatSound; // 비 소리
	private AudioSource audioSource; // AudioSource 컴포넌트
	public float eatDistance = 0.5f; // 먹기 위한 최소 거리
	private float eatTimer = 0f; // 먹는데 걸리는 시간
	private const float eatTime = 1f; // 과일을 먹기까지 필요한 시간
	public TextMeshProUGUI mytooltipText; // TextMeshPro 타입으로 변경
	public GameObject mytooltipCanvas; // 툴팁 캔버스 객체
	private TextMeshProUGUI wristPanelText;
	private bool isEaten = false;

	void Start()
	{
		grabInteractable = GetComponent<XRGrabInteractable>();
		if (grabInteractable != null)
		{
			grabInteractable.selectEntered.AddListener(OnGrabbed); // 업데이트된 이벤트 사용
			grabInteractable.selectExited.AddListener(OnReleased);
		}
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = eatSound;

		wristPanelText = GameObject.Find("Wrist").GetComponent<TextMeshProUGUI>();

	}

	void OnGrabbed(SelectEnterEventArgs args)
	{
		// 크기, 무게, 당도 정보 출력
		//Debug.Log($"Grabbed Fruit - Size: {size}, Weight: {weight}, Sweetness: {sweetness}");

		// 열매가 그랩될 때 Rigidbody 설정 변경
		Rigidbody rb = GetComponent<Rigidbody>();
		if (rb != null)
		{
			rb.isKinematic = false; // 열매를 그랩할 때 자유롭게 움직일 수 있도록 설정
		}
		
		TooltipManager.Instance.InitializeTooltip(mytooltipText, mytooltipCanvas);
		TooltipManager.Instance.SelectFruit(this);
		TooltipManager.Instance.ShowTooltip(true);
	}
	void OnReleased(SelectExitEventArgs args)
	{
		// 툴팁 비활성화
		TooltipManager.Instance.ShowTooltip(false);
	}

	void Update()
	{

		if (isEaten) return;

		// VR 헤드셋과의 거리를 계산
		float distanceToHeadset = Vector3.Distance(transform.position, Camera.main.transform.position);

		// 거리가 eatDistance 이내이면 타이머 증가
		if (distanceToHeadset < eatDistance)
		{
			eatTimer += Time.deltaTime;
			if (eatTimer >= eatTime)
			{
				EatFruit(); // 과일 먹기
				isEaten = true; // 과일을 먹었다고 표시
			}
		}
		else
		{
			eatTimer = 0f; // 거리가 멀어지면 타이머 리셋
		}
	}

	private void EatFruit()
	{
		WristStat wristStat = FindObjectOfType<WristStat>();

		if (wristStat != null)
		{
			// 스테미너 증가 로직
			int staminaIncrease = 0;
			switch (sweetness)
			{
				case SweetnessLevel.Low:
					staminaIncrease = 10; // 낮은 당도일 때 스테미너 10 증가
					break;
				case SweetnessLevel.Medium:
					staminaIncrease = 25; // 중간 당도일 때 스테미너 25 증가
					break;
				case SweetnessLevel.High:
					staminaIncrease = 50; // 높은 당도일 때 스테미너 50 증가
					break;
			}
			// 스테미너 변경
			wristStat.ChangeStamina(staminaIncrease);
		}

		if (eatSound != null)
		{
			AudioManager.Instance.PlayEatingSound(eatSound);
			StartCoroutine(DestroyAfterSound(eatSound.length));
		}
		else
		{
			Destroy(gameObject);
		}
	}
	IEnumerator DestroyAfterSound(float delay)
	{
		yield return new WaitForSeconds(delay);
		Destroy(gameObject);
	}

	public void UpdateTooltipText(TextMeshProUGUI tooltipText)
	{
		if (tooltipText != null)
		{
			string sweetnessText = "";
			switch (sweetness)
			{
				case SweetnessLevel.Low:
					sweetnessText = "Low";
					break;
				case SweetnessLevel.Medium:
					sweetnessText = "Medium";
					break;
				case SweetnessLevel.High:
					sweetnessText = "High";
					break;
			}

			tooltipText.text = $"Size: {size}\nWight: {weight}\nSweetness: {sweetnessText}";
		}
	}
	

}
