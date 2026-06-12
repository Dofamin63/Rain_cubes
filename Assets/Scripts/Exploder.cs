using UnityEngine;

public class Exploder : MonoBehaviour
{
    public void Explode(Vector3 position, float radius, float force, GameObject ignoreObject = null)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);
        
        foreach (Collider hit in colliders)
        {
            if (ignoreObject != null && hit.gameObject == ignoreObject)
                continue;

            if (hit.TryGetComponent(out Rigidbody rb))
            {
                rb.AddExplosionForce(force, position, radius);
            }
        }
    }
}