using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
   [Header("References")]
   [SerializeField] private Transform[] weapons;

   [Header("Keys")] 
   [SerializeField] private KeyCode[] keys;

   [Header("Settings")] 
   [SerializeField] private float switchTime;

   private int selectedWeapon;
   private float timeSinceLastSwitch;

   private void Start()
   {
      SetWeapons();
   }

   private void SetWeapons()
   {
      weapons = new Transform[transform.childCount];
      for (int i = 0; i < transform.childCount; i++)
      {
         weapons[i] = transform.GetChild(i);
      }

      if (keys == null) keys = new KeyCode[weapons.Length];
   }
   
   private void Select(int weaponIndex)
   {
      for (int i = 0; i < weapons.Length; i++) 
      {
         weapons[i].gameObject.SetActive(i == weaponIndex);
      }

      //timeSinceLastSwitch = 0f;
      OnWeaponSelect();
   } 
   private void OnWeaponSelect()
   {
      Debug.Log("Selected a new weapon.");
   }
}
