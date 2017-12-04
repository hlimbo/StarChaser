using UnityEngine.EventSystems;

//interface is used for all gameObjects that require more than one hit to die
//e.g. player has 3 lives so the ship won't die in 1 hit
public interface ILifeMessenger : IEventSystemHandler {

    void LoseLife();
    void GainLife();
}
