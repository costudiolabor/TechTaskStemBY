using System;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour {
    [SerializeField] private Button button;

    public event Action ClickEvent;
    
    public void Initialize() {
        button.onClick.AddListener(OnClick);
        Hide();
    }

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);
    private void OnClick() { ClickEvent?.Invoke(); }
    private void OnDestroy() { button.onClick.RemoveAllListeners(); }
}
