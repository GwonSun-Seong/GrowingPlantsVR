using System.Collections;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
	public float dayLengthInSeconds = 360f; // 6���� �Ϸ�� ���� (360��)
	public Transform sunTransform; // �𷺼ų� ����Ʈ�� Transform

	private float rotationSpeed; // �޺� ȸ�� �ӵ�
	private float timeOfDay = 0f; // ���� �ð� (0���� 1 ����)

	public enum TimeOfDay { Morning, Noon, Night }
	public TimeOfDay currentTimeOfDay;
	private TimeOfDay previousTimeOfDay;
	public GameObject fadeScreenobj;

	public Material morningSkybox; // ��ħ ��ī�̹ڽ�
	public Material noonSkybox; // ���� ��ī�̹ڽ�
	public Material nightSkybox; // ���� ��ī�̹ڽ�

	public Dialog dialogScript; // Dialog ��ũ��Ʈ ����
	public SimpleFadeScreen fadeScreen;

	void Start()
	{
		sunTransform = GetComponent<Transform>();
		sunTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
		rotationSpeed = 360f / dayLengthInSeconds;
		previousTimeOfDay = currentTimeOfDay;
	}

	void Update()
	{
		timeOfDay += Time.deltaTime / dayLengthInSeconds;
		if (timeOfDay >= 1f) // �Ϸ簡 ������ �ð��� �ʱ�ȭ�մϴ�.
		{
			timeOfDay -= 1f;
		}

		float angle = timeOfDay * 360f;
		sunTransform.rotation = Quaternion.Euler(new Vector3(angle, 0f, 0f));

		// �ð��� ���� Ȯ��
		UpdateTimeOfDay();
	}

	void UpdateTimeOfDay()
	{
		if (timeOfDay < 0.33f)
			currentTimeOfDay = TimeOfDay.Morning;
		else if (timeOfDay < 0.66f)
			currentTimeOfDay = TimeOfDay.Noon;
		else
			currentTimeOfDay = TimeOfDay.Night;

		if (previousTimeOfDay != currentTimeOfDay)
		{
			// ��ī�̹ڽ� �� �𷺼ų� ����Ʈ ����
			ChangeSkybox();

			// ���̵� ��/�ƿ� ����
			StartCoroutine(FadeInOut());

			previousTimeOfDay = currentTimeOfDay;

			string message = GetCurrentTimeOfDayMessage();
			dialogScript.SetText(message);
			StartCoroutine(ShowDialogTemporarily(message, 2f));
		}
	}
	void ChangeSkybox()
	{
		Material newSkybox = null;
		switch (currentTimeOfDay)
		{
			case TimeOfDay.Morning:
				newSkybox = morningSkybox;
				break;
			case TimeOfDay.Noon:
				newSkybox = noonSkybox;
				break;
			case TimeOfDay.Night:
				newSkybox = nightSkybox;
				break;
		}

		if (newSkybox != null)
			RenderSettings.skybox = newSkybox;
	}
	
	IEnumerator FadeInOut()
	{
		// ���̵� �ƿ� ����
		fadeScreenobj.SetActive(true);
		fadeScreen.FadeIn();
		yield return new WaitForSeconds(1.5f); // ���̵� ���� �ð� ����
		fadeScreen.FadeOut();
		fadeScreenobj.SetActive(false);
	}
	string GetCurrentTimeOfDayMessage()
	{
		switch (currentTimeOfDay)
		{
			case TimeOfDay.Morning:
				return "Moring";
			case TimeOfDay.Noon:
				return "Noon";
			case TimeOfDay.Night:
				return "Night";
			default:
				return "";
		}
	}
	IEnumerator ShowDialogTemporarily(string text, float duration)
	{
		dialogScript.SetText(text);
		dialogScript.ShowDialog();
		yield return new WaitForSeconds(duration);
		dialogScript.HideDialog();
	}
}
