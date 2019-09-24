﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tower : TowerBase {
    //[SerializeField]
	private ActivateKeys activationKey;
    [SerializeField]
    public Text InputText;
    [SerializeField]
    private GameObject freeze;
    private GameObject rangeIndicator;
    
    private float scaleSpeed;
    [SerializeField]
    Vector3 startSize;
    // Use this for initialization

    protected override void Start() {
	    base.Start();

        InputToColor(GetActivationKey());
        InputText.text = "";
        InputText.transform.localScale = new Vector3(-InputText.transform.localScale.x, InputText.transform.localScale.y, InputText.transform.localScale.z);
        rangeIndicator = transform.Find("RangeIndicator").gameObject;
        
        rangeIndicator.transform.localScale = startSize;
    }
	
	// Update is called once per frame
	void Update () {
        if (explosionRange < maxExplosionRange)
        {
            explosionRange += 0.003f;
            
        }
        if (explosionRange > maxExplosionRange) explosionRange = maxExplosionRange;
        rangeIndicator.transform.localScale = startSize * explosionRange;
        if (isActive)
        {
            rangeIndicator.SetActive(true);
        }
        else
        {
            rangeIndicator.SetActive(false);
        }

    }
    public override void Explosion()
    {
        if (Input.GetButton("Power"))
        {
            FreezePower();
            Debug.Log("Poweeeeeeeeeeeeeeeeeeeeeeeeeeeer");
        }
        else
        {
            base.Explosion();
        }
        InputText.text = "";

    }
    void FreezePower()
    {
        SoundManager.Instance.PlayFreeze();
        GameObject go = Instantiate(freeze, transform);

        go.GetComponent<Explosion>().SetSizeMultiplier(explosionRange);
        explosionRange -= explosionRangeDecrease;
        if (explosionRange < 0) explosionRange = 0;
        go.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }


    private void InputToColor(ActivateKeys key)
    {

        switch (key)
        {
            //A-button
            case ActivateKeys.X:
                InputText.color = Color.green;
                GetComponent<SpriteRenderer>().color = Color.green;
                break;
            //B-button
            case ActivateKeys.Circle:
                InputText.color = Color.red;
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            //X-Button
            case ActivateKeys.Square:
                InputText.color = Color.blue;
                GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            //Y-button
            case ActivateKeys.Triangle:
                InputText.color = Color.yellow;
                GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            default:
                break;
        }
    }
    public string InputToString(ActivateKeys key)
    {
        string inputText = "";

        switch (key)
        {
            //A-button
            case ActivateKeys.X:
                //inputText = "S";
                inputText = "A";
                break;
            //B-button
            case ActivateKeys.Circle:
                //inputText = "D";
                inputText = "B";
                break;
            //X-Button
            case ActivateKeys.Square:
                //inputText = "A";
                inputText = "X";
                break;
            //Y-button
            case ActivateKeys.Triangle:
                //inputText = "w";
                inputText = "Y";
                break;
            default:
                break;
        }
        return inputText;
    }
    void OnMouseDown() {
        SetActive();
        
    }

    public override void Die() {
        base.Die();
        InputText.GetComponent<TextHighlight>().StopHighlight();
    }

    public ActivateKeys GetActivationKey() {
		return activationKey;
	}

    public void SetActivationKey(ActivateKeys key)
    {
        activationKey = key;
        InputToColor(key);
        InputToString(key);

    }

    public override void activateAllChildren()
    {
        base.activateAllChildren();
    }
}