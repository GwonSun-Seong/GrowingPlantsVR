using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFadeScreen : MonoBehaviour
{
	public bool fadeOnStart = true;
	// Start is called before the first frame update
	public float fadeduration = 2;
	public Color fadeColor;
	public Color defaultFadeColor = Color.black;
	private Renderer rend;
	void Start()
	{
		rend = GetComponent<Renderer>();
		if (fadeOnStart)
			FadeIn();
	}
	public void FadeIn()
	{
		Fade(1, 0);
	}
	public void FadeOut()
	{
		Fade(0, 1);
	}
	public void Fade(float alphaIn, float alphaOut)
	{
		if (rend == null)
		{
			// 만약 rend가 null이라면 현재 스크립트가 연결된 게임 오브젝트에서 Renderer 컴포넌트를 찾아 할당합니다.
			rend = GetComponent<Renderer>();
		}

		if (rend != null)
		{
			// Renderer 컴포넌트가 유효한 경우에만 FadeRoutine을 시작합니다.
			StartCoroutine(FadeRoutine(alphaIn, alphaOut));
		}
		else
		{
			// Renderer 컴포넌트가 없을 경우 경고 메시지를 출력합니다.
			Debug.LogWarning("Renderer component not found.");
		}

	}
	public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
	{
		float timer = 0;
		while (timer <= fadeduration)
		{
			Color newColor = fadeColor;
			newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeduration);

			rend.material.SetColor("_Color", newColor);

			timer += Time.deltaTime;
			yield return null;
		}

		Color newColor2 = fadeColor;
		newColor2.a = alphaOut;

		rend.material.SetColor("_Color", newColor2);
	}
	// Update is called once per frame
	public void FadeToColor(Color color, float duration)
	{
		fadeColor = color;
		fadeduration = duration;
		FadeOut(); // FadeOut 메서드를 사용하여 색상 변경
	}
	public void SetDefaultFadeColor()
	{
		fadeColor = defaultFadeColor;
	}
	public void FadeToColor(Color color, float duration, bool resetToDefault = true)
	{
		fadeColor = color;
		fadeduration = duration;
		FadeOut(); // FadeOut 메서드를 사용하여 색상 변경

		if (resetToDefault)
		{
			StartCoroutine(ResetColorAfterDuration(duration));
		}
	}

	// 지정된 시간 후에 기본 색상으로 다시 설정하는 코루틴
	private IEnumerator ResetColorAfterDuration(float duration)
	{
		yield return new WaitForSeconds(duration);
		SetDefaultFadeColor();
	}
}
