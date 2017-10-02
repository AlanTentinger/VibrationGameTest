using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerAction
{


}

public class UpdatePlayerBoxcastHitAction : PlayerAction
{
	public RaycastHit objectBoxcastHit;

	public UpdatePlayerBoxcastHitAction (RaycastHit objectBoxcastHit)
	{
		this.objectBoxcastHit = objectBoxcastHit;
	}
		
}

public class UpdateGamePadRightTriggerPressedAction : PlayerAction
{
	public float gamePadRightTriggerPressedAmount;

	public UpdateGamePadRightTriggerPressedAction (float gamePadRightTriggerPressedAmount)
	{
		this.gamePadRightTriggerPressedAmount = gamePadRightTriggerPressedAmount;
	}
}

public class UpdateGamePadLeftTriggerPressedAction : PlayerAction
{
	public float gamePadLeftTriggerPressedAmount;

	public UpdateGamePadLeftTriggerPressedAction (float gamePadLeftTriggerPressedAmount)
	{
		this.gamePadLeftTriggerPressedAmount = gamePadLeftTriggerPressedAmount;
	}
}

public class UpdateGamePadRightThumbStickMovedAction : PlayerAction
{
	public GamePadThumbSticks.StickValue gamePadRightThumbStickValue;

	public UpdateGamePadRightThumbStickMovedAction (GamePadThumbSticks.StickValue gamePadRightThumbStickValue)
	{
		this.gamePadRightThumbStickValue = gamePadRightThumbStickValue;
	}
}

public class UpdateGamePadLeftThumbStickMovedAction : PlayerAction
{
	public GamePadThumbSticks.StickValue gamePadLeftThumbStickValue;

	public UpdateGamePadLeftThumbStickMovedAction (GamePadThumbSticks.StickValue gamePadLeftThumbStickValue)
	{
		this.gamePadLeftThumbStickValue = gamePadLeftThumbStickValue;
	}
}

public class UpdatePlayerIndexAction : PlayerAction
{
	public PlayerIndex playerIndex;

	public UpdatePlayerIndexAction (PlayerIndex playerIndex)
	{
		this.playerIndex = playerIndex;
	}
}

public class UpdateInsideHazardAction : PlayerAction
{
	public GameObject hazardInside;

	public UpdateInsideHazardAction (GameObject hazardInside)
	{
		this.hazardInside = hazardInside;
	}
}

public class UpdateAddColliderAction : PlayerAction
{
	public Collider collider;

	public UpdateAddColliderAction (Collider collider)
	{
		this.collider = collider;
	}
}

public class UpdateRemoveColliderAction : PlayerAction
{
	public Collider collider;

	public UpdateRemoveColliderAction (Collider collider)
	{
		this.collider = collider;
	}
}