using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterState))]
public class WalkHandler : MonoBehaviour
{
	public float WalkSpeed = 10f;
	private CharacterState state;

	void Start()
	{
		state = GetComponent<CharacterState>();
	}

	public void Walk(float xvcoeff)
	{
		state.xvel = xvcoeff * WalkSpeed;
	}

	public void Stop()
	{
		state.xvel = 0f;
	}
}