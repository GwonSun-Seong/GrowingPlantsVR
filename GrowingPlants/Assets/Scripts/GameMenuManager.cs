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
	public GameObject dogObject; // ������ ������Ʈ
	public GameObject catObject; // ����� ������Ʈ
	public float spawnDistance = 2;
	public float minimumHeight = 1.5f; // �޴��� �ּ� ���� ����


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

		// ��Ӵٿ� �ɼ� �ʱ�ȭ �� ����
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
		// ��� �ֿϵ��� ��Ȱ��ȭ
		dogObject.SetActive(false);
		catObject.SetActive(false);

		// ���õ� �ֿϵ��� Ȱ��ȭ
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

			// �÷��̾� �Ӹ� ��ġ�� �������� �޴� ��ġ ����
			Vector3 menuPosition = head.position + head.forward.normalized * spawnDistance;
			// �޴��� ���麸�� �ʹ� ���� �ʵ��� y ��ǥ ����
			menuPosition.y = Mathf.Max(menuPosition.y, minimumHeight);
			menu.transform.position = menuPosition;
		}

		// �޴��� �÷��̾ �ٶ󺸵��� ����
		menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
		menu.transform.forward *= -1;

		if (backgroundMusicManager != null)
		{
			backgroundMusicManager.SetMainVolume(MainvolumeSlider.value);
		}
	}

	public void OnMainVolumeSliderChanged(float value)
	{
		// ���� ���� ���� ����
		if (backgroundMusicManager != null)
		{
			backgroundMusicManager.audioSource.volume = value;
			backgroundMusicManager.defaultBGMSource.volume = value;
		}
	}

}