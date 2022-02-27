using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Objects : MonoBehaviour
{
    public float speed = .3f;
    public float delta = .5f;
    [SerializeField] InteractableEvent interactable;

    Vector3 oPos;
    private void Start() {
        oPos = transform.position;

        interactable.OnInteract.AddListener(() => {
            Destroy(this.gameObject);
        });
    }

    // Update is called once per frame
    void Update() {
        var t = Mathf.PingPong(speed * Time.time, delta);
        var pos = new Vector3(oPos.x, oPos.y + t, oPos.z);
        transform.position = pos;


    }
}
