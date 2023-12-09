using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public static GameObject menuGameObject;
    public static GameObject musicSubMenuGameObject;
    public static GameObject controlsSubMenuGameObject;
    
    public static bool optionsMenuState = false;
    public static bool musicSubMenuState = false;
    public static bool controlsSubMenuState = false;

    private static Vector2 mouseSensitivity2D;
    private static float musicSlider;

    private void Start()
    {
        //Assignment of the gameObjects for the static functions later.
        menuGameObject = GameObject.Find("Menu");
        musicSubMenuGameObject = menuGameObject.transform.GetChild(0).gameObject;
        controlsSubMenuGameObject = menuGameObject.transform.GetChild(1).gameObject;
        //Making Menus and SubMenus disappear
        //musicSubMenuGameObject.SetActive(musicSubMenuState);
        //controlsSubMenuGameObject.SetActive(controlsSubMenuState);
        SwitchOptionsMenu();
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1); //Index 1 in build
        GameMenu.switchGameMenu();
        transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Resume Game";
    }

    public static void EndGame()
    {
        Application.Quit();
    }

    public static void SwitchOptionsMenu()
    {
        optionsMenuState = !optionsMenuState; //Inverts current optionsMenuState bool value.
        menuGameObject.SetActive(optionsMenuState);
    }

    public static void SwitchMusicSubMenu()
    {
        musicSubMenuState = !musicSubMenuState; //Inverts current musicSubMenuState bool value;
        musicSubMenuGameObject.SetActive(musicSubMenuState);
    }

    public static void SwitchControlsSubMenu()
    {
        controlsSubMenuState = !controlsSubMenuState; //Inverts current controlsSubMenuState bool value;
        controlsSubMenuGameObject.SetActive(controlsSubMenuState);
    }

}
