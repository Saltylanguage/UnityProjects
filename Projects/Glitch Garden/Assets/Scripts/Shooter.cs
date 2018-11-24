using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

    [SerializeField] GameObject projectile;
    [SerializeField] GameObject launchPoint;
    void Fire()
    {
        Instantiate(projectile, launchPoint.transform.position, launchPoint.transform.rotation);
        return;
    }
}
