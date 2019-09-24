using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour {
	
	private List<IPowerUp> powerups;
	private IPowerUp currentPowerup;

	private void Awake() {
		powerups = new List<IPowerUp>();
		MonoBehaviour[] behaviours = GetComponents<MonoBehaviour>();


		for (int i = 0; i < behaviours.Length; i++) {
			if (behaviours[i] is IPowerUp) {
				powerups.Add(behaviours[i] as IPowerUp);
			}
		}

		currentPowerup = powerups[0];
	}

	public IPowerUp GetPowerUp() {
		return currentPowerup;
	}

	public IPowerUp SwitchPowers(int movement) {
		int powerupID = powerups.FindIndex(x => x == currentPowerup);
		powerupID += movement;
		if (powerupID >= powerups.Count)
			powerupID = 0;
		else if (powerupID < 0) {
			powerupID = powerups.Count - 1;
		}

		currentPowerup = powerups[powerupID];

		return currentPowerup;
	}
}
