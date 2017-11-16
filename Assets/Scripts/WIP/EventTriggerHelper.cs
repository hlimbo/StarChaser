using System;
using UnityEngine.EventSystems;

public static class EventTriggerHelper {

    public static void AddEvent(EventTrigger trigger, EventTriggerType triggerType, Action<PointerEventData> myFunction)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = triggerType;
        entry.callback.AddListener((data) => { myFunction((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }
}
