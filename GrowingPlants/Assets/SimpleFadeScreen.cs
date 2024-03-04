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
			// ���� rend�� null�̶�� ���� ��ũ��Ʈ�� ����� ���� ������Ʈ���� Renderer ������Ʈ�� ã�� �Ҵ��մϴ�.
			rend = GetComponent<Renderer>();
		}

		if (rend != null)
		{
			// Renderer ������Ʈ�� ��ȿ�� ��쿡�� FadeRoutine�� �����մϴ�.
			StartCoroutine(FadeRoutine(alphaIn, alphaOut));
		}
		else
		{
			// Renderer ������Ʈ�� ���� ��� ��� �޽����� ����մϴ�.
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
		FadeOut(); // FadeOut �޼��带 ����Ͽ� ���� ����
	}
	public void SetDefaultFadeColor()
	{
		fadeColor = defaultFadeColor;
	}
	public void FadeToColor(Color color, float duration, bool resetToDefault = true)
	{
		fadeColor = color;
		fadeduration = duration;
		FadeOut(); // FadeOut �޼��带 ����Ͽ� ���� ����

		if (resetToDefault)
		{
			StartCoroutine(ResetColorAfterDuration(duration));
		}
	}

	// ������ �ð� �Ŀ� �⺻ �������� �ٽ� �����ϴ� �ڷ�ƾ
	private IEnumerator ResetColorAfterDuration(float duration)
	{
		yield return new WaitForSeconds(duration);
		SetDefaultFadeColor();
	}
}
