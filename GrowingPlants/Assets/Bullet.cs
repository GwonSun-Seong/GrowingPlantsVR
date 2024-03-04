using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bullet : MonoBehaviour
{
	public TextMeshProUGUI scoreText;
	public GameObject hitSpritePrefab; // ��������Ʈ ������
	public float spriteScale = 0.05f; // ��������Ʈ�� ũ��
	public static int damage = 10;

	// TextMeshProUGUI ��ü�� �����ϴ� �޼���
	public void SetScoreText(TextMeshProUGUI text)
	{
		scoreText = text;
	}

	void OnCollisionEnter(Collision collision)
	{
		int score = 0;

		// ������ �κп� ���� ���� ���
		if (collision.gameObject.CompareTag("TargetCenter") || collision.gameObject.CompareTag("TargetMid") || collision.gameObject.CompareTag("TargetOuter"))
		{
			// �浹 ������ ��������Ʈ ���� �� ũ�� ����
			ContactPoint contact = collision.contacts[0];
			Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal) * Quaternion.Euler(90, 0, 0); // Y������ 90�� ȸ�� �߰�
			GameObject spriteInstance = Instantiate(hitSpritePrefab, contact.point, rotation);
			spriteInstance.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

			// ���� ���
			if (collision.gameObject.CompareTag("TargetCenter"))
				score = 10;
			else if (collision.gameObject.CompareTag("TargetMid"))
				score = 5;
			else if (collision.gameObject.CompareTag("TargetOuter"))
				score = 3;

			// ���� ǥ��
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
