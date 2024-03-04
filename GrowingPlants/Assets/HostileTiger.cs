using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation.Samples;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HostileTiger : MonoBehaviour
{
	public Transform player; // 플레이어 위치, 메인카메라를 여기에 할당
	public WristStat playerStats; // 플레이어의 상태 스크립트
	public PlayerGetHit playerGetHit;
	private NavMeshAgent agent;
	private Animator animator;

	public float detectionRange = 30f; // 플레이어 감지 범위
	public float attackRange = 3.5f; // 공격 범위
	private float attackCooldown = 1.5f; // 공격 쿨다운 시간
	private float lastAttackTime; // 마지막 공격 시간

	private float health = 200f; // 호랑이의 체력
	private bool hasReachedDestination = false; // 목적지 도달 여부
	private bool isAttacking = false; // 공격 중인지 여부
	private bool isRandomWalking = false;

	public Slider healthSlider; // 체력 슬라이더
	public GameObject healthPanel; // 체력 패널

	public AudioClip[] playerHitSounds; // 플레이어 피격음 클립
	private AudioSource audioSource;

	public float walkSpeed = 2.5f;  // 걷기 속도
	public float chaseSpeed = 5.0f; // 추격 속도
	public float desiredDistance = 1f;

	private AnimalSpawnManager spawnManager;
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		StartCoroutine(RandomWalk());

		healthSlider.maxValue = 200; // 슬라이더의 최대값을 100으로 설정
		healthSlider.value = health; // 현재 체력으로 슬라이더 초기화

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
			agent.speed = walkSpeed; // 원래 속도로 복귀
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
			if (playerStats.isAlreadyDead) // 플레이어가 죽었는지 확인
			{
				StopAttacking(); // 플레이어가 죽었다면 공격 중지
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
		isRandomWalking = false; // 여기
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
		agent.isStopped = false; // 이동 재개
		RandomWalk();
	}
	public void GetHitAndChasePlayer()
	{
		if (!playerStats.isAlreadyDead)
		{
			StopRandomWalk(); // 기존 랜덤 워크 중지
			ChasePlayer(); // 플레이어 추격 시작
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
					agent.speed = walkSpeed; // 이동 속도 설정
					animator.SetBool("isWalking", true);
					hasReachedDestination = true;
				}
			}

			// 에이전트가 목적지에 도달했는지 확인
			if (hasReachedDestination && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
			{
				// 목적지에 도달하면 애니메이션 중지 및 대기
				animator.SetBool("isWalking", false);
				agent.speed = 0; // 멈춤
				yield return new WaitForSeconds(2f); // 2초 동안 대기
				hasReachedDestination = false; // 다음 랜덤 위치로 이동하기 위해 플래그 재설정
			}

			yield return null; // 다음 프레임까지 대기
		}
	}


	IEnumerator PerformAttack()
	{
		isAttacking = true;
		lastAttackTime = Time.time;
		animator.SetBool("isRunning", false);
		animator.SetBool("isWalking", false);
		animator.SetBool("isAttacking", true);

		yield return new WaitForSeconds(1f); // 가정된 공격 애니메이션 시간


		if (playerStats.isAlreadyDead) // 플레이어가 죽었는지 확인
		{
			StopAttacking(); // 플레이어가 죽었다면 공격 중지
			yield break; // 코루틴 종료
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
		//Destroy(gameObject, 4f); // 사망 애니메이션이 2초라고 가정

		spawnManager.ReturnObjectToPool(gameObject);
	}

	public void ResetState()
	{
		health = 200f; // 초기 체력으로 리셋
		isAttacking = false; // 공격 상태 초기화
		animator.ResetTrigger("isDead"); // 사망 애니메이션 트리거 초기화
										 // 기타 필요한 상태 초기화 로직 추가...
	}
}