using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Gravestone")
        {
            animator.SetTrigger("jumpTrigger");
        }
        else if (other.gameObject.GetComponent<Defender>())
        {
            GetComponent<Attacker>().SetAttackState(other.gameObject);
        }
    }    

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Defender>())
        {
            GetComponent<Attacker>().SetAttackState(null);
        }
    }
}
