using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_LightSwitch : MonoBehaviour
{

    public float speed = 1f;
    public float delta = 1f;
    [SerializeField] LightSwitch lightSwitch;

    Vector3 oPos;
    private void Start() {
        oPos = transform.position;

        lightSwitch.OnInteract.AddListener(() => {
            Destroy(this.gameObject);
        });
    }

    // Update is called once per frame
    void Update()
    {
        var t = Mathf.PingPong(speed * Time.time, delta);
        var pos = new Vector3(oPos.x, oPos.y + t, oPos.z);
        transform.position = pos;


    }
}
