using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;

public class RecenterOrigin : MonoBehaviour
{
    public Transform head;
    public Transform origin;
    public Transform target;

	public WristStat wristStat;

	public InputActionProperty recenterButton;

	public GameObject fadeScreenobj;

	public void Recenter()
    {
		{
			// 기존 Recenter 로직
			Vector3 offset = head.position - origin.position;
			offset.y = 0;
			origin.position = target.position - offset;

			Vector3 targetForward = target.forward;
			targetForward.y = 0;
			Vector3 cameraForward = head.forward;
			cameraForward.y = 0;

			float angle = Vector3.SignedAngle(cameraForward, targetForward, Vector3.up);

			origin.RotateAround(head.position, Vector3.up, angle);

			if (wristStat.isAlreadyDead)
			{
				wristStat.hp = 30; // HP를 30으로 회복
				wristStat.isAlreadyDead = false; // 죽은 상태 해제
				fadeScreenobj.SetActive(false);
			}
		}
	}
	/*public void Recenter()
    {
		if (wristStat.isAlreadyDead)
		{
			wristStat.hp = 30; // HP를 30으로 회복
			wristStat.isAlreadyDead = false; // 죽은 상태 해제
			fadeScreenobj.SetActive(false);
		}
		else
		{
			// 기존 Recenter 로직
			Vector3 offset = head.position - origin.position;
			offset.y = 0;
			origin.position = target.position - offset;

			Vector3 targetForward = target.forward;
			targetForward.y = 0;
			Vector3 cameraForward = head.forward;
			cameraForward.y = 0;

			float angle = Vector3.SignedAngle(cameraForward, targetForward, Vector3.up);

			origin.RotateAround(head.position, Vector3.up, angle);
		}
	}*/

	// Update is called once per frame
	void Update()
    {
        if(recenterButton.action.WasPressedThisFrame())
        {
            Recenter();
        }
    }
}
