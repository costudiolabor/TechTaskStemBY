using UnityEngine;

public class Slot : MonoBehaviour { 
   [SerializeField] private Transform thisTransform;
   private Item _child;
   private int _id = 0;

   public void ResetSlot(int value) {
       _id = value;
       if (_child) {
           _child.transform.parent = null;
           _child.Disable();
           _child = null;
       }
   }

   public Transform GetTransform => thisTransform;
   public void SetChild(Item child) => _child = child;
   public Item GetChild => _child;
   public void SetId(int id) => _id = id;
   public int GetId => _id;
   
}
