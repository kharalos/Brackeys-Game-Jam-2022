using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    public Transform target;

    public float targetHeight = 1.7f;
    public float distance = 5.0f;

    public float maxDistance = 20;
    public float minDistance = .6f;
    //public float speedDistance = 5;

    public float xSpeed = 200.0f;
    public float ySpeed = 200.0f;

    public int yMinLimit = -40;
    public int yMaxLimit = 80;

    public int zoomRate = 40;

    public float zoomDampening = 5.0f;

    [SerializeField] float xDeg = 0.0f;
    [SerializeField] private float yDeg = 0.0f;
    private float currentDistance;
    private float desiredDistance;
    private float correctedDistance;

    void Start() {
        //Vector3 angles = transform.eulerAngles;
        //xDeg = angles.x;
        //yDeg = angles.y;

        currentDistance = distance;
        desiredDistance = distance;
        correctedDistance = distance;

        // Make the rigid body not change rotation
        if (this.gameObject.GetComponent<Rigidbody>())
            this.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
    }

    /**
     * Camera logic on LateUpdate to only update after all character movement logic has been handled.
     */
    void LateUpdate() {
        Vector3 vTargetOffset;

        // Don't do anything if target is not defined
        if (!target)
            return;

        // If either mouse buttons are down, let the mouse govern camera position
        if (GUIUtility.hotControl == 0) {
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) {
                xDeg += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                yDeg -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            }
        }

        // calculate the desired distance
        //desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
        desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate;
        desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);

        yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit);

        // set camera rotation
        Quaternion rotation = Quaternion.Euler(yDeg, xDeg, 0);
        correctedDistance = desiredDistance;

        // calculate desired camera position
        vTargetOffset = new Vector3(0, -targetHeight, 0);
        Vector3 position = target.position - (rotation * Vector3.forward * desiredDistance + vTargetOffset);


        // For smoothing, lerp distance if correctedDistance is more than currentDistance
        //currentDistance = correctedDistance > currentDistance ? Mathf.Lerp(currentDistance, correctedDistance, Time.deltaTime * zoomDampening) : correctedDistance;
        currentDistance = Mathf.Lerp(currentDistance, correctedDistance, Time.deltaTime * zoomDampening);

        // keep within legal limits
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        // recalculate position based on the new currentDistance
        position = target.position - (rotation * Vector3.forward * currentDistance + vTargetOffset);

        transform.rotation = rotation;
        transform.position = position;
    }

    private static float ClampAngle(float angle, float min, float max) {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
