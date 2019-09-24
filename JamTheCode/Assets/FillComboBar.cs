using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillComboBar : MonoBehaviour {
    private Combo comboScript;
    [SerializeField]
    private Image bomb;
    [SerializeField]
    private Text comboAmountText;
    [SerializeField]
    private Text highestComboText;
    // Use this for initialization
    void Start () {
        GameObject mainTower = GameObject.Find("MainTower");
        comboScript = mainTower.GetComponent<Combo>();
    }
	
	// Update is called once per frame
	void Update () {
        HandleBar();

    }

    void HandleBar()
    {
        bomb.fillAmount = comboScript.comboTimerMax / comboScript.comboTimer;
        comboAmountText.text = "Combo: " + comboScript.comboChain.ToString();
        highestComboText.text = "Highest combo: " + comboScript.highestCombo.ToString();
    }
}
