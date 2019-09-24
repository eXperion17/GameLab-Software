using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

    //Main tutorial text
    [SerializeField] private Text tutorialText;

    [SerializeField] private GameObject[] towers;
    public event Action OnLevelComplete;
    private EnemyManager enemyManager;


    //Button booleans
    public bool usedLeftTower = false;
    public bool usedRightTower = false;
    public bool usedTopTower = false;
    public bool usedBottomTower = false;

    private float interval;
    public bool introDone = false;
    public bool enemyIsSpawned = false;
    // Use this for initialization
    void Start () {
        tutorialText.text = "";
        enemyManager = GameObject.Find("MainTower").GetComponent<EnemyManager>();
        interval = 4f;
        StartCoroutine(Introduction());
	}
	
	// Update is called once per frame
	void Update () {

        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0 && enemyIsSpawned)
        {
            if(OnLevelComplete != null)
            {
                OnLevelComplete();
            }
        }
	}

    void OnEnable()
    {
        this.OnLevelComplete += LoadNextLevel;
    }

    void OnDisable()
    {
        this.OnLevelComplete -= LoadNextLevel;
    }


    void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator Introduction()
    {
        if(SceneManager.GetActiveScene().name == "_Tutorial")
        {
            #region TUTORIAL PART 1
            tutorialText.text = "Protect your castle in the middle!";
            yield return new WaitForSeconds(interval);
            tutorialText.text = "Use your four towers to withhold the enemies!";
            yield return new WaitForSeconds(interval);
            tutorialText.text = "Each tower is assigned to a key!";
            yield return new WaitForSeconds(interval);

            while (!usedTopTower)
            {
                tutorialText.text = "Use the 'W' or 'Y' on the controller key to use a tower!";
                yield return new WaitForSeconds(interval);
            }
            while (!usedBottomTower)
            {
                tutorialText.text = "Use the 'S' or 'A' on the controller key to use a tower!";
                yield return new WaitForSeconds(interval);
            }

            while (!usedLeftTower)
            {
                tutorialText.text = "Use the 'A' or 'X' on the controller key to use a tower!";
                yield return new WaitForSeconds(interval);
            }
            while (!usedRightTower)
            {
                tutorialText.text = "Use the 'D' or 'B' on the controller key to use a tower!";
                yield return new WaitForSeconds(interval);

            }
            tutorialText.text = "Prepare for the enemy wave!";
            yield return new WaitForSeconds(interval);
            tutorialText.text = "Kill the 10 enemies to continue!";
            yield return new WaitForSeconds(interval);
            tutorialText.text = "";

            for (int i = 0; i < 10; i++)
            {
                yield return new WaitForSeconds(enemyManager.interval);
                enemyManager.SpawnEnemy();
            }

            enemyIsSpawned = true;
            #endregion
        } else if(SceneManager.GetActiveScene().name == "_Tutorial_part2")
        {
            #region TUTORIAL PART 2
            tutorialText.text = "You now have the ability to use more towers!";
            yield return new WaitForSeconds(interval);
            tutorialText.text = "Try chaining towers!";
            yield return new WaitForSeconds(interval);
            tutorialText.text = "The grey highlighter shows your current tower";
            yield return new WaitForSeconds(interval);
            tutorialText.text = "Hold Spacebar or Left Bumper on the controller + Tower key to freeze your enemies!";
            yield return new WaitForSeconds(interval);
            tutorialText.text = "Kill the 20 enemies to continue";
            yield return new WaitForSeconds(interval);
            tutorialText.text = "";

            for (int i = 0; i < 20; i++)
            {
                yield return new WaitForSeconds(enemyManager.interval);
                enemyManager.SpawnEnemy();
                
            }
            enemyIsSpawned = true;

            #endregion
        }
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(1f);
        tutorialText.text = "Wave complete!";
        yield return new WaitForSeconds(1f);
        tutorialText.text = "";
        tutorialText.text = "Loading new level...";
        yield return new WaitForSeconds(4f);
        int i = Application.loadedLevel;
        Application.LoadLevel(i + 1);

    }
}
