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

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exit trigger");

        GetComponent<Attacker>().SetAttackState(null);

    }

}
