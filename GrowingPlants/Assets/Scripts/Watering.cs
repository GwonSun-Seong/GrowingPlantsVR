using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Watering : MonoBehaviour
{
	public AudioClip watering;
	private AudioSource audioSource;
    public GameObject waterEffectPrefab; // 파란색 스피어 프리팹
	public int numberOfSpheres = 10; // 생성할 스피어의 개수
	public float sphereSpawnInterval = 0.1f; // 스피어 생성 간격
	private int activeSphereCount = 0;

	private bool isWatering = false;
	private float mintiltThreshold = 20f; // 기울임 감지 임계값
	private float maxtiltThreshold = 170f;

	private XRBaseController currentController;

	public void SetController(XRBaseController controller)
	{
		currentController = controller;
	}

	void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = watering;
        audioSource.loop = true;
    }
    void Update()
	{
		// 물뿌리개의 현재 로테이션 값을 얻어옴
		float currentRotation = transform.rotation.eulerAngles.x;

        // 기울임 감지
        if (currentRotation > mintiltThreshold && currentRotation < maxtiltThreshold && !isWatering)
		{
			StartCoroutine(StartWaterEffect());
        }
        else if (currentRotation <= mintiltThreshold && isWatering)
		{
			StopWaterEffect();
		}
	}

	IEnumerator StartWaterEffect()
    {
        audioSource.PlayOneShot(watering);
        isWatering = true;

		if (isWatering && currentController != null)
		{
			// 진동 로직
			currentController.SendHapticImpulse(0.5f, 0.1f);
		}


		// 여러 개의 파란색 스피어 생성
		for (int i = 0; i < numberOfSpheres; i++)
		{
			activeSphereCount += numberOfSpheres;
			GameObject sphere = Instantiate(waterEffectPrefab, transform.position, Quaternion.identity);
			sphere.SetActive(true);
			StartCoroutine(DestroySphereAfterDelay(sphere, 0.4f));

			Collider sphereCollider = sphere.GetComponent<Collider>();
			if (sphereCollider != null)
			{
				sphereCollider.enabled = true;
			}

			yield return new WaitForSeconds(sphereSpawnInterval);

			
		}
		isWatering = false;
	}
	IEnumerator DestroySphereAfterDelay(GameObject sphere, float delay)
	{
		yield return new WaitForSeconds(delay);
		Destroy(sphere);
		activeSphereCount--;

		if (activeSphereCount == 0 && isWatering)
		{
			StopWaterEffect();
		}
	}
	IEnumerator StopAudioSourceAfterDelay(AudioSource source, float delay)
	{
		yield return new WaitForSeconds(delay);
		source.Stop();
	}

	void StopWaterEffect()
	{
		isWatering = false;
		StartCoroutine(StopAudioSourceAfterDelay(audioSource, 1.0f));
	}
}
