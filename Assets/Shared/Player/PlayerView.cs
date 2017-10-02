using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{

	private PlayerStore store;
	private PlayerActionCreator playerActionCreator;
	private GamePadState gamePadState;
	CharacterController control;

	private Text playerNumberText;

	public Camera POVCamera;

	public RenderTexture POVOne;
	public RenderTexture POVTwo;
	public RenderTexture POVThree;
	public RenderTexture POVFour;

	void Awake ()
	{
		store = GetComponent<PlayerStore> ();
		playerActionCreator = GetComponent<PlayerActionCreator> ();
		control = GetComponent<CharacterController> ();
		playerNumberText = transform.Find ("PlayerCanvas").Find("PlayerNumber").gameObject.GetComponent<Text> ();
	}

	void Start ()
	{
		AssignPlayerNumberUI ();
		AssignTargetTexture ();
	}

	void AssignPlayerNumberUI ()
	{
		playerNumberText.text = ((int)store.playerIndex).ToString();
		playerNumberText.color = store.color;
	}


	void AssignTargetTexture ()
	{
		switch (store.playerIndex)
		{
		case (PlayerIndex)0:
			POVCamera.targetTexture = POVOne;
			break;
		case (PlayerIndex)1:
			POVCamera.targetTexture = POVTwo;
			break;
		case (PlayerIndex)2:
			POVCamera.targetTexture = POVThree;
			break;
		case (PlayerIndex)3:
			POVCamera.targetTexture = POVFour;
			break;
		default:
			break;
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
		// input'
		GetComponent<Renderer> ().material.color = store.color;

		gamePadState = GamePad.GetState (store.playerIndex);

		playerActionCreator.UpdateGamePadLeftTriggerPressedActionRequested (gamePadState.Triggers.Left);
		playerActionCreator.UpdateGamePadRightTriggerPressedActionRequested (gamePadState.Triggers.Right);
		playerActionCreator.UpdateGamePadRightThumbStickMovedActionRequested (gamePadState.ThumbSticks.Right);
		playerActionCreator.UpdateGamePadLeftThumbStickMovedActionRequested (gamePadState.ThumbSticks.Left);

		Vector3 fwd = transform.TransformDirection (Vector3.forward);
		RaycastHit objectBoxcastHit;
		//if (Physics.BoxCast (transform.position, new Vector3 (transform.localScale.x / 2, transform.localScale.y / 2, transform.localScale.z / 2), fwd, out objectBoxcastHit, Quaternion.identity, store.maxDist))
		//{
		Physics.BoxCast (transform.position, new Vector3 (transform.localScale.x / 2, transform.localScale.y / 2, transform.localScale.z / 2), fwd, out objectBoxcastHit, Quaternion.identity, store.maxDist);
		playerActionCreator.UpdatePlayerBoxcastHitActionRequested (objectBoxcastHit);
		//}

		// output
		GamePad.SetVibration (store.playerIndex, store.leftVibration, store.rightVibration);
		Debug.Log (store.leftVibration);

		// TURN
		transform.localRotation *= Quaternion.Euler (0.0f, store.rotateAmount * Time.deltaTime, 0.0f);

		// MOVE		
		control.Move (transform.TransformDirection (store.moveAmount * Time.deltaTime));
	}

	void OnTriggerEnter (Collider collider)
	{
		Debug.Log ("player collision");
		playerActionCreator.UpdateAddColliderActionRequested (collider);
	}

	void OnTriggerExit (Collider collider)
	{
		playerActionCreator.UpdateRemoveColliderActionRequested (collider);
	}
}
