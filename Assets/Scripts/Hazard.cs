using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; // Required in C#


public class Hazard : MonoBehaviour {
	public int direction;
	public void InitiateHazardEffect(GameObject player, PlayerIndex playerIndex)
	{
		GamePad.SetVibration (playerIndex, 0, 0);
		player.transform.localRotation *= Quaternion.Euler(0.0f, .5f*direction * 50.0f * Time.deltaTime, 0.0f);
	}
}
