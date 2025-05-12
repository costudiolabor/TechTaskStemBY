using System;
using UnityEngine;

public class Entry : MonoBehaviour {
    [SerializeField] private GameHandler gameHandler;
    [SerializeField] private UIHandler uiHandler;
    [SerializeField] private TouchHandler touchHandler;
    [SerializeField] private SlotsHandler slotsHandler;
    [SerializeField] private Spawner spawner;

    private int _countPrefabs;
    private int _countItems;
    private const  int Offset = 1;
    private void Awake() { Initialize(); }
    private void Update() { touchHandler.OnUpdate(); }
    private void Initialize() {
        uiHandler.Initialize();
        spawner.Create(this);
        _countItems = spawner.GetItems();
        _countPrefabs = spawner.GetPrefabs() + Offset;
        slotsHandler.SetMono(this);
        slotsHandler.ResetSlots(_countPrefabs);
        Subscription();
    }
    private void OnClickItem(Item item) { slotsHandler.SetItem(item); }
    private void OnReset() {
        slotsHandler.ResetSlots(_countPrefabs);
        spawner.ResetItems(this);
        _countItems = spawner.GetItems();
    }
    private void OnNextWin() { gameHandler.ReLoadScene(); }
    private void OnNextLose() { gameHandler.ReLoadScene(); }
    private void OnClearMatch() {
        _countItems--;
        CheckCountItems();
    }
    private void OnOverFlow() { uiHandler.ShowLosePanel(); }
    private void CheckCountItems() { if (_countItems <= 0) { uiHandler.ShowWinPanel(); } }
    
    private void Subscription() {
        uiHandler.ClickResetEvent += OnReset;
        uiHandler.ClickNextWinEvent += OnNextWin;
        uiHandler.ClickNextLoseEvent += OnNextLose;
        touchHandler.ClickItemEvent += OnClickItem;
        slotsHandler.ClearMatchEvent += OnClearMatch;
        slotsHandler.OverFlowEvent += OnOverFlow;
    }

    private void UnSubscription() {
        uiHandler.ClickResetEvent -= OnReset;
        uiHandler.ClickNextWinEvent -= OnNextWin;
        uiHandler.ClickNextLoseEvent -= OnNextLose;
        touchHandler.ClickItemEvent -= OnClickItem;
        slotsHandler.ClearMatchEvent -= OnClearMatch;
        slotsHandler.OverFlowEvent -= OnOverFlow;
    }

    private void OnDestroy() { UnSubscription(); }
}
