using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour {

    [HideInInspector] public string text;
    public float duration = 2;
    public float sizeIncrease = 2f;
    [SerializeField] TMP_Text tmpComponent;

    Vector3 maxScale;
    void OnEnable() {
        transform.localScale = Vector3.one;
        GetComponent<Canvas>().worldCamera = Camera.main;
        maxScale = transform.localScale * 1.5f;
        tmpComponent.text = text;
    }

    private void Update() {
        //hack.
        if (tmpComponent.text != text) 
            tmpComponent.text = text;

        transform.localScale *= 1+((sizeIncrease / duration) * Time.deltaTime);
        if (transform.localScale.x >= maxScale.x) {
            GameObject.Destroy(this.transform.parent.gameObject);
        }
    }
}
