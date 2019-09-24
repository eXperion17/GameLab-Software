using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallController : MonoBehaviour {

    GameObject target;
    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (target != null)
	        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime*10f);
	    else
	    {
            Destroy(this.gameObject);
	    }
	}

    public void SetTarget(GameObject enemyTarget)
    {
        target = enemyTarget;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
