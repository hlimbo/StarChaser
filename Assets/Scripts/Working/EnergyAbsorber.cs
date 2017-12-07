using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;

public class EnergyAbsorber : MonoBehaviour
{
    public string tagToAbsorb;
    private EnergyAccumulator playerEnergy;

    private GameObject laserGO;
    private string untagged = "Untagged";
    private string sideAbsorber = "SideAbsorber";

    void Awake()
    {
        laserGO = GameObject.Find("LaserPivot");
        Assert.IsNotNull(laserGO, "LaserPivot game object is missing from the scene");
    }

    void Start()
    {
        playerEnergy = FindObjectOfType<EnergyAccumulator>();
        Assert.IsNotNull(playerEnergy, "EnergyAccumulator script reference is null");
    }

    void Update()
    {
        //deactivate side absorbers when shield is active
        gameObject.tag = (laserGO.activeInHierarchy) ? untagged : sideAbsorber;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (playerEnergy != null && tagToAbsorb.Equals(collider.tag))
        {
            // Debug.Log(collider.tag);
            ExecuteEvents.Execute<IEnergyMessenger>(playerEnergy.gameObject, null, (x, y) => x.GainEnergy());
        }
    }

}
