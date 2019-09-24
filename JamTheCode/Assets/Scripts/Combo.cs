using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour {
    public int comboChain = 0;


    [SerializeField]
    public float comboTimer = 3;
    public float comboTimerMax;
    public int highestCombo;
    [SerializeField]
    private int addCombo = 1;
    [SerializeField]
    private int addComboEnemy = 2;
    public PlayerInput playerInput;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //TODO check if input is being chained in certain amount of time
        ComboAdd();
        
    }
    void ComboAdd()
    {
        
        if (playerInput.success == 1) {

            comboTimerMax = comboTimer; 
            comboChain+= addCombo;
            playerInput.success = 0;
        }
        else if( playerInput.success == 0)
        {
            if(comboChain >= 1 && comboTimerMax > 0) {
               comboTimerMax -= Time.deltaTime;
                
            }
            
        }
        if(playerInput.success == -1 || comboTimerMax <= 0)
        {
            if (highestCombo <= comboChain)
            {
                highestCombo = comboChain;
            }
            comboChain = 0;
            comboTimerMax = 0;
            
            
            
        }
        if (comboTimerMax < 0)
        {
            comboTimerMax = 0;
        }
        
    }
    public void EnemyDied()
    {
        comboChain += addComboEnemy;
    }
}
