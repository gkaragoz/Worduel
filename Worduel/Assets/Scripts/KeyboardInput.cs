using UnityEngine;
using UnityEngine.EventSystems;

public class KeyboardInput : MonoBehaviour {

    private void Start() {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { OnPointerClick((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }

    public void OnPointerClick(PointerEventData data) {
        InputManager.instance.OnPressed(gameObject.name);
    }

}
