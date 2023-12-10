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
    public static GameObject musicText;
    
    public static GameObject controlsSubMenuGameObject;
    public static GameObject xSensitivityText;
    public static GameObject ySensitivityText;
    
    public static bool optionsMenuState = false;
    public static bool musicSubMenuState = false;
    public static bool controlsSubMenuState = false;

    private Vector2 mouseSensitivity2D;
    private static float musicSlider;

    private void Start()
    {
        //Assignment of the gameObjects for the static functions later.
        menuGameObject = GameObject.Find("Main Menu");
        musicSubMenuGameObject = menuGameObject.transform.GetChild(4).gameObject;
        controlsSubMenuGameObject = menuGameObject.transform.GetChild(5).gameObject;
        
        musicText = musicSubMenuGameObject.transform.GetChild(2).GetChild(0).gameObject;

        xSensitivityText = controlsSubMenuGameObject.transform.GetChild(0).GetChild(0).gameObject;
        ySensitivityText = controlsSubMenuGameObject.transform.GetChild(5).GetChild(0).gameObject;
        updateSensibilityText();
        //Making Menus and SubMenus disappear
        //controlsSubMenuGameObject.SetActive(controlsSubMenuState);
        
        SwitchControlsSubMenu();
        SwitchOptionsMenu();
        SwitchMusicSubMenu(); 
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1); //Game is Index 1 in build


        GameMenu.optionsMenuState = true;
        GameMenu.controlsSubMenuState = true;
        GameMenu.musicSubMenuState = true;
        GameMenu.updateMenu(musicSlider);
        GameMenu.SwitchMusicSubMenu();
        GameMenu.SwitchControlsSubMenu();
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
        if (controlsSubMenuState)
        {
            SwitchControlsSubMenu();
        }
        musicSubMenuState = !musicSubMenuState; //Inverts current musicSubMenuState bool value;
        musicSubMenuGameObject.SetActive(musicSubMenuState);
        musicText.GetComponent<TextMeshProUGUI>().text = musicSlider + "";
    }

    public static void SwitchControlsSubMenu()
    {
        if (musicSubMenuState)
        {
            SwitchMusicSubMenu();
        }
        controlsSubMenuState = !controlsSubMenuState; //Inverts current controlsSubMenuState bool value;
        controlsSubMenuGameObject.SetActive(controlsSubMenuState);
    }
    
    public void updateSensibilityText()
    {
        mouseSensitivity2D = PlayerLook.getSensitivi2D();
        xSensitivityText.GetComponent<TextMeshProUGUI>().text = mouseSensitivity2D.x + " - xSensibility";
        ySensitivityText.GetComponent<TextMeshProUGUI>().text = mouseSensitivity2D.y + " - ySensibility";
    }

    public static void increaseVolume()
    {
        musicSlider++;
        musicText.GetComponent<TextMeshProUGUI>().text = musicSlider + "";
    }

    public static void decreaseVolume()
    {
        musicSlider--;
        musicText.GetComponent<TextMeshProUGUI>().text = musicSlider + "";
    }
    
    public static void updateMenu(float music)
    {
        musicSlider = music;
        //musicText.GetComponent<TextMeshProUGUI>().text = musicSlider + "";
    }
    
    public void increaseXMouseSensibility()
    {
        mouseSensitivity2D.x++;
        xSensitivityText.GetComponent<TextMeshProUGUI>().text = mouseSensitivity2D.x + " - xSensibility";
        PlayerLook.setSensitivity2D(mouseSensitivity2D);
    }
    public void increaseYMouseSensibility()
    {
        mouseSensitivity2D.y++;
        ySensitivityText.GetComponent<TextMeshProUGUI>().text = mouseSensitivity2D.y + " - ySensibility";
        PlayerLook.setSensitivity2D(mouseSensitivity2D);
    }
    public void decreaseXMouseSensibility()
    {
        mouseSensitivity2D.x--;
        xSensitivityText.GetComponent<TextMeshProUGUI>().text = mouseSensitivity2D.x + " - xSensibility";
        PlayerLook.setSensitivity2D(mouseSensitivity2D);
    }
    public void decreaseYMouseSensibility()
    {
        mouseSensitivity2D.y--;
        ySensitivityText.GetComponent<TextMeshProUGUI>().text = mouseSensitivity2D.y + " - ySensibility";
        PlayerLook.setSensitivity2D(mouseSensitivity2D);
    }

}
