using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    Quaternion closedRot, openRot, targetRot;
    [SerializeField] Transform door;
    // Start is called before the first frame update
    void Start()
    {
        closedRot = door.rotation;
        openRot = Quaternion.Euler(door.eulerAngles.x, door.eulerAngles.y , door.eulerAngles.z + 90f);
        targetRot = closedRot;
    }

    // Update is called once per frame
    void Update()
    {
        door.rotation = Quaternion.Lerp(door.rotation, targetRot, 3f * Time.deltaTime);
    }
    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Customer")){
            targetRot = openRot;
        }
    }
    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Customer")){
            targetRot = closedRot;
        }
    }
}
