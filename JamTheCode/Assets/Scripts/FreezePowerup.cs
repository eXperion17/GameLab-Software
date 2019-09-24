using UnityEngine;
using UnityEditor;

public class FreezePowerup : MonoBehaviour, IPowerUp {
	public GameObject explosion;

	public void Activation(TowerBase tower) {
		//SoundManager.Instance.PlayFreeze();
		SoundLocator.GetSoundManager().PlayFreeze();
		tower.Explosion(explosion);
	}
}