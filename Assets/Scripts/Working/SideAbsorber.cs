using UnityEngine;
using UnityEngine.Assertions;

//reminder (unity thing): when SideAbsorber inherits from EnergyAbsorber, 
//SideAbsorber will invoke any MonoBehaviour methods defined in EnergyAbsorber
//regardless of their access specifier (public, private, or protected)
public class SideAbsorber : EnergyAbsorber
{
    private GameObject laserGO;
    private string untagged = "Untagged";
    private string sideAbsorber = "SideAbsorber";

    void Awake()
    {
        laserGO = GameObject.Find("LaserPivot");
        Assert.IsNotNull(laserGO, "LaserPivot game object is missing from the scene");
    }

    void Update()
    {
        //deactivate side absorbers when shield is active
        gameObject.tag = (laserGO.activeInHierarchy) ? untagged : sideAbsorber;
    }
}
