using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMesh Pro 네임스페이스 추가

public class Dialog : MonoBehaviour
{
	public Transform playerCamera;
	public GameObject dialoguePanel;
	public TextMeshProUGUI dialogueText; // TMPro 텍스트 컴포넌트
	public Vector3 panelOffset;

	void Start()
	{
		HideDialog(); // 시작 시 다이얼로그 숨기기
	}
	void Update()
	{
		if (dialoguePanel.activeSelf) // 다이얼로그가 활성화되어 있을 때만
		{
			// 패널의 위치를 업데이트
			dialoguePanel.transform.position = playerCamera.position + playerCamera.forward * panelOffset.z + playerCamera.right * panelOffset.x + playerCamera.up * panelOffset.y;

			// 패널을 카메라를 향하도록 하되, Y축 회전만 고려
			Vector3 targetPosition = new Vector3(playerCamera.position.x, dialoguePanel.transform.position.y, playerCamera.position.z);
			dialoguePanel.transform.LookAt(targetPosition);
			dialoguePanel.transform.Rotate(0, 180, 0); // 180도 회전하여 글씨가 정방향이 되도록 조정
		}
	}
	// 다이얼로그 표시
	public void ShowDialog()
	{
		dialoguePanel.SetActive(true);
	}

	// 다이얼로그 숨기기
	public void HideDialog()
	{
		dialoguePanel.SetActive(false);
	}

	// 텍스트 설정
	public void SetText(string text)
	{
		dialogueText.text = text;
	}

	// 다른 메소드에서 이벤트에 따라 ShowDialog를 호출할 수 있습니다.
	// 예: 특정 트리거에 들어갈 때, 아이템을 선택할 때 등
}
