using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Boo : MonoBehaviour
{
    public float speed = 1f;
    public float delta = .5f;

    void Update() {
        var t = Mathf.PingPong(speed * Time.time, delta);

        transform.localScale = Vector3.one * (t + 1);

        if (PlayerInput.InteractKeyDown)
            Destroy(this.gameObject);
    }
}
