using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;


public class PlayerActionCreator : MonoBehaviour
{

	PlayerStore playerStore;

	void Awake ()
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

	public void UpdateGamePadRightThumbStickMovedActionRequested (GamePadThumbSticks.StickValue rightThumbStickValue)
	{
		playerStore.Dispatch (new UpdateGamePadRightThumbStickMovedAction (rightThumbStickValue));
	}

	public void UpdateGamePadLeftThumbStickMovedActionRequested (GamePadThumbSticks.StickValue leftThumbStickValue)
	{
		playerStore.Dispatch (new UpdateGamePadLeftThumbStickMovedAction (leftThumbStickValue));
	}

	public void UpdatePlayerIndexActionRequested (PlayerIndex playerIndex)
	{
		playerStore.Dispatch (new UpdatePlayerIndexAction (playerIndex));
	}

	public void UpdateInsideHazardActionRequested (GameObject hazardInside)
	{
		playerStore.Dispatch (new UpdateInsideHazardAction (hazardInside));
	}

	public void UpdateAddColliderActionRequested (Collider collider)
	{
		playerStore.Dispatch (new UpdateAddColliderAction (collider));
	}

	public void UpdateRemoveColliderActionRequested (Collider collider)
	{
		playerStore.Dispatch (new UpdateRemoveColliderAction (collider));
	}
}
