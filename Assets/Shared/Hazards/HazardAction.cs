using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardAction : MonoBehaviour {
	
}

public class UpdatePlayerInsideCountAction : HazardAction {
	public int playerCountChange;

	public UpdatePlayerInsideCountAction (int playerCountChange){
		this.playerCountChange = playerCountChange;
	}
}
