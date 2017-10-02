using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardActionCreator : MonoBehaviour
{
	HazardStore hazardStore;

	void Awake ()
	{
		hazardStore = GetComponent<HazardStore> ();
	}


	public void UpdatePlayerInsideCountRequested (int playerCountChange)
	{
		hazardStore.Dispatch (new UpdatePlayerInsideCountAction (playerCountChange));
	}
}
