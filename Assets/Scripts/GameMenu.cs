using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class GameMenu : MonoBehaviour
{
    public static GameObject menuGameObject;
    public static GameObject musicSubMenuGameObject;
    public static GameObject musicText;
    
    public static GameObject controlsSubMenuGameObject;
    public static GameObject xSensitivityText;
    public static GameObject ySensitivityText;
    
    public static bool optionsMenuState = true;
    public static bool musicSubMenuState = true;
    public static bool controlsSubMenuState = true;

    private Vector2 mouseSensitivity2D;
    private static float musicSlider;
    
    private void Start()
    {
        menuGameObject = GameObject.Find("Menu");
        musicSubMenuGameObject = menuGameObject.transform.GetChild(4).gameObject;
        controlsSubMenuGameObject = menuGameObject.transform.GetChild(5).gameObject;
        
        musicText = musicSubMenuGameObject.transform.GetChild(2).GetChild(0).gameObject;
        
        xSensitivityText = controlsSubMenuGameObject.transform.GetChild(2).GetChild(0).gameObject;
        ySensitivityText = controlsSubMenuGameObject.transform.GetChild(5).GetChild(0).gameObject;
        
        updateSensibilityText();
        SwitchMusicSubMenu();
        SwitchControlsSubMenu();
        switchGameMenu();
    }
    
    public static void goToMainMenu()
    {
        switchGameMenu();
        PlayerMotor.UnlockCursor();
        //MainMenu.SwitchOptionsMenu();
        MainMenu.updateMenu(musicSlider);
        SceneManager.LoadSceneAsync(0);
    }

    public static void switchGameMenu()
    {
        optionsMenuState = !optionsMenuState;
        //Debug.Log("Game Menu switched: " + optionsMenuState);
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
    
    public static void SwitchMusicSubMenu()
    {
        if (controlsSubMenuState)
        {
            musicSubMenuState = !musicSubMenuState; //Inverts current musicSubMenuState bool value;
            controlsSubMenuGameObject.SetActive(!controlsSubMenuState);
        }
        musicSubMenuState = !musicSubMenuState; //Inverts current musicSubMenuState bool value;
        musicSubMenuGameObject.SetActive(musicSubMenuState); 
        musicText.GetComponent<TextMeshProUGUI>().text = musicSlider + "";
    }

    public static void SwitchControlsSubMenu()
    {
        if (musicSubMenuState)
        {
            controlsSubMenuState = !controlsSubMenuState;
            musicSubMenuGameObject.SetActive(!musicSubMenuState); 
        }
        controlsSubMenuState = !controlsSubMenuState;
        controlsSubMenuGameObject.SetActive(controlsSubMenuState);
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
        musicText.GetComponent<TextMeshProUGUI>().text = musicSlider + "";
    }

    public void updateSensibilityText()
    {
        mouseSensitivity2D = PlayerLook.getSensitivi2D();
        xSensitivityText.GetComponent<TextMeshProUGUI>().text = mouseSensitivity2D.x + " - xSensibility";
        ySensitivityText.GetComponent<TextMeshProUGUI>().text = mouseSensitivity2D.y + " - ySensibility";
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
