using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalTapir : MonoBehaviour
{
    NavMeshAgent nav;
	public Transform target;

	void Awake()
	{
		nav = GetComponent<NavMeshAgent>();

	}
	// Update is called once per frame
	void Update()
    {
		nav.SetDestination(target.position);
    }
}
