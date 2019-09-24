using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour {
    [SerializeField] private GameObject explosion;


    private bool startedCoroutine = false;

    public void StartExplosion() {
        explosion.SetActive(true);
        SoundManager.Instance.PlayMainExplosion();
        GameObject tower = gameObject.transform.FindChild("TowerBase").gameObject;

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
