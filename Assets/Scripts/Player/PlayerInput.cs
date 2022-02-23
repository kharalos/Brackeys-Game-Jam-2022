using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool SwitchToAzerty;
    public bool Up { get; private set; }
    public bool Down { get; private set; }
    public bool Left { get; private set; }
    public bool Right { get; private set; }
    public bool Interact { get; private set; }
    public bool ControlCamera { get; private set; }

    private void Update() {
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
}
