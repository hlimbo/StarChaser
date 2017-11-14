using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour {

    public CooldownTimer shieldCD;
    public CooldownTimer laserCD;
    public GameObject shieldGO;
    public GameObject laserGO;

    void Start()
    {
        shieldCD.GetComponent<Button>().onClick.AddListener(ShieldAbility);
        laserCD.GetComponent<Button>().onClick.AddListener(LaserAbility);
        shieldGO.SetActive(false);
        laserGO.SetActive(false);
    }

    void Update()
    {
        if(!shieldCD.isAbilityActive)
        {
            shieldGO.SetActive(false);
        }

        if(!laserCD.isAbilityActive)
        {
            laserGO.SetActive(false);
        }
    }

    void ShieldAbility()
    {
        if (!shieldGO.activeInHierarchy && !shieldCD.isOnCooldown)
        {
            print("shield");
            shieldGO.SetActive(true);
        }
    }

    void LaserAbility()
    {
        if(!laserGO.activeInHierarchy && !laserCD.isOnCooldown)
        {
            print("laser");
            laserGO.SetActive(true);
        }
    }
}
