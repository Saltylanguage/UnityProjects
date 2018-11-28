using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderIcon : MonoBehaviour
{
    [SerializeField] GameObject defenderPrefab;
    public Color32 unselectedColor;
    DefenderSpawner defenderSpawner;
    SpriteRenderer sr;
    EconomyManager economyManager;

    private void Start()
    {
        defenderSpawner = FindObjectOfType<DefenderSpawner>();
        sr = GetComponent<SpriteRenderer>();
        economyManager = FindObjectOfType<EconomyManager>();
    }

    private void Update()
    {
        if (!defenderSpawner.GetDefender())
        {
            sr.color = unselectedColor;
        }
    }

    private void OnMouseDown()
    {
        var defenderIcons = FindObjectsOfType<DefenderIcon>();
        foreach (DefenderIcon icon in defenderIcons)
        {
            icon.GetComponent<SpriteRenderer>().color = unselectedColor;
        }
        if (economyManager.GetCurrentMoney() >= defenderPrefab.GetComponent<Defender>().GetCost())
        {
            defenderSpawner.SetSelectedDefender(defenderPrefab);
            sr.color = Color.white;
        }
    }

}
