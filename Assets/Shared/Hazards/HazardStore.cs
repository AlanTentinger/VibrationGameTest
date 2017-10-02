using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardStore : MonoBehaviour {

	// CONSTANTS
	public Color defaultColor = Color.yellow;

	// EXTERNAL
	private int _playerInsideCount;
	private Color _color;

	// INTERNAL 
	public Color color;


	void Awake () {
		_playerInsideCount = 0;
		_color = defaultColor;

	}

	void Start (){
		ComputeState ();
	}

	public void Dispatch (HazardAction action)
	{
		Reducer (action);
		ComputeState ();
	}

	private void Reducer (HazardAction action)
	{
		switch (action.GetType ().ToString ())
		{
		case "UpdatePlayerInsideCountAction":
			_playerInsideCount += (((UpdatePlayerInsideCountAction)action).playerCountChange);
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
		color = ComputeColor(_color, _playerInsideCount);
	}

	private Color ComputeColor (Color _color, int _playerInsideCount)
	{
		if (_playerInsideCount > 0)
		{
			return Color.red;
		}
		else
		{
			return defaultColor;
		}
	}
}
