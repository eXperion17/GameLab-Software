using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    private GameObject mainTower;

    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    public float interval;
    public event Action OnLevelComplete;

    public float spawnDistance;
    private int waveCount;

    [SerializeField]
    private int waveEnemyAmount;
    [SerializeField]
    private Text waveCountText;
    [SerializeField]
    private Text waveCountMessage;

    //private List<Vector3> spawnPositions;

    private bool isTutorial;
    private TutorialManager tutorialManager;


    // Use this for initialization
    void Start()
    {
        mainTower = GameObject.Find("MainTower");

        interval = 1f;
        //spawnPositions = new List<Vector3>();
        waveCount = 0;
        waveEnemyAmount = 30;

        if (SceneManager.GetActiveScene().name == "_Tutorial" || SceneManager.GetActiveScene().name == "_Tutorial_part2")
        {
            tutorialManager = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
            isTutorial = true;

        }
        else
        {
            isTutorial = false;
        }
        StartCoroutine(EnemySpawner());
    }

    // Update is called once per frame
    void Update()
    {

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

    IEnumerator EnemySpawner()
    {
        if (!isTutorial)
        {

            UpdateWaveCount();
            yield return new WaitForSeconds(1f);
            UpdateWaveMiddle();

            yield return new WaitForSeconds(1.5f);
            while (waveCount < 4)
            {
                waveCountMessage.text = "";
                Debug.Log(interval);
                for (int i = 0; i < waveEnemyAmount; i++)
                {
                    SpawnEnemy();
                    yield return new WaitForSeconds(interval);
                }

                while (AreEnemiesLeft())
                {
                    yield return new WaitForSeconds(1f);
                }

                if (waveCount < 3)
                {
                    yield return new WaitForSeconds(4f);
                    NextWave();
                    yield return new WaitForSeconds(6f);
                }
                else
                {
                    waveCount++;
                }

                //Vector3 randomPosition = RandomCircle(new Vector3(transform.position.x, 0, transform.position.z), spawnDistance);
                //Instantiate(enemy, randomPosition, Quaternion.identity);
            }

            if (OnLevelComplete != null)
            {
                OnLevelComplete();
            }
        }
    }


    private bool AreEnemiesLeft()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Debug.Log(enemies.Length);

        if (enemies.Length == 0)
            return false;
        else
            return true;
        ;
    }

    private void NextWave()
    {
        waveEnemyAmount += 20;
        interval *= 0.9f;
        UpdateWaveCount();
        UpdateWaveMiddle();

    }

    private void UpdateWaveCount()
    {
        mainTower.GetComponent<TowerBase>().activateAllChildren();
        waveCount++;
        waveCountText.text = "Wave: " + waveCount;
    }

    private void UpdateWaveMiddle()
    {
        waveCountMessage.text = "Wave: " + waveCount;
    }

    public void SpawnEnemy()
    {
        Vector3 randomPosition = RandomCircle(this.transform.position, spawnDistance);
        Instantiate(enemy, randomPosition, Quaternion.identity);


    }
    public void SpawnTutorialWave()
    {

        #region TUTORIAL REGION

        int tutorialEnemyAmount = 0;

        if (SceneManager.GetActiveScene().name == "_Tutorial")
        {
            tutorialEnemyAmount = 10;
        }
        else
        {
            tutorialEnemyAmount = 20;
        }
        bool waveDone = false;

        for (int i = 0; i < tutorialEnemyAmount; i++)
        {
            SpawnEnemy();
        }
        #endregion
    }
    private Vector3 RandomCircle(Vector3 center, float radius)
    {

        // create random angle between 0 to 360 degrees
        float ang = UnityEngine.Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);

        return pos;
    }

    IEnumerator LoadLevel()
    {
        
        Text tutorialText = GameObject.Find("TutorialCanvas").GetComponentInChildren<Text>();
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
