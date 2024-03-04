using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ButtonVR : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    AudioSource sound;
    bool isPressed;
    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;

		// 총알 자국 제거 이벤트에 RemoveBulletHoles 메소드 추가
		onPress.AddListener(RemoveBulletHoles);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!isPressed)
		{
			button.transform.localPosition = new Vector3(-0.442f, 0.003f, 0.534f);
			presser = other.gameObject;
			onPress.Invoke(); // 버튼 눌림 이벤트 호출
			sound.Play();
			isPressed = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject == presser)
		{
			button.transform.localPosition = new Vector3(-0.442f, 0.015f, 0.534f);
			onRelease.Invoke(); // 버튼 떼어짐 이벤트 호출
			isPressed = false;
		}
	}

	// 총알 자국을 제거하는 메소드
	public void RemoveBulletHoles()
	{
		GameObject[] bulletHoles = GameObject.FindGameObjectsWithTag("BulletHoleImage");
		foreach (GameObject bulletHole in bulletHoles)
		{
			Destroy(bulletHole); // 오브젝트 삭제
		}
	}
}
