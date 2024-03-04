using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class TooltipManager : MonoBehaviour
{
	public static TooltipManager Instance { get; private set; }
	private TextMeshProUGUI tooltipText; // TextMeshPro Ÿ������ ����
	private Fruit selectedFruit; // ���� ���õ� ����
	private GameObject tooltipCanvas; // ���� ĵ���� ��ü

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
