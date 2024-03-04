using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMesh Pro ���ӽ����̽� �߰�

public class Dialog : MonoBehaviour
{
	public Transform playerCamera;
	public GameObject dialoguePanel;
	public TextMeshProUGUI dialogueText; // TMPro �ؽ�Ʈ ������Ʈ
	public Vector3 panelOffset;

	void Start()
	{
		HideDialog(); // ���� �� ���̾�α� �����
	}
	void Update()
	{
		if (dialoguePanel.activeSelf) // ���̾�αװ� Ȱ��ȭ�Ǿ� ���� ����
		{
			// �г��� ��ġ�� ������Ʈ
			dialoguePanel.transform.position = playerCamera.position + playerCamera.forward * panelOffset.z + playerCamera.right * panelOffset.x + playerCamera.up * panelOffset.y;

			// �г��� ī�޶� ���ϵ��� �ϵ�, Y�� ȸ���� ���
			Vector3 targetPosition = new Vector3(playerCamera.position.x, dialoguePanel.transform.position.y, playerCamera.position.z);
			dialoguePanel.transform.LookAt(targetPosition);
			dialoguePanel.transform.Rotate(0, 180, 0); // 180�� ȸ���Ͽ� �۾��� �������� �ǵ��� ����
		}
	}
	// ���̾�α� ǥ��
	public void ShowDialog()
	{
		dialoguePanel.SetActive(true);
	}

	// ���̾�α� �����
	public void HideDialog()
	{
		dialoguePanel.SetActive(false);
	}

	// �ؽ�Ʈ ����
	public void SetText(string text)
	{
		dialogueText.text = text;
	}

	// �ٸ� �޼ҵ忡�� �̺�Ʈ�� ���� ShowDialog�� ȣ���� �� �ֽ��ϴ�.
	// ��: Ư�� Ʈ���ſ� �� ��, �������� ������ �� ��
}
