using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public int totalGems;
	public int gemsRemaining;

	void Start() {
		gemsRemaining = totalGems;
	}

}
