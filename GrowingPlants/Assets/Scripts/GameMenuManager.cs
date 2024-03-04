using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

public class GameMenuManager : MonoBehaviour
{
	public GameObject menu;
	public InputActionProperty showButton;
	public BackgroundMusicManager backgroundMusicManager;
	public Transform head;
	public Slider MainvolumeSlider;
	//public Slider EffectvolumeSlider;
	public TMP_Dropdown bgmDropdown;
	public TMP_Dropdown petDropdown;
	public GameObject dogObject; // 강아지 오브젝트
	public GameObject catObject; // 고양이 오브젝트
	public float spawnDistance = 2;
	public float minimumHeight = 1.5f; // 메뉴의 최소 높이 설정


	// Start is called before the first frame update
	void Start()
	{
		MainvolumeSlider.value = 0.2f;
		//EffectvolumeSlider.value = 0.2f;
		OnMainVolumeSliderChanged(MainvolumeSlider.value);

		if (bgmDropdown == null)
		{
			return;
		}

		// 드롭다운 옵션 초기화 및 설정
		bgmDropdown.ClearOptions();
		List<string> bgmOptions = new List<string>();
		foreach (var bgmClip in backgroundMusicManager.bgmClips)
		{
			bgmOptions.Add(bgmClip.name);
		}
		bgmDropdown.AddOptions(bgmOptions);

		bgmDropdown.onValueChanged.AddListener(delegate {
			OnBgmSelectionChanged(bgmDropdown.value);
		});

		petDropdown.ClearOptions();
		List<string> options = new List<string> { "None", "Dog", "Cat", "Both" };
		petDropdown.AddOptions(options);

		petDropdown.onValueChanged.AddListener(OnPetSelectionChanged);

		petDropdown.value = 2;
		OnPetSelectionChanged(petDropdown.value);

	}
	public void OnBgmSelectionChanged(int index)
	{
		if (index >= 0 && index < backgroundMusicManager.bgmClips.Length)
		{
			AudioClip selectedClip = backgroundMusicManager.bgmClips[index];
			backgroundMusicManager.PlayBgm(selectedClip);
		}
	}

	void OnPetSelectionChanged(int index)
	{
		// 모든 애완동물 비활성화
		dogObject.SetActive(false);
		catObject.SetActive(false);

		// 선택된 애완동물 활성화
		if (index == 1) // Dog
		{
			dogObject.SetActive(true);
		}
		else if (index == 2) // Cat
		{
			catObject.SetActive(true);
		}
		else if (index == 3) // Both
		{
			dogObject.SetActive(true);
			catObject.SetActive(true);
		}
	}


	// Update is called once per frame
	void Update()
	{
		if (showButton.action.WasPressedThisFrame())
		{
			menu.SetActive(!menu.activeSelf);

			// 플레이어 머리 위치를 기준으로 메뉴 위치 설정
			Vector3 menuPosition = head.position + head.forward.normalized * spawnDistance;
			// 메뉴가 지면보다 너무 낮지 않도록 y 좌표 조정
			menuPosition.y = Mathf.Max(menuPosition.y, minimumHeight);
			menu.transform.position = menuPosition;
		}

		// 메뉴가 플레이어를 바라보도록 설정
		menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
		menu.transform.forward *= -1;

		if (backgroundMusicManager != null)
		{
			backgroundMusicManager.SetMainVolume(MainvolumeSlider.value);
		}
	}

	public void OnMainVolumeSliderChanged(float value)
	{
		// 메인 볼륨 조절 로직
		if (backgroundMusicManager != null)
		{
			backgroundMusicManager.audioSource.volume = value;
			backgroundMusicManager.defaultBGMSource.volume = value;
		}
	}

}