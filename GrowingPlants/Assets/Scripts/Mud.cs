using System.Collections;
using UnityEngine;

public class Mud : MonoBehaviour
{
    public AudioClip Hoeing; // 호미 소리
    private AudioSource audioSource; // AudioSource 컴포넌트
    public GameObject MudPrefab; // 진흙더미 프리팹
	public GameObject WeedPrefab; // 잡초 프리팹
	public float spawnCooldown = 2f; // 생성 쿨다운 시간
	private bool canSpawn = true;
	public float checkRadius = 0.75f; // 진흙더미 검사 범위
	private float weedSpawnTimer;
	public float weedSpawnArea = 1.0f; // 잡초가 생성될 수 있는 범위
	public float weedCheckRadius = 0.3f; // 잡초 생성 위치 검사 범위
	public ParticleSystem mudParticle; // 파티클 시스템 참조

	private void Start()
	{
		// 처음 시작할 때 타이머를 30초에서 90초 사이의 랜덤한 값으로 설정
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
			weedPosition.y = 0.25f; // y좌표 고정
			GameObject weed = Instantiate(WeedPrefab, weedPosition, Quaternion.identity);

			float randomScale = Random.Range(0.5f, 1.5f); // 예시: 최소 0.5배에서 최대 1.5배 크기
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
				return true; // 주변에 Dirt가 있으면 true 반환
			}
		}
		return false; // 주변에 Dirt가 없으면 false 반환
	}

	Vector3 RandomPositionAroundMud()
	{
		Vector3 basePosition = transform.position;
		float randomX = basePosition.x + Random.Range(-weedSpawnArea, weedSpawnArea);
		float randomZ = basePosition.z + Random.Range(-weedSpawnArea, weedSpawnArea);
		return new Vector3(randomX, basePosition.y, randomZ);
	}

	// ... 기존의 OnTriggerEnter와 SpawnMud 코루틴 ...



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
				yield break; // 주변에 진흙더미가 있으면 생성 중단
			}
		}

		spawnPosition.y = 0.25f; // y좌표를 0.25로 고정
		canSpawn = false; // 중복 생성 방지

		GameObject mudInstance = Instantiate(MudPrefab, spawnPosition, Quaternion.identity);
        audioSource.Play();

        // 파티클 시스템 활성화
        if (mudParticle != null)
		{
			mudParticle.transform.position = mudInstance.transform.position; // 파티클 시스템 위치 설정
			mudParticle.Play(); // 파티클 재생
		}

		yield return new WaitForSeconds(spawnCooldown);

		canSpawn = true; // 생성 가능 상태로 변경
	}
}
