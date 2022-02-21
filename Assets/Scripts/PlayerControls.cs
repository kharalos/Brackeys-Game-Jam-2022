using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float forwardSpeed = 5;
    [SerializeField] float backSpeed = 2;
    [SerializeField] float rotationSpeed = 180;
    [SerializeField] float gravity = 20;
    
    public bool SwitchToAzerty;
    public bool Up { get; private set; }
    public bool Down { get; private set; }
    public bool Left { get; private set; }
    public bool Right { get; private set; }
    public bool Interact { get; private set; }
    public bool ControlCamera { get; private set; }

    private CharacterController cc;

    private void Awake() {
        cc = GetComponent<CharacterController>();
    }

    private void Update() {
        //Handle input
        {
            if (SwitchToAzerty) {
                Up = (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow)) ? true : false;
                Down = (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) ? true : false;
                Left = (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow)) ? true : false;
                Right = (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) ? true : false;
            }
            else {
                Up = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) ? true : false;
                Down = (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) ? true : false;
                Left = (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) ? true : false;
                Right = (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) ? true : false;
            }

            Interact = (Input.GetKeyDown(KeyCode.E)) ? true : false;
            ControlCamera = (Input.GetKey(KeyCode.Mouse1)) ? true : false;
        }

        //Handle movement
        {
            Vector3 camRotation = new Vector3(0, Camera.main.transform.rotation.eulerAngles.y, 0);
            Matrix4x4 m = Matrix4x4.Rotate(Quaternion.Euler(camRotation));

            if (!ControlCamera) {
                if (Up) {
                    Vector3 dir = m.MultiplyVector(Vector3.forward);
                    cc.Move(dir * forwardSpeed * Time.deltaTime);
                }
                else if (Down) {
                    Vector3 dir = m.MultiplyVector(Vector3.back);
                    cc.Move(dir * backSpeed * Time.deltaTime);
                }
                if (Left) transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
                if (Right) transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
            }
            else {
                Vector3 dir = Vector3.zero;
                if (Up) dir += new Vector3(0, 0, 1);
                if (Left) dir += new Vector3(-1, 0, 0);
                if (Down) dir += new Vector3(0, 0, -1);
                if (Right) dir += new Vector3(1, 0, 0);
                dir = dir.normalized;
                dir = m.MultiplyVector(dir);

                if (Down) cc.Move(dir * backSpeed * Time.deltaTime);
                else cc.Move(dir * forwardSpeed * Time.deltaTime);

                transform.rotation = Quaternion.Euler(camRotation);
            }

            //gravity
            cc.Move(Vector3.down * Time.deltaTime * gravity);
        }
    }

    ///--------------------
    void AlternateControls () {
        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) dir += new Vector3(0, 0, 1);
        if (Input.GetKey(KeyCode.A)) dir += new Vector3(-1, 0, 0);
        if (Input.GetKey(KeyCode.S)) dir += new Vector3(0, 0, -1);
        if (Input.GetKey(KeyCode.D)) dir += new Vector3(1, 0, 0);


        Vector3 rotation = new Vector3(0, Camera.main.transform.rotation.eulerAngles.y, 0);
        Matrix4x4 m = Matrix4x4.Rotate(Quaternion.Euler(rotation));
        dir = m.MultiplyVector(dir);
        transform.position += dir * Time.deltaTime * forwardSpeed;
        transform.LookAt(dir + transform.position);
    }
}
