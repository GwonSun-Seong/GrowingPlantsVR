using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Watering : MonoBehaviour
{
	public AudioClip watering;
	private AudioSource audioSource;
    public GameObject waterEffectPrefab; // �Ķ��� ���Ǿ� ������
	public int numberOfSpheres = 10; // ������ ���Ǿ��� ����
	public float sphereSpawnInterval = 0.1f; // ���Ǿ� ���� ����
	private int activeSphereCount = 0;

	private bool isWatering = false;
	private float mintiltThreshold = 20f; // ����� ���� �Ӱ谪
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
		// ���Ѹ����� ���� �����̼� ���� ����
		float currentRotation = transform.rotation.eulerAngles.x;

        // ����� ����
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
			// ���� ����
			currentController.SendHapticImpulse(0.5f, 0.1f);
		}


		// ���� ���� �Ķ��� ���Ǿ� ����
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
