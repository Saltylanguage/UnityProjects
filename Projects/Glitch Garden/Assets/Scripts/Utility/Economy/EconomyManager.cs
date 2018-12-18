using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EconomyManager : MonoBehaviour
{
    int currentMoney = 100;
    [SerializeField] Text scoreText;

    private void Start()
    {       
    }

    public int GetCurrentMoney() { return currentMoney; }

    public void AddCurrency(int value)
    {
        currentMoney += value;
        scoreText.text = currentMoney.ToString();
    }

    public void SpendCurrency(int value)
    {
        currentMoney -= value;
        scoreText.text = currentMoney.ToString();
    }
}
