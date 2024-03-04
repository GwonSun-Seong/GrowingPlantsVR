using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class Fruit : MonoBehaviour
{
	public enum SweetnessLevel { Low, Medium, High }
	public SweetnessLevel sweetness; // �絵
	public float size; // ũ��
	public float weight; // ����
	private XRGrabInteractable grabInteractable;
	public AudioClip eatSound; // �� �Ҹ�
	private AudioSource audioSource; // AudioSource ������Ʈ
	public float eatDistance = 0.5f; // �Ա� ���� �ּ� �Ÿ�
	private float eatTimer = 0f; // �Դµ� �ɸ��� �ð�
	private const float eatTime = 1f; // ������ �Ա���� �ʿ��� �ð�
	public TextMeshProUGUI mytooltipText; // TextMeshPro Ÿ������ ����
	public GameObject mytooltipCanvas; // ���� ĵ���� ��ü
	private TextMeshProUGUI wristPanelText;
	private bool isEaten = false;

	void Start()
	{
		grabInteractable = GetComponent<XRGrabInteractable>();
		if (grabInteractable != null)
		{
			grabInteractable.selectEntered.AddListener(OnGrabbed); // ������Ʈ�� �̺�Ʈ ���
			grabInteractable.selectExited.AddListener(OnReleased);
		}
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = eatSound;

		wristPanelText = GameObject.Find("Wrist").GetComponent<TextMeshProUGUI>();

	}

	void OnGrabbed(SelectEnterEventArgs args)
	{
		// ũ��, ����, �絵 ���� ���
		//Debug.Log($"Grabbed Fruit - Size: {size}, Weight: {weight}, Sweetness: {sweetness}");

		// ���Ű� �׷��� �� Rigidbody ���� ����
		Rigidbody rb = GetComponent<Rigidbody>();
		if (rb != null)
		{
			rb.isKinematic = false; // ���Ÿ� �׷��� �� �����Ӱ� ������ �� �ֵ��� ����
		}
		
		TooltipManager.Instance.InitializeTooltip(mytooltipText, mytooltipCanvas);
		TooltipManager.Instance.SelectFruit(this);
		TooltipManager.Instance.ShowTooltip(true);
	}
	void OnReleased(SelectExitEventArgs args)
	{
		// ���� ��Ȱ��ȭ
		TooltipManager.Instance.ShowTooltip(false);
	}

	void Update()
	{

		if (isEaten) return;

		// VR ���°��� �Ÿ��� ���
		float distanceToHeadset = Vector3.Distance(transform.position, Camera.main.transform.position);

		// �Ÿ��� eatDistance �̳��̸� Ÿ�̸� ����
		if (distanceToHeadset < eatDistance)
		{
			eatTimer += Time.deltaTime;
			if (eatTimer >= eatTime)
			{
				EatFruit(); // ���� �Ա�
				isEaten = true; // ������ �Ծ��ٰ� ǥ��
			}
		}
		else
		{
			eatTimer = 0f; // �Ÿ��� �־����� Ÿ�̸� ����
		}
	}

	private void EatFruit()
	{
		WristStat wristStat = FindObjectOfType<WristStat>();

		if (wristStat != null)
		{
			// ���׹̳� ���� ����
			int staminaIncrease = 0;
			switch (sweetness)
			{
				case SweetnessLevel.Low:
					staminaIncrease = 10; // ���� �絵�� �� ���׹̳� 10 ����
					break;
				case SweetnessLevel.Medium:
					staminaIncrease = 25; // �߰� �絵�� �� ���׹̳� 25 ����
					break;
				case SweetnessLevel.High:
					staminaIncrease = 50; // ���� �絵�� �� ���׹̳� 50 ����
					break;
			}
			// ���׹̳� ����
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
