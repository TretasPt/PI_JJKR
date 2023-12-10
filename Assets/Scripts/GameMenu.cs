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
    private static List<String> AudioClipsList = new List<String> {"2021_HSV1_Cave_of_Agony", "2021_HSV1_Darvaza's_Awakening", "2021_HSV1_Dolls_of_Nagoro", "2021_HSV1_The_Haunted_Docks", "2021_HSV1_Tomb_of_the_Forgotten"};
    private static AudioSource audioSource;
    
    public static GameObject menuGameObject;
    public static GameObject musicSubMenuGameObject;
    public static GameObject musicText;
    
    public static GameObject controlsSubMenuGameObject;
    public static GameObject xSensitivityText;
    public static GameObject ySensitivityText;
    
    public static bool optionsMenuState = true;
    public static bool musicSubMenuState = false;
    public static bool controlsSubMenuState = false;

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
        
        audioSource = transform.parent.GetComponent<AudioSource>();
        
        updateSensibilityText();
        SwitchMusicSubMenu();
        SwitchControlsSubMenu();
        switchGameMenu();
        initMusic();
    }
    
    public static void initMusic()
    {
        audioSource.clip = MainMenu.getRandomMusic(); //sets audioSource's clip to a random music inside the resources folder
        //Debug.Log("audioClip: " + audioSource.clip);
        audioSource.loop = true;
        audioSource.Play();
    }
    
    public static void goToMainMenu()
    {
        //switchGameMenu();
        SceneManager.LoadSceneAsync(0);
        PlayerMotor.UnlockCursor();
        
        //MainMenu.initMusic();
        MainMenu.updateMenu(musicSlider);
        
        MainMenu.optionsMenuState = true;
        MainMenu.SwitchOptionsMenu();
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
    
    public static void increaseVolume()
    {
        musicSlider++;
        musicText.GetComponent<TextMeshProUGUI>().text = musicSlider + "";
        audioSource.volume += 1/20f;
    }

    public static void decreaseVolume()
    {
        musicSlider--;
        musicText.GetComponent<TextMeshProUGUI>().text = musicSlider + "";
        audioSource.volume -= 1/20f;
    }

    public static void updateMenu(float music)
    {
        musicSlider = music;
        audioSource.volume = musicSlider / 20f;
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
