using UnityEngine;

public class OverlapBox : MonoBehaviour {

    [SerializeField] LayerMask layerMask;
    [SerializeField] Vector3 boxSize;

    public Collider[] GetColliders() {
        return Physics.OverlapBox(transform.position, boxSize, transform.rotation, layerMask);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(transform.localPosition, boxSize*2);
    }
}