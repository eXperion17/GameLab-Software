using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawn : MonoBehaviour
{
    public GameObject tower;
    private bool towerSpawned;
    private GameObject myTower;

	// Use this for initialization
	void Start ()
	{
	    SpawnTower();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.A))
	    {
	        DestroyTower();
	    }
        if (Input.GetKey(KeyCode.S))
        {
            SpawnTower();
        }
    }

    void SpawnTower()
    {
        if (!towerSpawned)
        {
            myTower = (GameObject) Instantiate(tower, transform.position, transform.rotation);
            towerSpawned = true;
        }
    }

    void DestroyTower()
    {
        Destroy(myTower);
        towerSpawned = false;
    }
}
