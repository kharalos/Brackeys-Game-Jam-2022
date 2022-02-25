using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static bool SwitchToAzerty;
    public static bool Up { get; private set; }
    public static bool Down { get; private set; }
    public static bool Left { get; private set; }
    public static bool Right { get; private set; }
    public static bool InteractKeyDown { get; private set; }
    public static bool InteractKey { get; private set; }
    public static bool InteractKeyUp { get; private set; }
    public static bool ControlCamera { get; private set; }

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

        InteractKeyDown = (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse1)) ? true : false;
        InteractKey = (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Mouse1)) ? true : false;
        InteractKeyUp = (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Mouse1)) ? true : false;
        ControlCamera = (Input.GetKey(KeyCode.Mouse0)) ? true : false;
    }
}
