using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class MainMenu : MonoBehaviour
{
    private static AudioSource audioSource;
    private static List<String> AudioClipsList = new List<String> {"2021_HSV1_Cave_of_Agony", "2021_HSV1_Darvaza's_Awakening", "2021_HSV1_Dolls_of_Nagoro", "2021_HSV1_The_Haunted_Docks", "2021_HSV1_Tomb_of_the_Forgotten"};

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
        PlayerMotor.UnlockCursor();

        //Assignment of the gameObjects for the static functions later.
        menuGameObject = transform.gameObject;
        musicSubMenuGameObject = menuGameObject.transform.GetChild(4).gameObject;
        controlsSubMenuGameObject = menuGameObject.transform.GetChild(5).gameObject;
        
        musicText = musicSubMenuGameObject.transform.GetChild(2).GetChild(0).gameObject;

        xSensitivityText = controlsSubMenuGameObject.transform.GetChild(0).GetChild(0).gameObject;
        ySensitivityText = controlsSubMenuGameObject.transform.GetChild(5).GetChild(0).gameObject;
        

        updateSensibilityText();
        //Making Menus and SubMenus disappear
        //controlsSubMenuGameObject.SetActive(controlsSubMenuState);
        
        initMusic();
        updateMenu(musicSlider);
        SwitchControlsSubMenu();
        SwitchOptionsMenu();
        SwitchMusicSubMenu();
    }

    public static void initMusic()
    {
        audioSource = menuGameObject.transform.parent.GetComponent<AudioSource>();
        audioSource.clip = getRandomMusic(); //sets audioSource's clip to a random music inside the resources folder
        //Debug.Log("audioClip: " + audioSource.clip);
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1); //Game is Index 1 in build
        

        GameMenu.optionsMenuState = true;
        GameMenu.controlsSubMenuState = false;
        GameMenu.musicSubMenuState = false;
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

    public static AudioClip getRandomMusic()
    {
        return Resources.Load<AudioClip>("Music/" + AudioClipsList[Random.Range(0, AudioClipsList.Count)]);
    }

}
