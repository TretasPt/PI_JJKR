using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int health;

    // Update is called once per frame
    void Update()
    {
        if (health < 1)
            SceneManager.LoadSceneAsync(0); //Index 0 in build
            PlayerMotor.UnlockCursor();
    }

    public void applyDamage(int damage)
    {
        health -= damage;
        Debug.Log("Damage: " + damage);
    }
}
