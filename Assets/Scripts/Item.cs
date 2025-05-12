using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour {
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private Collider2D colliderItem;
    [SerializeField] private float speedMove = 10;
    
    private Vector2 _finishPosition;
    private int _id;
    public void SetId(int id) => _id = id;
    public int GetId => _id;
    
    public void SetItem(Transform parent) {
        rigidBody2D.bodyType = RigidbodyType2D.Static;
        ShowAnimation(parent.position);
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }
    
    private void ShowAnimation(Vector3 position) {
        DisableCollider();
        _finishPosition = position;
        StartCoroutine(MoveItem());
    }
    
    public void DisableCollider() => colliderItem.enabled = false;

    private IEnumerator MoveItem() {
        var distance = 1.0f;
        while (distance > 0.01f) { 
            Vector2 beginPosition = transform.position;
            transform.position = Vector3.Lerp( beginPosition , _finishPosition, Time.deltaTime * speedMove);
            distance = Vector2.Distance(beginPosition, _finishPosition);
            yield return null;
        }
    }
    
    public void Enable() {
        colliderItem.enabled = true;
        rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
        gameObject.SetActive(true);
    }
    public void Disable() { gameObject.SetActive(false); }
}

