using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
	public GameObject loadingPanel; // Inspector에서 할당
	public Slider loadingSlider; // Inspector에서 할당

	void Start()
	{
		StartCoroutine(StartGameAfterDelay(5f));
	}

	IEnumerator StartGameAfterDelay(float delay)
	{
		float elapsedTime = 0;

		while (elapsedTime < delay)
		{
			elapsedTime += Time.deltaTime;
			loadingSlider.value = elapsedTime / delay;
			yield return null;
		}

		loadingPanel.SetActive(false); // 로딩 패널 비활성화
	}
}
