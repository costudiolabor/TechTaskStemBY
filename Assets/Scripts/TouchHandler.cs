using System;
using UnityEngine;

[Serializable]
public class TouchHandler {
    [SerializeField] private Camera mainCamera;
    public event Action<Item> ClickItemEvent;
    public void OnUpdate() { DetectObjectWithRaycast(); }
    
    private void DetectObjectWithRaycast() {
        if (Input.GetMouseButtonDown(0)) {
            Click();
        }
    }

    private void Click() {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit) {
            if (hit.collider.TryGetComponent(out Item slot)) {
                ClickItemEvent?.Invoke(slot);
            }
        }
    }
}