using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSeparator : MonoBehaviour {

    Rigidbody mRigidbody;
    [SerializeField] float mRadialForce = 100f;

    private void Start()
    {
        mRigidbody = GetComponent<Rigidbody>();
        Time.timeScale = 3;
    }

    private void OnCollisionStay(Collision collision)
    {
        var otherPos = collision.collider.transform.position;

        Vector3 toCollider = otherPos - transform.position;
        float distanceSqr = Vector3.Magnitude(toCollider);

        if (Mathf.Approximately(distanceSqr, 0) || Mathf.Approximately(mRigidbody.velocity.magnitude, 0))
        {
            mRigidbody.AddForce(Vector3.right * mRadialForce, ForceMode.Impulse);
            collision.rigidbody.AddForce(Vector3.left * mRadialForce, ForceMode.Impulse);
        }
        else
        {
            Vector3 directionToCol = Vector3.Normalize(toCollider);
            mRigidbody.AddForce(-directionToCol * (1.5f / distanceSqr) * mRadialForce, ForceMode.Impulse);
            collision.rigidbody.AddForce(directionToCol * (1.5f / distanceSqr) * mRadialForce, ForceMode.Impulse);
        }
    }

}
