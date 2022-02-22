using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltPlayerControls : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float gravity = 20;
    CharacterController cc;

    private void Awake() {
        cc = GetComponent<CharacterController>();
    }

    private void Update() {
        //Handle Controls
        {
            Vector3 dir = Vector3.zero;
            if (Input.GetKey(KeyCode.W)) dir += new Vector3(0, 0, 1);
            if (Input.GetKey(KeyCode.A)) dir += new Vector3(-1, 0, 0);
            if (Input.GetKey(KeyCode.S)) dir += new Vector3(0, 0, -1);
            if (Input.GetKey(KeyCode.D)) dir += new Vector3(1, 0, 0);
            dir = dir.normalized;

            Vector3 rotation = new Vector3(0, Camera.main.transform.rotation.eulerAngles.y, 0);
            Matrix4x4 m = Matrix4x4.Rotate(Quaternion.Euler(rotation));
            dir = m.MultiplyVector(dir);
            cc.Move(dir * Time.deltaTime * speed);
            transform.LookAt(dir + transform.position);

            //gravity
            cc.Move(Vector3.down * Time.deltaTime * gravity);
        }
    }
}
