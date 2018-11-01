using UnityEngine;

public class OverUIObject : MonoBehaviour {

    void OnMouseOver() {        
        EventsManager.Instance.BroadcastPointerOnUI(true);        
    }

    void OnMouseExit() {
        EventsManager.Instance.BroadcastPointerOnUI(false);
    }

    void OnDestroy() {
        EventsManager.Instance.BroadcastPointerOnUI(false);
    }
}
