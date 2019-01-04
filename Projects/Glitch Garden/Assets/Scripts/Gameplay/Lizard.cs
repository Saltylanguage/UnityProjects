using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Defender>())
        {
            GetComponent<Attacker>().SetAttackState(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("exit trigger");

        if (!other.gameObject)
        {
            GetComponent<Attacker>().SetAttackState(null);
        }

    }

}
