using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponSelection : MonoBehaviour
{
    public GameObject[] weapons;
    public int selectedWeapon = 0;


    private void Start()
    {
        weapons[selectedWeapon].SetActive(true);
        weapons[selectedWeapon].GetComponent<GunMechanics>().inSelection = true;
    }
    public void NextWeapon()
    {
        weapons[selectedWeapon].SetActive(false);
        weapons[selectedWeapon].GetComponent<GunMechanics>().inSelection = false;

        selectedWeapon = (selectedWeapon + 1) % weapons.Length;

        weapons[selectedWeapon].SetActive(true);
        weapons[selectedWeapon].GetComponent<GunMechanics>().inSelection = true;

    }

    public void PreviousWeapon()
    {
        weapons[selectedWeapon].SetActive(false);
        weapons[selectedWeapon].GetComponent<GunMechanics>().inSelection = false;

        selectedWeapon--;
        if (selectedWeapon < 0)
        {
            selectedWeapon += weapons.Length;
        }
        weapons[selectedWeapon].SetActive(true);
        weapons[selectedWeapon].GetComponent<GunMechanics>().inSelection = true;

    }

    public void StartGame()
    {
        weapons[selectedWeapon].GetComponent<GunMechanics>().inSelection = false;
        PlayerPrefs.SetInt("SelectedWeapon", selectedWeapon);
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
