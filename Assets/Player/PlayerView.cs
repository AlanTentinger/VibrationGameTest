using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerView : MonoBehaviour
{

	private PlayerStore store;
	private PlayerActionCreator actionCreator;
	private GamePadState gamePadState;
	CharacterController control;


	// Use this for initialization
	void Start ()
	{
		Init ();
	}

	public void Init ()
	{
		store = GetComponent<PlayerStore> ();
		actionCreator = GetComponent<PlayerActionCreator> ();
		control = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// input

		gamePadState = GamePad.GetState (store.playerIndex);

		actionCreator.UpdateGamePadLeftTriggerPressedActionRequested (gamePadState.Triggers.Left);
		actionCreator.UpdateGamePadRightTriggerPressedActionRequested (gamePadState.Triggers.Right);
		actionCreator.UpdateGamePadRightThumbStickMovedActionRequested (gamePadState.ThumbSticks.Right);
		actionCreator.UpdateGamePadLeftThumbStickMovedActionRequested (gamePadState.ThumbSticks.Left);

		Vector3 fwd = transform.TransformDirection (Vector3.forward);
		RaycastHit objectBoxcastHit;
		//if (Physics.BoxCast (transform.position, new Vector3 (transform.localScale.x / 2, transform.localScale.y / 2, transform.localScale.z / 2), fwd, out objectBoxcastHit, Quaternion.identity, store.maxDist))
		//{
		Physics.BoxCast (transform.position, new Vector3 (transform.localScale.x / 2, transform.localScale.y / 2, transform.localScale.z / 2), fwd, out objectBoxcastHit, Quaternion.identity, store.maxDist);
		actionCreator.UpdatePlayerBoxcastHitActionRequested (objectBoxcastHit);
		//}

		// output
		GamePad.SetVibration (store.playerIndex, store.leftVibration, store.rightVibration);
		Debug.Log (store.leftVibration);

		// TURN
		transform.localRotation *= Quaternion.Euler (0.0f, store.rotateAmount * Time.deltaTime, 0.0f);

		// MOVE		
		control.Move (transform.TransformDirection (store.moveAmount * Time.deltaTime));
	}

	void OnTriggerStay (Collider collider)
	{
		string collObjTag = collider.gameObject.tag;
		Debug.Log ("collision");
		if (collObjTag == "Obstacle")
		{
			// rotate and rumble for x seconds
		//	collider.gameObject.GetComponent<Hazard> ().InitiateHazardEffect (gameObject, playerIndex);


			Debug.Log ("collided with obstacle");
		}
		else if (collObjTag == "Finish")
		{
			// game over signal
			//gameController.GameEnd (playerIndex);
		}
	}

}
