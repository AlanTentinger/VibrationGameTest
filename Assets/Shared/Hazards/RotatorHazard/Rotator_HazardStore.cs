using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator_HazardStore : HazardStore
{

	// CONSTANTS
	public float rotateMagnitude = 50f;

	// INTERNAL STATE
	private int _rotateModifier;

	// EXTERNAL STATE
	public float rotateEffect;

	void Awake ()
	{
		_rotateModifier = new List<int>(){-3,-2,-1,1,2,3}[Random.Range(0,6)];
		ComputeState ();
	}

	void ComputeState (){
		rotateEffect = ComputeRotateEffect (_rotateModifier);
	}

	private float ComputeRotateEffect (float _rotateModifier){
		return (float)_rotateModifier * rotateMagnitude;
	}

}
