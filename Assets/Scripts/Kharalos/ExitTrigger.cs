using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Staff") || other.CompareTag("Customer")){
            Debug.Log(other.name + " has ran away");
            Destroy(other.gameObject);
            GameManager.Instance.currentCustomerCount -= 1;
        }
    }
}
