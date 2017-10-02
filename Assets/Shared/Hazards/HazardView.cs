using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardView : MonoBehaviour
{

	HazardStore hazardStore;
	HazardActionCreator hazardActionCreator;

	void Awake ()
	{
		hazardStore = GetComponent<HazardStore> ();
		hazardActionCreator = GetComponent<HazardActionCreator> ();
	}

	void Update ()
	{
		GetComponent<Renderer> ().material.color = hazardStore.color; 
	}

	void OnTriggerEnter (Collider collider)
	{
		string collObjTag = collider.gameObject.tag;
		Debug.Log ("collision");
		if (collObjTag == "Player")
		{
			hazardActionCreator.UpdatePlayerInsideCountRequested (1);
			Debug.Log ("player collided with hazard");
		}

		//else if (collObjTag == "Finish")
		//{
		//	// game over signal
		//	//gameController.GameEnd (playerIndex);
		//}
	}

	void OnTriggerExit (Collider collider)
	{
		string collObjTag = collider.gameObject.tag;
		Debug.Log ("collision");
		if (collObjTag == "Player")
		{
			hazardActionCreator.UpdatePlayerInsideCountRequested (-1);
		}
	}


}
