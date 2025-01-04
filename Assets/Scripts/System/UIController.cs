using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public static UIController instance;

    [SerializeField] GameObject menu;
    //[SerializeField] GameObject controls;
    [SerializeField] GameObject[] windows;
    [SerializeField] GameObject[] upgrade_menus;

    [Header("Upgrade Components")]
    [SerializeField] GameObject[] selectedUpgrade;

    private void Awake() {

        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    /*Controll the in game ui...
      - Open and Close the menus
      - Open back up at the landing menu
     
     */

    public void ToggleWindow(int windowNumber) {
        for (int i = 0; i < windows.Length; i++) {
            if (i == windowNumber) {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            } else {
                windows[i].SetActive(false);
            }
        }
    }

    public void ToggleUpgradeMenus(int upgradeNumber) {
        for (int i = 0; i < upgrade_menus.Length; i++) {
            if (i == upgradeNumber) {
                upgrade_menus[i].SetActive(!upgrade_menus[i].activeInHierarchy);
                selectedUpgrade[i].SetActive(!selectedUpgrade[i].activeInHierarchy);
            } else {
                upgrade_menus[i].SetActive(false);
                selectedUpgrade[i].SetActive(false);
            }
        }
    }

    public void OpenMenu() {
        for (int i = 0; i < windows.Length; i++) {
            windows[i].SetActive(true);
        }
        menu.SetActive(false);

        //LevelManager.Instance.gameMenuOpen = true;
    }

    public void CloseMenu() {
        for (int i = 0; i < windows.Length; i++) {
            windows[i].SetActive(false);
        }
        menu.SetActive(true);

        //LevelManager.Instance.gameMenuOpen = false;
    }
}