using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1); //Index 1 in build
        transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Resume Game";
    }

    public static void EndGame()
    {
        Application.Quit();
    }
    
    public void Start(){
        RandomVariables.TestDistributions();
    }

}
