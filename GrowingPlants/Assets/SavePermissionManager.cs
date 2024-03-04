using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class SavePermissionManager : MonoBehaviour
{
	void Start()
	{
		if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
		{
			Permission.RequestUserPermission(Permission.ExternalStorageRead);
		}
	}
}
