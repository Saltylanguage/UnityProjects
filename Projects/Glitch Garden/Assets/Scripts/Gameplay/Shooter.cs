using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [SerializeField] GameObject projectile;
    [SerializeField] GameObject launchPoint;

    Animator animator;
    AttackerSpawner myLaneSpawner;

    private void Start()
    {
        animator = GetComponent<Animator>();
        SetLaneSpawner();
    }

    private void Update()
    {
        if (myLaneSpawner)
        {
            if (IsAttackerInLane())
            {
                animator.SetBool("isAttacking", true);
            }
            else
            {
                animator.SetBool("isAttacking", false);
            }
        }
    }

    void SetLaneSpawner()
    {
        AttackerSpawner[] spawners = FindObjectsOfType<AttackerSpawner>();
        foreach (AttackerSpawner spawner in spawners)
        {
            if (Mathf.Approximately(spawner.transform.position.y, transform.position.y))
            {
                myLaneSpawner = spawner;
                return;
            }
        }
    }

    private bool IsAttackerInLane()
    {
        if (myLaneSpawner.transform.childCount <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void Fire()
    {
        GameObject newProjectile = Instantiate(projectile, launchPoint.transform.position, launchPoint.transform.rotation);
        Destroy(newProjectile, 4);
        return;
    }
}
