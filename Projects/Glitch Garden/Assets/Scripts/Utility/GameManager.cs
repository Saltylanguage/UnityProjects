using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    DefenderSpawner defenderSpawner;
    EconomyManager economyManager;
    CameraRaycaster cameraRaycaster;
    GameObject hitObject;

    void Start()
    {
        defenderSpawner = GetComponent<DefenderSpawner>();
        economyManager = GetComponent<EconomyManager>();
        cameraRaycaster = FindObjectOfType<CameraRaycaster>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {         
            if (cameraRaycaster.validHit)
            {
                HandleMouseClick(cameraRaycaster.layerHit);
            }
        }
    }

    void HandleMouseClick(Layer hitLayer)
    {
        hitObject = cameraRaycaster.hit.transform.gameObject;
        switch (hitLayer)
        {
            case Layer.icons:
                IconClick();
                return;
            case Layer.currency:
                CurrencyClick();
                return;
            case Layer.defenders:
                return;
            case Layer.gameField:
                GameFieldClick();
                return;
            default:
                Debug.Log("Something went wrong [Check Layers]");
                return;
        }

    }

    void IconClick()
    {
        UnselectAllIcons();
        var currentDefender = hitObject.GetComponent<DefenderIcon>().GetDefenderPrefab();
        if (economyManager.GetCurrentMoney() >= currentDefender.GetComponent<Defender>().GetCost())
        {
            defenderSpawner.SetSelectedDefender(currentDefender);
            hitObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    void CurrencyClick()
    {
        Currency currency = hitObject.GetComponent<Currency>();
        currency.CollectCurrency();
        economyManager.AddCurrency(currency.GetValue());
    }

    void GameFieldClick()
    {
        var validDefender = defenderSpawner.GetDefender();
        if (validDefender)
        {
            defenderSpawner.Spawn();
            economyManager.SpendCurrency(defenderSpawner.GetDefender().GetComponent<Defender>().GetCost());
            defenderSpawner.SetSelectedDefender(null);
        }
        UnselectAllIcons();
    }

    void UnselectAllIcons()
    {
        var defenderIcons = FindObjectsOfType<DefenderIcon>();
        foreach (DefenderIcon icon in defenderIcons)
        {
            icon.GetComponent<SpriteRenderer>().color = icon.unselectedColor;
        }
    }
}
