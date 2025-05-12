using System;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {
    [SerializeField] private UIPanel winPanel;
    [SerializeField] private UIPanel losePanel;
    [SerializeField] private Button buttonReset;

    public event Action ClickResetEvent, ClickNextWinEvent, ClickNextLoseEvent;
    
    public void Initialize() {
        winPanel.Initialize();
        losePanel.Initialize();
        Subscription();
    }

    private void OnClickReset() { ClickResetEvent?.Invoke(); }
    public void ShowWinPanel() { winPanel.Show(); }
    public void ShowLosePanel() { losePanel.Show(); }
    private void Subscription() {
        buttonReset.onClick.AddListener(OnClickReset);
        winPanel.ClickEvent += OnClickNextWin;
        losePanel.ClickEvent += OnClickNextLose;
    }

    private void OnClickNextWin() { ClickNextWinEvent?.Invoke(); }
    private void OnClickNextLose() { ClickNextLoseEvent?.Invoke(); }
    private void UnSubscription() { buttonReset.onClick.RemoveAllListeners(); }
    private void OnDestroy() { UnSubscription(); }
    
}