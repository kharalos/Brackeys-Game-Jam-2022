using UnityEngine;
using UnityEngine.Events;

public class PhysicsAddForce : MonoBehaviour
{
    public Vector3 ForceVector;
    public GameObject m_brokenModel;
    public UnityEvent OnBreak;
    public void Push()
    {
        GetComponent<Rigidbody>().AddForce(ForceVector * Time.fixedDeltaTime * 100, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name == "Floor")
        {
            OnBreak.Invoke();
            m_brokenModel.transform.parent = null;
            m_brokenModel.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
