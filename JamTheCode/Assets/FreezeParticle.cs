using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeParticle : MonoBehaviour {
    ParticleSystem freeze;
    
   
    // Use this for initialization
    void Start () {
        freeze = GetComponent<ParticleSystem>();
        freeze.Play();



    }
	
	// Update is called once per frame
	void Update () {
       
       


    }
}
