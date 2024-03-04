using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bullet : MonoBehaviour
{
	public TextMeshProUGUI scoreText;
	public GameObject hitSpritePrefab; // 스프라이트 프리팹
	public float spriteScale = 0.05f; // 스프라이트의 크기
	public static int damage = 10;

	// TextMeshProUGUI 객체를 설정하는 메서드
	public void SetScoreText(TextMeshProUGUI text)
	{
		scoreText = text;
	}

	void OnCollisionEnter(Collision collision)
	{
		int score = 0;

		// 과녁의 부분에 따라 점수 계산
		if (collision.gameObject.CompareTag("TargetCenter") || collision.gameObject.CompareTag("TargetMid") || collision.gameObject.CompareTag("TargetOuter"))
		{
			// 충돌 지점에 스프라이트 생성 및 크기 조정
			ContactPoint contact = collision.contacts[0];
			Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal) * Quaternion.Euler(90, 0, 0); // Y축으로 90도 회전 추가
			GameObject spriteInstance = Instantiate(hitSpritePrefab, contact.point, rotation);
			spriteInstance.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

			// 점수 계산
			if (collision.gameObject.CompareTag("TargetCenter"))
				score = 10;
			else if (collision.gameObject.CompareTag("TargetMid"))
				score = 5;
			else if (collision.gameObject.CompareTag("TargetOuter"))
				score = 3;

			// 점수 표시
			if (scoreText != null)
				scoreText.text = "Score: " + score;

			Destroy(gameObject);
		}
		else if (collision.gameObject.CompareTag("Enemy"))
		{
			var enemyBoar = collision.gameObject.GetComponent<HostileBoar>();
			var enemyTiger = collision.gameObject.GetComponent<HostileTiger>();
			var enemyWolf = collision.gameObject.GetComponent<HostileWolf>();

			if (enemyBoar != null)
			{
				enemyBoar.TakeDamage(damage);
				enemyBoar.GetHitAndChasePlayer();
			}
			else if (enemyTiger != null)
			{
				enemyTiger.TakeDamage(damage);
				enemyTiger.GetHitAndChasePlayer();
			}
			else if (enemyWolf != null)
			{
				enemyWolf.TakeDamage(damage);
				enemyWolf.GetHitAndChasePlayer();
			}
		}
		else if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Pet"))
		{
			Destroy(gameObject);
		}

	}
}
