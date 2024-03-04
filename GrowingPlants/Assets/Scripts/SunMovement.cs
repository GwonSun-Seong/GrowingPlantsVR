using System.Collections;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
	public float dayLengthInSeconds = 360f; // 6분을 하루로 설정 (360초)
	public Transform sunTransform; // 디렉셔널 라이트의 Transform

	private float rotationSpeed; // 햇빛 회전 속도
	private float timeOfDay = 0f; // 현재 시간 (0에서 1 사이)

	public enum TimeOfDay { Morning, Noon, Night }
	public TimeOfDay currentTimeOfDay;
	private TimeOfDay previousTimeOfDay;
	public GameObject fadeScreenobj;

	public Material morningSkybox; // 아침 스카이박스
	public Material noonSkybox; // 점심 스카이박스
	public Material nightSkybox; // 저녁 스카이박스

	public Dialog dialogScript; // Dialog 스크립트 참조
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
		if (timeOfDay >= 1f) // 하루가 끝나면 시간을 초기화합니다.
		{
			timeOfDay -= 1f;
		}

		float angle = timeOfDay * 360f;
		sunTransform.rotation = Quaternion.Euler(new Vector3(angle, 0f, 0f));

		// 시간대 변경 확인
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
			// 스카이박스 및 디렉셔널 라이트 변경
			ChangeSkybox();

			// 페이드 인/아웃 실행
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
		// 페이드 아웃 로직
		fadeScreenobj.SetActive(true);
		fadeScreen.FadeIn();
		yield return new WaitForSeconds(1.5f); // 페이드 지속 시간 조정
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
