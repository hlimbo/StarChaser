using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;

public class EnergyAbsorber : MonoBehaviour
{
    public string tagToAbsorb;
    protected EnergyAccumulator playerEnergy;

    void Start()
    {
        playerEnergy = FindObjectOfType<EnergyAccumulator>();
        Assert.IsNotNull(playerEnergy, "EnergyAccumulator script reference is null");
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
