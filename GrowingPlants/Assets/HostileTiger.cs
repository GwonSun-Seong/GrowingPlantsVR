using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation.Samples;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HostileTiger : MonoBehaviour
{
	public Transform player; // �÷��̾� ��ġ, ����ī�޶� ���⿡ �Ҵ�
	public WristStat playerStats; // �÷��̾��� ���� ��ũ��Ʈ
	public PlayerGetHit playerGetHit;
	private NavMeshAgent agent;
	private Animator animator;

	public float detectionRange = 30f; // �÷��̾� ���� ����
	public float attackRange = 3.5f; // ���� ����
	private float attackCooldown = 1.5f; // ���� ��ٿ� �ð�
	private float lastAttackTime; // ������ ���� �ð�

	private float health = 200f; // ȣ������ ü��
	private bool hasReachedDestination = false; // ������ ���� ����
	private bool isAttacking = false; // ���� ������ ����
	private bool isRandomWalking = false;

	public Slider healthSlider; // ü�� �����̴�
	public GameObject healthPanel; // ü�� �г�

	public AudioClip[] playerHitSounds; // �÷��̾� �ǰ��� Ŭ��
	private AudioSource audioSource;

	public float walkSpeed = 2.5f;  // �ȱ� �ӵ�
	public float chaseSpeed = 5.0f; // �߰� �ӵ�
	public float desiredDistance = 1f;

	private AnimalSpawnManager spawnManager;
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		StartCoroutine(RandomWalk());

		healthSlider.maxValue = 200; // �����̴��� �ִ밪�� 100���� ����
		healthSlider.value = health; // ���� ü������ �����̴� �ʱ�ȭ

		audioSource = GetComponent<AudioSource>();

		spawnManager = FindObjectOfType<AnimalSpawnManager>();
	}
	void Update()
	{
		if (health > 0)
		{	
			float distanceToPlayer = Vector3.Distance(player.position, transform.position);

			if (distanceToPlayer > detectionRange && !isRandomWalking)
			{
				isRandomWalking = true;
				StartCoroutine(RandomWalk());
			}
			else if (distanceToPlayer <= detectionRange && distanceToPlayer > attackRange && !playerStats.isAlreadyDead)
			{
				StopRandomWalk();
				ChasePlayer();
			}
			else if (distanceToPlayer <= attackRange && !playerStats.isAlreadyDead)
			{
				StopRandomWalk();
				AttackPlayer();
			}

			UpdateHealthPanel();
		}
		else { Die(); AudioManager.Instance.StopChase(); }
	}
	/*void Update()
	{
		if (health > 0)
		{
			//if(playerStats.isAlreadyDead)
			float distanceToPlayer = Vector3.Distance(player.position, transform.position);

			if (distanceToPlayer > detectionRange && !isRandomWalking)
			{
				isRandomWalking = true;
				StartCoroutine(RandomWalk());
			}
			else if (distanceToPlayer <= detectionRange && distanceToPlayer > attackRange)
			{
				StopRandomWalk();
				ChasePlayer();
			}
			else if (distanceToPlayer <= attackRange)
			{
				StopRandomWalk();
				AttackPlayer();
			}

			UpdateHealthPanel();
		}
		else { Die(); AudioManager.Instance.StopChase(); }
	}*/
	void StopRandomWalk()
	{
		if (isRandomWalking)
		{
			StopCoroutine(RandomWalk());
			isRandomWalking = false;
			animator.SetBool("isWalking", false);
			agent.speed = walkSpeed; // ���� �ӵ��� ����
		}
	}
	void UpdateHealthPanel()
	{
		healthPanel.SetActive(health < 200);
		healthPanel.transform.LookAt(player.position);
	}
	void AttackPlayer()
	{
		if (!isAttacking && Time.time >= lastAttackTime + attackCooldown)
		{
			if (playerStats.isAlreadyDead) // �÷��̾ �׾����� Ȯ��
			{
				StopAttacking(); // �÷��̾ �׾��ٸ� ���� ����
			}
			else
			{
				StartCoroutine(PerformAttack());
			}
		}
	}

	public void ChasePlayer()
	{
		AudioManager.Instance.StartChase();
		isRandomWalking = false; // ����
		agent.isStopped = false;
		agent.speed = chaseSpeed;

		Vector3 directionToPlayer = (player.position - transform.position).normalized;
		Vector3 targetPosition = player.position - directionToPlayer * desiredDistance;
		agent.SetDestination(targetPosition);
		animator.SetBool("isRunning", true);
	}
	void StopAttacking()
	{
		isAttacking = false;
		animator.SetBool("isAttacking", false);
		animator.SetBool("isRunning", false);
		agent.isStopped = false; // �̵� �簳
		RandomWalk();
	}
	public void GetHitAndChasePlayer()
	{
		if (!playerStats.isAlreadyDead)
		{
			StopRandomWalk(); // ���� ���� ��ũ ����
			ChasePlayer(); // �÷��̾� �߰� ����
		}
		else { RandomWalk();}
   
	}

	IEnumerator RandomWalk()
	{
		animator.SetBool("isAttacking", false);
		animator.SetBool("isRunning", false);
		while (true)
		{
			if (!hasReachedDestination)
			{
				Vector3 randomDirection = Random.insideUnitSphere * 5f;
				randomDirection += transform.position;
				NavMeshHit hit;

				if (NavMesh.SamplePosition(randomDirection, out hit, 5f, NavMesh.AllAreas))
				{
					agent.SetDestination(hit.position);
					agent.speed = walkSpeed; // �̵� �ӵ� ����
					animator.SetBool("isWalking", true);
					hasReachedDestination = true;
				}
			}

			// ������Ʈ�� �������� �����ߴ��� Ȯ��
			if (hasReachedDestination && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
			{
				// �������� �����ϸ� �ִϸ��̼� ���� �� ���
				animator.SetBool("isWalking", false);
				agent.speed = 0; // ����
				yield return new WaitForSeconds(2f); // 2�� ���� ���
				hasReachedDestination = false; // ���� ���� ��ġ�� �̵��ϱ� ���� �÷��� �缳��
			}

			yield return null; // ���� �����ӱ��� ���
		}
	}


	IEnumerator PerformAttack()
	{
		isAttacking = true;
		lastAttackTime = Time.time;
		animator.SetBool("isRunning", false);
		animator.SetBool("isWalking", false);
		animator.SetBool("isAttacking", true);

		yield return new WaitForSeconds(1f); // ������ ���� �ִϸ��̼� �ð�


		if (playerStats.isAlreadyDead) // �÷��̾ �׾����� Ȯ��
		{
			StopAttacking(); // �÷��̾ �׾��ٸ� ���� ����
			yield break; // �ڷ�ƾ ����
		}


		if (Vector3.Distance(player.position, transform.position) <= attackRange)
		{
			if (!playerStats.isAlreadyDead && playerStats.hp > 0)
			{
				playerStats.hp -= 35;
				PlayRandomPlayerHitSound();
			}
		}

		yield return new WaitForSeconds(attackCooldown);
		isAttacking = false;
	}

	void PlayRandomPlayerHitSound()
	{
		if (playerHitSounds.Length > 0)
		{
			int randomIndex = Random.Range(0, playerHitSounds.Length);
			audioSource.PlayOneShot(playerHitSounds[randomIndex]);
		}
	}


	public void TakeDamage(int damage)
	{
		if (!playerStats.isAlreadyDead)
		{
			health -= damage;
			healthSlider.value = health;
		}
	}

	void Die()
	{
		agent.isStopped = true;
		animator.SetBool("isRunning", false);
		animator.SetBool("isWalking", false);
		animator.SetBool("isAttacking", false);
		animator.SetTrigger("isDead");
		//Destroy(gameObject, 4f); // ��� �ִϸ��̼��� 2�ʶ�� ����

		spawnManager.ReturnObjectToPool(gameObject);
	}

	public void ResetState()
	{
		health = 200f; // �ʱ� ü������ ����
		isAttacking = false; // ���� ���� �ʱ�ȭ
		animator.ResetTrigger("isDead"); // ��� �ִϸ��̼� Ʈ���� �ʱ�ȭ
										 // ��Ÿ �ʿ��� ���� �ʱ�ȭ ���� �߰�...
	}
}