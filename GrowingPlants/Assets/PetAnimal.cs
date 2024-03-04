using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Polyperfect.Animals;

public class PetAnimal : MonoBehaviour
{
	public Transform target; // ���� ��� (��: �÷��̾�)
	private NavMeshAgent nav; // NavMeshAgent ������Ʈ
	private Animator animator; // Animator ������Ʈ
	private AudioSource audioSource;

	private float followOffset = 5f; // ���� ���� �Ÿ� ������
	private float stopDistance; // ���� �Ÿ�

	void Start()
	{
		nav = GetComponent<NavMeshAgent>(); // NavMeshAgent ������Ʈ�� ������
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>(); // AudioSource ������Ʈ�� ������

		SetRandomStopDistance();
		nav.stoppingDistance = 5f; // ���ߴ� �Ÿ� ����
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
					nav.speed = 4f; // �ٴ� �ӵ�
					animator.SetBool("isRunning", true);
					animator.SetBool("isWalking", false);
				}
				else
				{
					nav.speed = 2f; // �ȴ� �ӵ�
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
	public Transform target; // ���� ��� (��: �÷��̾�)
	private NavMeshAgent nav; // NavMeshAgent ������Ʈ
	private Animator animator; // Animator ������Ʈ
	private AudioSource audioSource;

	private float followOffset = 5f;
	private float stopDistance;

	void Start()
	{
		nav = GetComponent<NavMeshAgent>(); // NavMeshAgent ������Ʈ�� ������
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>(); // AudioSource ������Ʈ�� ������

		SetRandomStopDistance();
		StartCoroutine(PlaySoundRoutine());

	}

	void Update()
	{
		if (target != null)
		{
			float distanceToTarget = Vector3.Distance(transform.position, target.position);

			// �÷��̾���� �Ÿ��� ���� ������ �̵��� ����
			if (distanceToTarget > stopDistance)
			{
				nav.isStopped = false; // �̵� �簳
				FaceTarget();

				if (distanceToTarget > 50f)
				{
					nav.speed = 6f; // �ٴ� �ӵ�
					animator.SetBool("isRunning", true);
					animator.SetBool("isWalking", false);
				}
				else
				{
					nav.speed = 2f; // �ȴ� �ӵ�
					animator.SetBool("isRunning", false);
					animator.SetBool("isWalking", true);
				}

				Vector3 followTarget = target.position - (target.forward * followOffset);
				nav.SetDestination(followTarget); // �÷��̾� �ڿ� �������� �ΰ� ����
			}
			else
			{
				// �÷��̾�� �ʹ� ������ ����
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
			yield return new WaitForSeconds(Random.Range(30f, 60f)); // 30�ʿ��� 1�� ������ ���� �ð� ��ٸ�

			if (target != null && Vector3.Distance(transform.position, target.position) <= 5f)
			{
				// �÷��̾�� ������ ���� ���� �Ҹ� ���
				audioSource.pitch = Random.Range(0.8f, 1.2f);
				audioSource.Play();
			}
		}
	}
	private void SetRandomStopDistance()
	{
		stopDistance = Random.Range(2f, 8f); // 3���� 5 ������ ���� �Ÿ� ����
	}
	private void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}
}*/

