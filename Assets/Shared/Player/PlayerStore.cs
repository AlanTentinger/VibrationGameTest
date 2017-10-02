using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerStore : MonoBehaviour
{

	// component/gameobject references
	public GameObject goal;


	// internal state
	private RaycastHit _objectBoxcastHit = new RaycastHit ();
	private float _gamePadRightTriggerPressedAmount = 0.0f;
	private float _gamePadLeftTriggerPressedAmount = 0.0f;
	private PlayerIndex _playerIndex;
	private GamePadThumbSticks.StickValue _gamePadRightThumbStickValue;
	private GamePadThumbSticks.StickValue _gamePadLeftThumbStickValue;
	private GameObject _hazardInside;
	private List<Collider> _collidersInside = new List<Collider> ();

	// derived internal state
	private bool _gamePadRightTriggerPressed;
	private bool _gamePadLeftTriggerPressed;
	private Vector3 _goalDirection;
	private float _angleTowardGoal;

	// external state
	public float leftVibration;
	public float rightVibration;
	public PlayerIndex playerIndex;
	public float rotateAmount;
	public Vector3 moveAmount;
	public float maxDist;
	public Color color;


	void Awake ()
	{
		//playerIndex must set upon initialization
		goal = GameObject.FindGameObjectWithTag ("Finish");
		ComputeState ();
	}

	public void Dispatch (PlayerAction action)
	{
		Reducer (action);
		ComputeState ();
	}

	private void Reducer (PlayerAction action)
	{
		switch (action.GetType ().ToString ())
		{
		case "UpdatePlayerBoxcastHitAction":
			_objectBoxcastHit = ((UpdatePlayerBoxcastHitAction)action).objectBoxcastHit;
			break;
		case "UpdateGamePadRightTriggerPressedAction":
			_gamePadRightTriggerPressedAmount = ((UpdateGamePadRightTriggerPressedAction)action).gamePadRightTriggerPressedAmount;
			break;
		case "UpdateGamePadLeftTriggerPressedAction":
			_gamePadLeftTriggerPressedAmount = ((UpdateGamePadLeftTriggerPressedAction)action).gamePadLeftTriggerPressedAmount;
			break;
		case "UpdatePlayerIndexAction":
			_playerIndex = ((UpdatePlayerIndexAction)action).playerIndex;
			break;
		case "UpdateGamePadRightThumbStickMovedAction":
			_gamePadRightThumbStickValue = ((UpdateGamePadRightThumbStickMovedAction)action).gamePadRightThumbStickValue;
			break;
		case "UpdateGamePadLeftThumbStickMovedAction":
			_gamePadLeftThumbStickValue = ((UpdateGamePadLeftThumbStickMovedAction)action).gamePadLeftThumbStickValue;
			break;
		case "UpdateInsideHazardAction":
			_hazardInside = ((UpdateInsideHazardAction)action).hazardInside;
			break;
		case "UpdateAddColliderAction":
			_collidersInside.Add (((UpdateAddColliderAction)action).collider);
			break;
		case "UpdateRemoveColliderAction":
			_collidersInside.Remove (((UpdateRemoveColliderAction)action).collider);
			break;
		default:
			Debug.Log ("no type of action");
			// don't change internal state
			break;
		}
	}


	private void ComputeState ()
	{
		// external derived state
		_gamePadRightTriggerPressed = ComputeGamePadRightTriggerPressed (_gamePadRightTriggerPressedAmount);
		_gamePadLeftTriggerPressed = ComputeGamePadLeftTriggerPressed (_gamePadLeftTriggerPressedAmount);
		_goalDirection = ComputeGoalDirection (goal);
		_angleTowardGoal = ComputeAngleTowardGoal (_goalDirection);

		// internal derived state
		leftVibration = ComputeLeftVibration (_objectBoxcastHit, _gamePadRightTriggerPressed);
		rightVibration = ComputeRightVibration (_angleTowardGoal);
		playerIndex = ComputePlayerIndex (_playerIndex);
		rotateAmount = ComputeRotateAmount (_gamePadRightThumbStickValue, _collidersInside);
		moveAmount = ComputeMoveAmount (_gamePadLeftThumbStickValue);
		maxDist = 7f;
		color = ComputeColor(_playerIndex);
	}

	private bool ComputeGamePadRightTriggerPressed (float _gamePadRightTriggerPressedAmount)
	{
		if (_gamePadRightTriggerPressedAmount > 0.5f)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	private bool ComputeGamePadLeftTriggerPressed (float _gamePadLeftTriggerPressedAmount)
	{
		if (_gamePadLeftTriggerPressedAmount > 0.5f)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public float ComputeAngleTowardGoal (Vector3 _goalDirection)
	{
		return Vector3.SignedAngle (_goalDirection, transform.forward, Vector3.up);
		// 100/0 looking at goal
		// float angleTowardGoal = Vector3.SignedAngle (goalDir, -transform.forward, Vector3.up);
		// right = (angleTowardGoal+180f)/360f;
	}

	private Vector3 ComputeGoalDirection (GameObject goal)
	{
		return goal.transform.position - transform.position;
	}

	private float ComputeLeftVibration (RaycastHit _objectBoxcastHit, bool _gamePadRightTriggerPressed)
	{
		if (_objectBoxcastHit.collider != null)
		{
			if ((_objectBoxcastHit.collider.gameObject.tag == "Hazard" && !_gamePadRightTriggerPressed))
			{
				return (1 / _objectBoxcastHit.distance);
			}
		}
		return 0f;

	}

	private float ComputeRightVibration (float _angleTowardGoal)
	{
		return Mathf.Abs (_angleTowardGoal) / 60f;
	}

	private float ComputeRotateAmount (GamePadThumbSticks.StickValue _gamePadRightThumbStickValue, List<Collider> _collidersInside)
	{
		float returnRotateAmount = 0;
		if (Mathf.Abs (_gamePadRightThumbStickValue.X) > .4)
		{
			returnRotateAmount += _gamePadRightThumbStickValue.X * 50.0f;
		}
		foreach (Collider col in _collidersInside)
		{
			string collObjTag = col.gameObject.tag;
			if (collObjTag == "Hazard")
			{
				HazardStore hazStore = col.gameObject.GetComponent<HazardStore> ();
				if (hazStore.GetType ().ToString () == "Rotator_HazardStore")
				{
					returnRotateAmount += ((Rotator_HazardStore)hazStore).rotateEffect;
				}
			}
		}

		return returnRotateAmount;
	}

	private Vector3 ComputeMoveAmount (GamePadThumbSticks.StickValue _gamePadLeftThumbStickValue)
	{
		float xMove = 0;
		float zMove = 0;
		if (Mathf.Abs (_gamePadLeftThumbStickValue.X) > .4)
		{
			xMove = _gamePadLeftThumbStickValue.X * 5.0f;
		}
		if (Mathf.Abs (_gamePadLeftThumbStickValue.Y) > .4)
		{
			zMove = _gamePadLeftThumbStickValue.Y * 5.0f;
		}
		return new Vector3 (xMove, 0.0f, zMove);
	}

	private PlayerIndex ComputePlayerIndex (PlayerIndex _playerIndex)
	{
		return _playerIndex;
	}

	private Color ComputeColor(PlayerIndex _playerIndex)
	{
		switch (_playerIndex)
		{
		case (PlayerIndex)0:
			return Color.blue;
			break;
		case (PlayerIndex)1:
			return Color.red;
			break;
		case (PlayerIndex)2:
			return Color.green;
			break;
		case (PlayerIndex)3:
			return Color.magenta;
			break;
		default:
			return Color.black;
			break;
		}
	}
}
