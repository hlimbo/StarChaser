using UnityEngine.EventSystems;

//interface used to communicate between 
public interface IEnergyMessenger : IEventSystemHandler {

    void GainEnergy(int value);

}
