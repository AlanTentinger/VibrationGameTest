using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class GameController : MonoBehaviour {

	public GameObject rotatorHazard;
	public GameObject goal;
	public GameObject player;

	public AudioClip gameEndSound;
	private AudioSource source;

	public List<GameObject> players;

	private bool gameHasEnded = false;


	// External State
	public float numberOfHazardCircles = 5f;
	public float circleDist = 20f;

	// Testing
	public float circleDistCoef = 2f;
	public float hazRadiusVariability = 3f;
	public float hazAngleVariabilityCoef = .5f;


	// Internal State
	public float hazMax;

	void Awake() {
		source = GetComponent<AudioSource> ();

		// Connect controllers to player objects
		for (int i = 0; i < 4; ++i)
		{
			PlayerIndex testPlayerIndex = (PlayerIndex)i;
			GamePadState testState = GamePad.GetState(testPlayerIndex);
			if (testState.IsConnected)
			{
				Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
				Vector3 startPos = new Vector3 (numberOfHazardCircles * circleDist+goal.transform.position.x+circleDist, 0.5f, 0f);
				GameObject newPlayer = Object.Instantiate (player, startPos, Quaternion.identity) as GameObject;
				newPlayer.transform.RotateAround (goal.transform.position, Vector3.up, 90 * i);
				newPlayer.GetComponent<PlayerActionCreator> ().Init ();
				newPlayer.GetComponent<PlayerStore> ().Init ();
				newPlayer.GetComponent<PlayerView> ().Init ();

				newPlayer.GetComponent<PlayerActionCreator>().UpdatePlayerIndexActionRequested( (PlayerIndex)i);
				//newPlayer.GetComponent<PlayerStore> ().gameController = this;
				players.Add(newPlayer);
			}
		}
	}

	// Use this for initialization
	void Start () {
		// per circle loop




		hazMax = circleDist / circleDistCoef;
		Vector3 initialHazardPos;
		for (int circleIndex = 1; circleIndex <= numberOfHazardCircles; circleIndex++) {
			float circleRadius = circleIndex * circleDist;
			float hazMinAngle = Mathf.Ceil(((1f * hazMax)/circleRadius) * 57.296f);
			float hazAngleVariability = hazMinAngle * hazAngleVariabilityCoef;

			float hazMaxAngle = hazMinAngle + hazAngleVariability;

			Debug.Log (hazMaxAngle);

			float numHazardsInCirlce = Mathf.Floor ((360) / hazMaxAngle);
			Debug.Log (numHazardsInCirlce);
				
			initialHazardPos = new Vector3 (goal.transform.position.x, 0.0f, goal.transform.position.z + circleRadius);

			float horizontalMaxHazardDistance = Mathf.Ceil((hazMaxAngle/ 57.296f)*circleRadius)/2f;
			Vector3 hazPos = initialHazardPos;
			float count = 0; 
			while (Vector3.Distance(initialHazardPos,hazPos)>horizontalMaxHazardDistance || count <2) {
				float hazAngle = hazMaxAngle - Random.Range (0, hazAngleVariability);
				GameObject newHaz = CreateHazard (rotatorHazard, hazPos);
				newHaz.transform.RotateAround (goal.transform.position, Vector3.up, hazAngle);
				hazPos = newHaz.transform.position;
				count ++;
			}
		}


	}

	GameObject CreateHazard(GameObject haz, Vector3 pos){
		GameObject newHaz = Object.Instantiate (haz, pos,Quaternion.identity) as GameObject;
		float hazRadius = hazMax - Random.Range (0, hazRadiusVariability);
		newHaz.transform.localScale = new Vector3(hazRadius, 1f,hazRadius);
		return newHaz;
	}
	public void GameEnd(PlayerIndex playerIndex){
		gameHasEnded = true;
		Debug.Log (playerIndex + " WON!");
		source.PlayOneShot (gameEndSound,.1f);


	}

	// Update is called once per frame
	void Update () {
		//source.Play ();

	}
}
