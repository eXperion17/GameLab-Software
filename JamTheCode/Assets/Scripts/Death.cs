using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour {
    [SerializeField] private GameObject explosion;

	private SoundManager soundManager;


    private bool startedCoroutine = false;

	private void Awake() {
		soundManager = SoundLocator.GetSoundManager();
	}

	public void StartExplosion() {
        explosion.SetActive(true);
		soundManager.PlayMainExplosion();
        //SoundManager.Instance.PlayMainExplosion();
        GameObject tower = gameObject.transform.Find("TowerBase").gameObject;

        tower.SetActive(false);

        gameObject.GetComponent<PlayerInput>().enabled = false;

        StartCoroutine("LoadLevel");
    }

    IEnumerator LoadLevel() {
        while (true) {
            yield return new WaitForSeconds(4f);
            SceneManager.LoadScene(Application.loadedLevel);
        }
        if (startedCoroutine) {
            gameObject.SetActive(false);
        } else {
            startedCoroutine = true;
        }
        
        yield return new WaitForSeconds(4f);
    }
}
