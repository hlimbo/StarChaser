using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;

public class EnergyAbsorber : MonoBehaviour
{
    public string tagToAbsorb;
    protected EnergyAccumulator playerEnergy;

    protected void Start()
    {
        playerEnergy = FindObjectOfType<EnergyAccumulator>();
        Assert.IsNotNull(playerEnergy, "EnergyAccumulator script reference is null");
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        // Debug.Log("collision");
        if (tagToAbsorb.Equals(collider.tag))
        {
            ExecuteEvents.Execute<IEnergyMessenger>(playerEnergy.gameObject, null, (x, y) => x.GainEnergy());
        }
    }

}
