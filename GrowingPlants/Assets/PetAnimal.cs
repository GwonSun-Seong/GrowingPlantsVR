using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Polyperfect.Animals;

public class PetAnimal : MonoBehaviour
{
	public Transform target; // 따라갈 대상 (예: 플레이어)
	private NavMeshAgent nav; // NavMeshAgent 컴포넌트
	private Animator animator; // Animator 컴포넌트
	private AudioSource audioSource;

	private float followOffset = 5f; // 따라갈 때의 거리 오프셋
	private float stopDistance; // 멈출 거리

	void Start()
	{
		nav = GetComponent<NavMeshAgent>(); // NavMeshAgent 컴포넌트를 가져옴
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트를 가져옴

		SetRandomStopDistance();
		nav.stoppingDistance = 5f; // 멈추는 거리 설정
		StartCoroutine(PlaySoundRoutine());
	}

	void Update()
	{
		if (target != null)
		{
			float distanceToTarget = Vector3.Distance(transform.position, target.position);

			if (distanceToTarget > stopDistance)
			{
				nav.isStopped = false;
				FaceTarget();

				if (distanceToTarget > followOffset)
				{
					nav.speed = 4f; // 뛰는 속도
					animator.SetBool("isRunning", true);
					animator.SetBool("isWalking", false);
				}
				else
				{
					nav.speed = 2f; // 걷는 속도
					animator.SetBool("isRunning", false);
					animator.SetBool("isWalking", true);
				}

				Vector3 followTarget = target.position - (target.forward * followOffset);
				nav.SetDestination(followTarget);
			}
			else
			{
				nav.isStopped = true;
				animator.SetBool("isRunning", false);
				animator.SetBool("isWalking", false);
				SetRandomStopDistance();
			}
		}
	}

	IEnumerator PlaySoundRoutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(30f, 60f));

			if (target != null && Vector3.Distance(transform.position, target.position) <= 5f)
			{
				audioSource.pitch = Random.Range(0.8f, 1.2f);
				audioSource.Play();
			}
		}
	}

	private void SetRandomStopDistance()
	{
		stopDistance = Random.Range(2f, 8f);
	}

	private void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Polyperfect.Animals;

public class PetAnimal : MonoBehaviour
{
	public Transform target; // 따라갈 대상 (예: 플레이어)
	private NavMeshAgent nav; // NavMeshAgent 컴포넌트
	private Animator animator; // Animator 컴포넌트
	private AudioSource audioSource;

	private float followOffset = 5f;
	private float stopDistance;

	void Start()
	{
		nav = GetComponent<NavMeshAgent>(); // NavMeshAgent 컴포넌트를 가져옴
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트를 가져옴

		SetRandomStopDistance();
		StartCoroutine(PlaySoundRoutine());

	}

	void Update()
	{
		if (target != null)
		{
			float distanceToTarget = Vector3.Distance(transform.position, target.position);

			// 플레이어와의 거리에 따라 동물의 이동을 제어
			if (distanceToTarget > stopDistance)
			{
				nav.isStopped = false; // 이동 재개
				FaceTarget();

				if (distanceToTarget > 50f)
				{
					nav.speed = 6f; // 뛰는 속도
					animator.SetBool("isRunning", true);
					animator.SetBool("isWalking", false);
				}
				else
				{
					nav.speed = 2f; // 걷는 속도
					animator.SetBool("isRunning", false);
					animator.SetBool("isWalking", true);
				}

				Vector3 followTarget = target.position - (target.forward * followOffset);
				nav.SetDestination(followTarget); // 플레이어 뒤에 오프셋을 두고 따라감
			}
			else
			{
				// 플레이어와 너무 가까우면 멈춤
				nav.isStopped = true;
				animator.SetBool("isRunning", false);
				animator.SetBool("isWalking", false);

				SetRandomStopDistance();
			}
		}
	}

	IEnumerator PlaySoundRoutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(30f, 60f)); // 30초에서 1분 사이의 랜덤 시간 기다림

			if (target != null && Vector3.Distance(transform.position, target.position) <= 5f)
			{
				// 플레이어와 가까이 있을 때만 소리 재생
				audioSource.pitch = Random.Range(0.8f, 1.2f);
				audioSource.Play();
			}
		}
	}
	private void SetRandomStopDistance()
	{
		stopDistance = Random.Range(2f, 8f); // 3에서 5 사이의 랜덤 거리 설정
	}
	private void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}
}*/

