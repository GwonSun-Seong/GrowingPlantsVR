using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class TooltipManager : MonoBehaviour
{
	public static TooltipManager Instance { get; private set; }
	private TextMeshProUGUI tooltipText; // TextMeshPro 타입으로 변경
	private Fruit selectedFruit; // 현재 선택된 과일
	private GameObject tooltipCanvas; // 툴팁 캔버스 객체

	public void ShowTooltip(bool show)
	{
		if (tooltipCanvas != null)
		{
			tooltipCanvas.SetActive(show);
		}
	}
	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}
	public void InitializeTooltip(TextMeshProUGUI text, GameObject canvas)
	{
		tooltipText = text;
		tooltipCanvas = canvas;
	}
	public void SelectFruit(Fruit fruit)
	{
		selectedFruit = fruit;
		if (selectedFruit != null)
		{
			selectedFruit.UpdateTooltipText(tooltipText);
		}
	}
	
}
