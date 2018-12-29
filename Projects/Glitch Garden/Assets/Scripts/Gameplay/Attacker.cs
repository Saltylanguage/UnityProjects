using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{

    [Range(0, 5)]
    [SerializeField] float currentSpeed = 1.0f;
    [SerializeField] int damage = 1;
    GameObject currentTarget;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
        SetAttackState(currentTarget);
    }

    public void SetMovementSpeed(float speed)
    {
        currentSpeed = speed;
    }

    public void SetAttackState(GameObject target)
    {
        if (target)
        {
            animator.SetBool("isAttacking", true);
            currentTarget = target;
        }
        else
        {
            animator.SetBool("isAttacking", false);
            currentTarget = null;
        }

    }

    public void Attack()
    {
        if (!currentTarget) { return; }
        Health health = currentTarget.GetComponent<Health>();
        if (health)
        {
            health.TakeDamage(damage);
        }
    }
}
