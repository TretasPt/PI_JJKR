using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMenu : MonoBehaviour
{
    public static GameObject menuGameObject;
    public static GameObject musicSubMenuGameObject;
    public static GameObject controlsSubMenuGameObject;
    
    public static bool optionsMenuState = false;
    public static bool musicSubMenuState = false;
    public static bool controlsSubMenuState = false;

    private Vector2 mouseSensitivity2D;
    private float musicSlider;
    
    private void Start()
    {
        //Assignment of the gameObjects for the static functions later.
        menuGameObject = GameObject.Find("Menu");
        musicSubMenuGameObject = menuGameObject.transform.GetChild(0).gameObject;
        controlsSubMenuGameObject = menuGameObject.transform.GetChild(1).gameObject;
        //Making Menus and SubMenus disappear
        //musicSubMenuGameObject.SetActive(musicSubMenuState);
        //controlsSubMenuGameObject.SetActive(controlsSubMenuState);
        
        switchGameMenu();
    }


    public static void goToMainMenu()
    {
        switchGameMenu();
        PlayerMotor.UnlockCursor();
        SceneManager.LoadSceneAsync(0);
    }

    public static void switchGameMenu()
    {
        optionsMenuState = !optionsMenuState;
        if (optionsMenuState)
        {
            menuGameObject.SetActive(optionsMenuState);
            PlayerMotor.UnlockCursor();
        }
        else
        {
            menuGameObject.SetActive(optionsMenuState);
            PlayerMotor.LockCursor();

        }
    }
    
    
}
