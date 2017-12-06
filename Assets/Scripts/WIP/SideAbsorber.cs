using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;

public class SideAbsorber : EnergyAbsorber
{
    private GameObject laserGO;
    private string untagged = "Untagged";
    private string shield = "Shield";

    void Awake()
    {
        laserGO = GameObject.Find("LaserPivot");
        Assert.IsNotNull(laserGO, "LaserPivot game object is missing from the scene");
    }

    void Update()
    {
        gameObject.tag = (laserGO.activeInHierarchy) ? untagged : shield;
    }
}
