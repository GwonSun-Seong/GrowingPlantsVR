using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
	public GameObject loadingPanel; // Inspector���� �Ҵ�
	public Slider loadingSlider; // Inspector���� �Ҵ�

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

		loadingPanel.SetActive(false); // �ε� �г� ��Ȱ��ȭ
	}
}
