using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO change to object pool
public class Currency : MonoBehaviour
{
    [SerializeField] int value = 20;
    [SerializeField] ParticleSystem obtainedVFX;

    public int GetValue() { return value; }

    public void CollectCurrency()
    {
        if (obtainedVFX)
        {
            ParticleSystem newVFX = Instantiate(obtainedVFX, transform.position, Quaternion.identity);
            Destroy(newVFX.gameObject, 1);
        }
        Destroy(gameObject);
    }
}
