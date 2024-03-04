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
			// 로드된 데이터를 사용하여 게임 상태를 초기화
			wristStat.stamina = playerData.stamina;
			wristStat.hp = playerData.hp;
			wristStat.money = playerData.money;
			// wristStat.money = playerData.money; // 'money'가 WristStat에 있다면

			if (Physics.Raycast(playerData.position, Vector3.down, out RaycastHit hit))
			{
				// 지형과의 충돌 지점에 플레이어 위치 조정
				playerTransform.position = hit.point;
			}
			else
			{
				// Raycast가 지형과 충돌하지 않는 경우, 기본 위치 사용
				playerTransform.position = playerData.position;
			}
			playerTransform.rotation = playerData.rotation;
		}
	}
	void Update()
	{
		// 예시: 게임 로직에 따라 스테미나, HP, 돈을 업데이트
		playerData.stamina = wristStat.stamina;// 현재 스테미나 값;
		playerData.hp = wristStat.hp;// 현재 HP 값;
		playerData.money = wristStat.money;// 현재 돈 값;

		playerData.position = playerTransform.position;
		playerData.rotation = playerTransform.rotation;

		if (playerData.hp <= 10)
		{
			// 파티클의 위치를 플레이어 위치의 Y값 + 30으로 설정
			birdsparticle.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + 30, playerTransform.position.z);

			// 파티클이 아직 재생 중이지 않다면 재생
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
