using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;


public class PlayerActionCreator : MonoBehaviour
{

	PlayerStore playerStore;

	// Use this for initialization
	void Start ()
	{
		Init ();
	}

	public void Init ()
	{
		playerStore = GetComponent<PlayerStore> ();

	}


	public void UpdatePlayerBoxcastHitActionRequested (RaycastHit objectBoxcastHit)
	{
		playerStore.Dispatch (new UpdatePlayerBoxcastHitAction (objectBoxcastHit));
	}

	public void UpdateGamePadRightTriggerPressedActionRequested (float gamePadRightTriggerPressedAmount)
	{
		playerStore.Dispatch (new UpdateGamePadRightTriggerPressedAction (gamePadRightTriggerPressedAmount));
	}

	public void UpdateGamePadLeftTriggerPressedActionRequested (float gamePadLeftTriggerPressedAmount)
	{
		playerStore.Dispatch (new UpdateGamePadLeftTriggerPressedAction (gamePadLeftTriggerPressedAmount));
	}

	public void UpdateGamePadRightThumbStickMovedActionRequested(GamePadThumbSticks.StickValue rightThumbStickValue)
	{
		playerStore.Dispatch (new UpdateGamePadRightThumbStickMovedAction (rightThumbStickValue));
	}

	public void UpdateGamePadLeftThumbStickMovedActionRequested(GamePadThumbSticks.StickValue leftThumbStickValue)
	{
		playerStore.Dispatch (new UpdateGamePadLeftThumbStickMovedAction (leftThumbStickValue));
	}

	public void UpdatePlayerIndexActionRequested(PlayerIndex playerIndex)
	{
		playerStore.Dispatch (new UpdatePlayerIndexAction (playerIndex));
	}
}
