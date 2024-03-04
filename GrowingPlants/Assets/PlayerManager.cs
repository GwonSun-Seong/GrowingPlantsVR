using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerManager : MonoBehaviour
{
	// Start is called before the first frame update
	private PlayerData playerData = new PlayerData();
	public WristStat wristStat;
	public Transform playerTransform;
	public ParticleSystem birdsparticle;

	void Start()
	{
		if (ES3.KeyExists("playerData"))
		{
			playerData = ES3.Load<PlayerData>("playerData");
			// �ε�� �����͸� ����Ͽ� ���� ���¸� �ʱ�ȭ
			wristStat.stamina = playerData.stamina;
			wristStat.hp = playerData.hp;
			wristStat.money = playerData.money;
			// wristStat.money = playerData.money; // 'money'�� WristStat�� �ִٸ�

			if (Physics.Raycast(playerData.position, Vector3.down, out RaycastHit hit))
			{
				// �������� �浹 ������ �÷��̾� ��ġ ����
				playerTransform.position = hit.point;
			}
			else
			{
				// Raycast�� ������ �浹���� �ʴ� ���, �⺻ ��ġ ���
				playerTransform.position = playerData.position;
			}
			playerTransform.rotation = playerData.rotation;
		}
	}
	void Update()
	{
		// ����: ���� ������ ���� ���׹̳�, HP, ���� ������Ʈ
		playerData.stamina = wristStat.stamina;// ���� ���׹̳� ��;
		playerData.hp = wristStat.hp;// ���� HP ��;
		playerData.money = wristStat.money;// ���� �� ��;

		playerData.position = playerTransform.position;
		playerData.rotation = playerTransform.rotation;

		if (playerData.hp <= 10)
		{
			// ��ƼŬ�� ��ġ�� �÷��̾� ��ġ�� Y�� + 30���� ����
			birdsparticle.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + 30, playerTransform.position.z);

			// ��ƼŬ�� ���� ��� ������ �ʴٸ� ���
			if (!birdsparticle.isPlaying)
			{
				birdsparticle.Play();
			}
		}
		else { birdsparticle.Stop(); }
	}
	void OnApplicationQuit()
	{
		try
		{
			ES3.Save("playerData", playerData);
		}
		catch (Exception e)
		{
			Debug.Log(e);
		}
	}
}
