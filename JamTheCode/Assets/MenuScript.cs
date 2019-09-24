using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject select;


    public void OpenSelect() {
        mainMenu.SetActive(false);
        select.SetActive(true);
    }

    public void OpenMenu() {
        mainMenu.SetActive(true);
        select.SetActive(false);
    }

    public void Quit() {
        Application.Quit();
    }
}
