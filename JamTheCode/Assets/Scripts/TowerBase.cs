using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TowerBase : MonoBehaviour {
    [SerializeField]
    public TowerBase parentTower;
    [SerializeField]
    public Tower[] children;
    private TowerBase mainTower;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject selectObject;


    [SerializeField] protected float maxExplosionRange = 2f;
    [SerializeField] protected float explosionRangeDecrease = 0.5f;
    protected float explosionRange;



    [SerializeField]
    private Animation animations;
    private GameObject ashes;


    [SerializeField]
    protected bool isActive = true;

    [SerializeField]
    private GameObject fireSpawn;
    [SerializeField]
    private GameObject magicBall;

    [SerializeField] private GameObject connector;
    [SerializeField] private bool buildConnections;
    [SerializeField] private Death death;

    public enum ActivateKeys
    {
        X = 0,
        Circle = 1,
        Square = 2,
        Triangle = 3
    }

    // Use this for initialization
    protected virtual void Start () {

        //GetComponent<SpriteRenderer>().color = Color.green;
        mainTower = GameObject.Find("MainTower").GetComponent<TowerBase>();
        
        RandomizeActivationKeys();
        explosionRange = maxExplosionRange;

        animations = gameObject.GetComponentInChildren<Animation>();
        ashes = animations.gameObject.transform.GetChild(1).gameObject;

        //animations = GameObject.Find("TowerV1").GetComponent<Animation>();

        if (!isActive) animations.Play("Destroyed");
        TextActivator(mainTower.children);
        BuildConnections();
    }

    protected void BuildConnections() {
        if (buildConnections) {
            for (int i = 0; i < children.Length; i++) {
                //Vector3 newPos = (transform.position + children[i].transform.position) / 2;
                GameObject conn = Instantiate(this.connector, transform.position, Quaternion.identity);
                conn.transform.LookAt(children[i].transform);

                float dist = Vector3.Distance(children[i].transform.position, transform.position);

                conn.transform.Translate(Vector3.forward * dist / 2, conn.transform);
                conn.transform.localScale = new Vector3(0.1f, 0.2f, dist * 0.95f);

                //if (children[i].children.Length > 0) {
                children[i].BuildConnections();
                //}
            }
        }
    }
	
	// Update is called once per frame
	void Update () {

	}
    

    protected virtual void RandomizeActivationKeys()
    {
        int[] activationKeys = new int[4] { 0, 1, 2, 3 };
        for (int i = 0; i < activationKeys.Length; i++)
        {
            int temp = activationKeys[i];
            int randomIndex = Random.Range(i, activationKeys.Length);
            activationKeys[i] = activationKeys[randomIndex];
            activationKeys[randomIndex] = temp;
        }

        for (int i = 0; i < children.Length; i++)
        {
            children[i].SetActivationKey((ActivateKeys)activationKeys[i]);
        }
        
    }

    public void Shoot(GameObject enemy)
    {
        GameObject newMagicBall = Instantiate(magicBall);
        newMagicBall.transform.position = fireSpawn.transform.position;
        newMagicBall.GetComponent<MagicBallController>().SetTarget(enemy);
    }
    public void TextActivator(Tower[] childrenList)
    {
        foreach (Tower child in childrenList)
        {
            if (child.isActive)
            {
                //child.InputText = child.GetComponentInChildren<Text>();
                child.InputText.text = child.InputToString(child.GetActivationKey());
                child.InputText.GetComponent<TextHighlight>().StartHighlight();
                //child.TextActivator(child.children);
            }
        }
    }

    virtual public void ResetTextTowers()
    {
        foreach (Tower tower in children)
        {
            tower.GetComponentInChildren<Text>().text = "";
            tower.ResetTextTowers();
        }
    }
    public void SetParent(TowerBase tower) {
		parentTower = tower;
	}

	public void AddChild(Tower child) {
		if (children.Length == 4) {
			Debug.Log("Tower exceeds children limit!");
		}
		children[children.Length] = child;
	}

	public Tower GetChildByKey(Tower.ActivateKeys input) {
		for (int i = 0; i < children.Length; i++) {
			Tower tempChild = children[i];
			if (tempChild.GetActivationKey() == input) {
				return tempChild;
			}
		}

		return null;
	}
    
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            if (gameObject.name == "MainTower") {
                death.StartExplosion();
            }

            if (isActive) other.GetComponent<Enemy>().Explode();
            Die();
        }

        
    }
    
    virtual public void Explosion() {
        GameObject go = Instantiate(explosion, transform);
        go.GetComponent<Explosion>().SetSizeMultiplier(explosionRange);
        explosionRange -= explosionRangeDecrease;
        if (explosionRange < 0) explosionRange = 0;
        go.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        //Selection Ring
        //GameObject select = Instantiate(selectObject, transform);
        //select.transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        //select.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f;

    }

    virtual public void Die() {
        if (!isActive) return;

        //TODO REMOVE WHEN MAIN TOWER HAS MODEL
        if (gameObject.name != "MainTower") {
            ashes.SetActive(true);
            //ashes.transform.Rotate(Vector3.forward, Random.value * 360);
            animations.Play("Destruction");
            SoundManager.Instance.PlayTowerCrumble();
        } 
        isActive = false;
        GetComponent<SpriteRenderer>().enabled = false;

        ResetTextTowers();
        //this.gameObject.GetComponentInChildren<Text>().text = "";
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i].Active()) children[i].Invoke("Die", 1f);
        }
        //GetComponent<SpriteRenderer>().enabled = false;
    }

    virtual public void activateAllChildren()
    {
        foreach (Tower tower in children)
        {
            
            tower.SetActive();
            tower.Invoke("activateAllChildren", 1f);
        }
    }

    public bool Active() {
        return isActive;
    }

    public void SetActive() {

        if (parentTower.Active())
        {
            GetComponent<SpriteRenderer>().enabled = true;
            if (!isActive)
            {
                animations.Play("Repair");
                SoundManager.Instance.PlayTowerCrumble();
            }

            isActive = true;
        }
    }
}
