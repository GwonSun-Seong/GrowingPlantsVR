using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class HostileAnimal : MonoBehaviour
{
	public enum AnimalType { Boar, Tiger, Wolf }
	public AnimalType animalType;

	public GameObject[] animalPrefabs;
	public Transform[] spawnPoints;
	public Transform playerpos;
	public float spawnInterval = 20f;

	private GameObject currentAnimal;
	private float health;
	public WristStat playerStats;
	private NavMeshAgent agent;
	private Animator animator;

	private float detectionRange = 20f;
	private float attackRange = 1f;
	private Vector3 currentDestination;

	private bool isDead = false;
	private bool isAttacking = false;

	void Start()
	{
		StartCoroutine(SpawnAnimalsPeriodically());
	}

	void Update()
	{
		if (isDead || currentAnimal == null) return;

		float distanceToPlayer = Vector3.Distance(playerpos.position, transform.position);

		if (distanceToPlayer <= attackRange && !isAttacking)
		{
			StartCoroutine(AttackPlayer());
		}
		else if (distanceToPlayer <= detectionRange)
		{
			ChasePlayer();
		}
		else
		{
			WanderRandomly();
		}
	}

	void SpawnAnimal()
	{
		if (spawnPoints.Length == 0)
		{
			Debug.LogError("Spawn points not set in the inspector");
			return;
		}

		Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

		GameObject prefabToSpawn = null;
		switch (animalType)
		{
			case AnimalType.Boar:
				prefabToSpawn = animalPrefabs[0]; // Boar prefab
				health = 100f;
				break;
			case AnimalType.Tiger:
				prefabToSpawn = animalPrefabs[1]; // Tiger prefab
				health = 150f;
				break;
			case AnimalType.Wolf:
				prefabToSpawn = animalPrefabs[2]; // Wolf prefab
				health = 70f;
				break;
		}

		currentAnimal = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
		agent = currentAnimal.GetComponent<NavMeshAgent>();
		animator = currentAnimal.GetComponent<Animator>();
	}
	IEnumerator SpawnAnimalsPeriodically()
	{
		while (true)
		{
			yield return new WaitForSeconds(spawnInterval);
			SpawnAnimal();
		}
	}
	IEnumerator AttackPlayer()
	{
		isAttacking = true;
		agent.SetDestination(transform.position); // Stop moving
		animator.SetBool("isRunning", false);
		animator.SetBool("isWalking", false);
		animator.SetBool("isAttacking", true);
		yield return new WaitForSeconds(1f); // Assuming attack animation is 1 second long

		// Damage the player if close enough
		if (Vector3.Distance(playerpos.position, transform.position) <= attackRange)
		{
			playerStats.hp -= 30;
		}

		animator.SetBool("isAttacking", false);
		isAttacking = false;
	}

	void ChasePlayer()
	{
		if (currentAnimal != null && agent != null && !isAttacking)
		{
			agent.SetDestination(playerpos.position);
			currentDestination = playerpos.position;
			animator.SetBool("isRunning", true);
			animator.SetBool("isWalking", false);
			animator.SetBool("isAttacking", false);
		}
	}

	void WanderRandomly()
	{
		if (currentAnimal != null && agent != null && !isAttacking)
		{
			Vector3 randomDirection = Random.insideUnitSphere * detectionRange;
			randomDirection += transform.position;
			NavMeshHit hit;
			NavMesh.SamplePosition(randomDirection, out hit, detectionRange, 1);
			Vector3 finalPosition = hit.position;

			agent.SetDestination(finalPosition);
			currentDestination = finalPosition;
			animator.SetBool("isRunning", false);
			animator.SetBool("isWalking", true);
			animator.SetBool("isAttacking", false);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Bullet") && !isDead)
		{
			health -= 30f;
			if (health <= 0)
			{
				Die();
			}
		}
	}

	void Die()
	{
		isDead = true;
		animator.SetBool("isDead", true);
		Destroy(gameObject, 2f); // Assuming the death animation is 5 seconds long
	}
}