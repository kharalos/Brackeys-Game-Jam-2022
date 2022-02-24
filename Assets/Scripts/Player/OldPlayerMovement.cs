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
    private PlayerInput input;

    private void Awake() {
        cc = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
    }

    private void Update() {

        //Handle movement
        {
            Vector3 camRotation = new Vector3(0, Camera.main.transform.rotation.eulerAngles.y, 0);
            Matrix4x4 m = Matrix4x4.Rotate(Quaternion.Euler(camRotation));

            if (!input.ControlCamera) {
                if (input.Up) {
                    Vector3 dir = m.MultiplyVector(Vector3.forward);
                    cc.Move(dir * forwardSpeed * Time.deltaTime);
                }
                else if (input.Down) {
                    Vector3 dir = m.MultiplyVector(Vector3.back);
                    cc.Move(dir * backSpeed * Time.deltaTime);
                }
                if (input.Left) transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
                if (input.Right) transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
            }
            else {
                Vector3 dir = Vector3.zero;
                if (input.Up) dir += new Vector3(0, 0, 1);
                if (input.Left) dir += new Vector3(-1, 0, 0);
                if (input.Down) dir += new Vector3(0, 0, -1);
                if (input.Right) dir += new Vector3(1, 0, 0);
                dir = dir.normalized;
                dir = m.MultiplyVector(dir);

                if (input.Down) cc.Move(dir * backSpeed * Time.deltaTime);
                else cc.Move(dir * forwardSpeed * Time.deltaTime);

                transform.rotation = Quaternion.Euler(camRotation);
            }

            //gravity
            cc.Move(Vector3.down * Time.deltaTime * gravity);
        }
    }
}
