using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class OldPlayerMovement : MonoBehaviour
{
    [SerializeField] float forwardSpeed = 5;
    [SerializeField] float backSpeed = 2;
    [SerializeField] float rotationSpeed = 180;
    [SerializeField] float gravity = 20;

    private CharacterController cc;

    private void Awake() {
        cc = GetComponent<CharacterController>();
    }

    private void Update() {

        //Handle movement
        {
            Vector3 camRotation = new Vector3(0, Camera.main.transform.rotation.eulerAngles.y, 0);
            Matrix4x4 m = Matrix4x4.Rotate(Quaternion.Euler(camRotation));

            if (!PlayerInput.ControlCamera) {
                if (PlayerInput.Up) {
                    Vector3 dir = m.MultiplyVector(Vector3.forward);
                    cc.Move(dir * forwardSpeed * Time.deltaTime);
                }
                else if (PlayerInput.Down) {
                    Vector3 dir = m.MultiplyVector(Vector3.back);
                    cc.Move(dir * backSpeed * Time.deltaTime);
                }
                if (PlayerInput.Left) transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
                if (PlayerInput.Right) transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
            }
            else {
                Vector3 dir = Vector3.zero;
                if (PlayerInput.Up) dir += new Vector3(0, 0, 1);
                if (PlayerInput.Left) dir += new Vector3(-1, 0, 0);
                if (PlayerInput.Down) dir += new Vector3(0, 0, -1);
                if (PlayerInput.Right) dir += new Vector3(1, 0, 0);
                dir = dir.normalized;
                dir = m.MultiplyVector(dir);

                if (PlayerInput.Down) cc.Move(dir * backSpeed * Time.deltaTime);
                else cc.Move(dir * forwardSpeed * Time.deltaTime);

                transform.rotation = Quaternion.Euler(camRotation);
            }

            //gravity
            cc.Move(Vector3.down * Time.deltaTime * gravity);
        }
    }
}
