using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerScript : MonoBehaviour
{

    private TowerBase currentTower;
    private GameObject mainTower;
    private float speed = 15f;

    // Use this for initialization
    void Start ()
    {
        mainTower = GameObject.Find("MainTower");
    }
	
	// Update is called once per frame
	void Update () {
		currentTower = mainTower.GetComponent<PlayerInput>().currentTower;
        transform.position = Vector3.MoveTowards(transform.position, currentTower.transform.position, Time.deltaTime * speed);
    }
}
