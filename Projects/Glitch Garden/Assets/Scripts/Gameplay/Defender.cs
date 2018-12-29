using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField] int cost = 2;

    public int GetCost() { return cost; }

    public void AddCurrency(int amount)
    {
        FindObjectOfType<EconomyManager>().AddCurrency(amount);
    }

}
