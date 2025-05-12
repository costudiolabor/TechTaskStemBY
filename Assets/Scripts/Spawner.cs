using System;
using UnityEngine;
using System.Collections;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

[Serializable]
public class Spawner {
    [SerializeField] private float delaySpawn = 0.1f;
    [SerializeField] private SetupItem setupItem;
    [SerializeField] private Transform[] spawnPoints;
    
    private const int MaxItems = 21;
    private int _indexItem;
    private int _currentItem;
    private Item[] _itemPrefabs;
    private Item[] _items = new Item[MaxItems];
    public int GetPrefabs() => setupItem.GetPrefabs().Length;
    public int GetItems() => _items.Length;
    public void Create(MonoBehaviour mono) {
        _itemPrefabs = setupItem.GetPrefabs();
        SetIdItems();
        for (int i = 0; i < MaxItems; i++) { _items[i] = GetItem(); }
        Shuffle(_items);
        CreateItems();
        mono.StartCoroutine(Spawn());
    }

    private void SetIdItems() {
        int offset = 1;
        for (int i = 0; i < _itemPrefabs.Length; i++) {
            _itemPrefabs[i].SetId(i + offset);
        }
    }

    private Item GetItem() {
        if (_indexItem == 0) { _currentItem = Random.Range(0, _itemPrefabs.Length); }
        Item item = _itemPrefabs[_currentItem];
        if (_indexItem == 2) 
            _indexItem = 0;
        else
            _indexItem++;
        return item;
    }
    
    private void Shuffle(Item[] items) {
        for (int i = 0; i < items.Length; i++) {
            int j = Random.Range(0, items.Length);
            (items[j], items[i]) = (items[i], items[j]);
        }
    }
    
    private void CreateItems() {
        for (int i = 0; i < MaxItems; i++) { 
            Item item = Object.Instantiate(_items[i]);
            item.SetId(_items[i].GetId);
            _items[i] = item;
            _items[i].Disable();
        }
    }
    
    private IEnumerator Spawn() {
        for (int i = 0; i < MaxItems; i++) { 
            int randomPoint = Random.Range(0, spawnPoints.Length);
            yield return new WaitForSeconds(delaySpawn);
            _items[i].transform.position = spawnPoints[randomPoint].position;
            _items[i].Enable();
        }
    }
    
    public void ResetItems(MonoBehaviour mono) {
        for (int i = 0; i < MaxItems; i++) {
            _items[i].Disable();
        }
        mono.StartCoroutine(Spawn());
    }
    
}
