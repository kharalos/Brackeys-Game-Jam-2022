using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps the canvas rotated towards the camera.
/// </summary>
public class FaceCanvasTowardsCamera : MonoBehaviour
{

    private void LateUpdate() {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }
}
