using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour {
    [SerializeField] private TowerBase towerBase;

    public TowerBase currentTower;
    private TowerBase tower = null;
    private TowerBase mainTower;
    public int success = 0;
    //bool isButtonDown = false;
    private float timer;
    //[SerializeField]
    //private float activationTime = 1f;

    private TutorialManager tutorialManager;
    // Use this for initialization
    void Start() {
        ChangeCurrenTower(towerBase);
        towerBase = GameObject.Find("MainTower").GetComponent<TowerBase>();
        towerBase.TextActivator(towerBase.children);
        if(SceneManager.GetActiveScene().name == "_Tutorial")
        {
            tutorialManager = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
        }
        
    }
	
	// Update is called once per frame
	void Update () {
       InputHandler();
        

    }

    protected void InputHandler() {
        if (!currentTower.Active()) {
            ChangeCurrenTower(towerBase);
        }
        
        bool hasActiveChild = false;
        for (int i = 0; i < currentTower.children.Length; i++)
        {
            if (currentTower.children[i].Active())
            {
                hasActiveChild = true;
            }
        }
        if (!hasActiveChild)
        {
            ChangeCurrenTower(towerBase);
        }

        if (Input.GetButtonDown("X"))
        {
            if (SceneManager.GetActiveScene().name == "_Tutorial")
            {
                tutorialManager.usedBottomTower = true;
            }
            tower = currentTower.GetChildByKey(Tower.ActivateKeys.X);
            HandlePlayerInput(tower);
        } else if (Input.GetButtonDown("Circle")) {
            if (SceneManager.GetActiveScene().name == "_Tutorial")
            {
                tutorialManager.usedRightTower = true;
            }
            tower = currentTower.GetChildByKey(Tower.ActivateKeys.Circle);
            HandlePlayerInput(tower);
        } else if (Input.GetButtonDown("Square")) {
            if (SceneManager.GetActiveScene().name == "_Tutorial")
            {
                tutorialManager.usedLeftTower = true;
            }
            tower = currentTower.GetChildByKey(Tower.ActivateKeys.Square);
            HandlePlayerInput(tower);
        } else if (Input.GetButtonDown("Triangle")) {
            if (SceneManager.GetActiveScene().name == "_Tutorial")
            {
                tutorialManager.usedTopTower = true;
            }
            tower = currentTower.GetChildByKey(Tower.ActivateKeys.Triangle);
            HandlePlayerInput(tower);
        }
        
        if (currentTower.children.Length == 0) {
            ChangeCurrenTower(towerBase);
            currentTower.TextActivator(currentTower.children);
        }
    }

    void HandlePlayerInput(TowerBase tower)
    {
        if (tower != null)
        {
            tower.TextActivator(tower.children);
            SoundManager.Instance.PlayTowerShoot();
            if (tower.Active())
            {
                towerBase.ResetTextTowers();
                Explosion(tower);
                success = 1;
            }
        }
        else
        {
            success = -1;
            //TODO Enter combo breaker
        }
    }

    void ChangeCurrenTower(TowerBase tower)
    {
        currentTower = tower;
        currentTower.TextActivator(currentTower.children);
    }
    void Explosion(TowerBase tower)
    {
        tower.Explosion();
        ChangeCurrenTower(tower);
    }
}
