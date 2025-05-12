using System;
using System.Collections;
using UnityEngine;


[Serializable]
public class SlotsHandler {
    [SerializeField] private Slot[] slots;
    [SerializeField] private int countBalloonMatch = 3;
    [SerializeField] private float delayShiftLeft = 0.1f;
    
    private int _currentIndex;
    private int _indexMatch;
    private int _emptySlotId;
    private MonoBehaviour _mono;
    
    public event Action ClearMatchEvent, OverFlowEvent;

    public void SetMono(MonoBehaviour mono) { _mono = mono; }
    
    public void ResetSlots(int value) {
        _currentIndex = 0;
        _emptySlotId = value;
        foreach (var slot in slots) {
            slot.ResetSlot(_emptySlotId);
        }
    }

    public void SetItem(Item item) {
        if (_currentIndex == slots.Length) {
            return;
        }
        FindSlotForItem(item);
        
        if (_currentIndex == slots.Length) { 
           OverFlowEvent?.Invoke();
        }
    }
    
    private void FindSlotForItem(Item item) {
        bool isMatch = false;
        _indexMatch = 0;
        for (int i = 0; i < slots.Length; i++) {
            if (item.GetId == slots[i].GetId) {
                _indexMatch = i + 1;
                isMatch = true;
            }
            else if (isMatch == false & slots[i].GetId == _emptySlotId) {
               _indexMatch = i;
               break;
            }
        }
        
        ShiftSlotsRight();
        SetBalloon(item, _indexMatch);
        FindTwoMatch();
    }

    private void ShiftSlotsRight() {
        for (int i = slots.Length - 1; i > _indexMatch; i--) { 
            slots[i].SetId(slots[i - 1].GetId);
            slots[i].SetChild(slots[i - 1].GetChild);
            if (slots[i].GetChild) slots[i].GetChild.SetItem(slots[i].GetTransform);
        }
    }

    private void FindTwoMatch() {
        int i = 0;
        int last = slots[i].GetId;
        int startIndex = i;
        var endIndex = i;
        for (i = 1; i < slots.Length; i++) {
            if (slots[i].GetId == _emptySlotId) return;
            if (last == slots[i].GetId) {
                endIndex = i;
                if ((endIndex - startIndex) == (countBalloonMatch - 1)) {
                    _mono.StartCoroutine(ClearMatch(startIndex, endIndex));
                    return;
                }
            }
            else {
                last = slots[i].GetId;
                startIndex = i;
                endIndex = i;
            }
        }
    }

    private IEnumerator ClearMatch(int startIndex, int endIndex) {
        for (int i = startIndex; i <= endIndex; i++) {
            slots[i].ResetSlot(_emptySlotId);
            ClearMatchEvent?.Invoke();
            _currentIndex--;
        }
        yield return new WaitForSeconds(delayShiftLeft);
        ShiftSlotsLeft(startIndex);
    }

    private void ShiftSlotsLeft(int startIndex) {
        int endIndex = slots.Length - countBalloonMatch;
        for (int i = startIndex; i < endIndex; i++) {
            int offset = i + countBalloonMatch;
            slots[i].SetId(slots[offset].GetId);
            slots[i].SetChild(slots[offset].GetChild);
            slots[offset].SetId(_emptySlotId);
            slots[offset].SetChild(null);
            if (slots[i].GetChild) slots[i].GetChild.SetItem(slots[i].GetTransform);
        }
    }
    
    private void SetBalloon(Item item, int index) { 
        slots[index].SetId(item.GetId);
        Transform transform = slots[index].GetTransform;
        slots[index].SetChild(item);
        _currentIndex++;
        SetObject(item, transform);
    }
    
    private void SetObject(Item item, Transform transform) {
        item.DisableCollider();
        item.SetItem(transform);
    }
}