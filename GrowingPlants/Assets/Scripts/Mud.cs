using System.Collections;
using UnityEngine;

public class Mud : MonoBehaviour
{
    public AudioClip Hoeing; // ȣ�� �Ҹ�
    private AudioSource audioSource; // AudioSource ������Ʈ
    public GameObject MudPrefab; // ������� ������
	public GameObject WeedPrefab; // ���� ������
	public float spawnCooldown = 2f; // ���� ��ٿ� �ð�
	private bool canSpawn = true;
	public float checkRadius = 0.75f; // ������� �˻� ����
	private float weedSpawnTimer;
	public float weedSpawnArea = 1.0f; // ���ʰ� ������ �� �ִ� ����
	public float weedCheckRadius = 0.3f; // ���� ���� ��ġ �˻� ����
	public ParticleSystem mudParticle; // ��ƼŬ �ý��� ����

	private void Start()
	{
		// ó�� ������ �� Ÿ�̸Ӹ� 30�ʿ��� 90�� ������ ������ ������ ����
		weedSpawnTimer = Random.Range(45f, 120f);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Hoeing;
    }

	private void Update()
	{
		weedSpawnTimer -= Time.deltaTime;
		if (weedSpawnTimer <= 0f)
		{
			TrySpawnWeed();
			weedSpawnTimer = Random.Range(45f, 120f);
		}
	}

	void TrySpawnWeed()
	{
		Vector3 weedPosition = RandomPositionAroundMud();
		if (!IsDirtNearby(weedPosition))
		{
			weedPosition.y = 0.25f; // y��ǥ ����
			GameObject weed = Instantiate(WeedPrefab, weedPosition, Quaternion.identity);

			float randomScale = Random.Range(0.5f, 1.5f); // ����: �ּ� 0.5�迡�� �ִ� 1.5�� ũ��
			weed.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
		}
	}

	bool IsDirtNearby(Vector3 position)
	{
		Collider[] hitColliders = Physics.OverlapSphere(position, weedCheckRadius);
		foreach (var hitCollider in hitColliders)
		{
			if (hitCollider.gameObject.CompareTag("Dirt"))
			{
				return true; // �ֺ��� Dirt�� ������ true ��ȯ
			}
		}
		return false; // �ֺ��� Dirt�� ������ false ��ȯ
	}

	Vector3 RandomPositionAroundMud()
	{
		Vector3 basePosition = transform.position;
		float randomX = basePosition.x + Random.Range(-weedSpawnArea, weedSpawnArea);
		float randomZ = basePosition.z + Random.Range(-weedSpawnArea, weedSpawnArea);
		return new Vector3(randomX, basePosition.y, randomZ);
	}

	// ... ������ OnTriggerEnter�� SpawnMud �ڷ�ƾ ...



	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Homi") && canSpawn)
		{
			Vector3 spawnPosition = other.ClosestPointOnBounds(transform.position);
			StartCoroutine(SpawnMud(spawnPosition));
		}
	}

	IEnumerator SpawnMud(Vector3 spawnPosition)
	{
		Collider[] hitColliders = Physics.OverlapSphere(spawnPosition, checkRadius);
		foreach (var hitCollider in hitColliders)
		{
			if (hitCollider.gameObject.CompareTag("Dirt"))
			{
				yield break; // �ֺ��� ������̰� ������ ���� �ߴ�
			}
		}

		spawnPosition.y = 0.25f; // y��ǥ�� 0.25�� ����
		canSpawn = false; // �ߺ� ���� ����

		GameObject mudInstance = Instantiate(MudPrefab, spawnPosition, Quaternion.identity);
        audioSource.Play();

        // ��ƼŬ �ý��� Ȱ��ȭ
        if (mudParticle != null)
		{
			mudParticle.transform.position = mudInstance.transform.position; // ��ƼŬ �ý��� ��ġ ����
			mudParticle.Play(); // ��ƼŬ ���
		}

		yield return new WaitForSeconds(spawnCooldown);

		canSpawn = true; // ���� ���� ���·� ����
	}
}
