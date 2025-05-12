using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "SetupItem", menuName = "Scriptable Objects/SetupItem")]
public class SetupItem : ScriptableObject {
    [SerializeField] private Item[] itemsPrefabs;
    public Item[] GetPrefabs() { return itemsPrefabs; }
   
}