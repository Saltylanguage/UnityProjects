using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DefenderSpawner : MonoBehaviour
{

    GameObject defender;
    [SerializeField] EconomyManager economyManager;

    private void Start()
    {
        economyManager = FindObjectOfType<EconomyManager>();
    }

    private void OnMouseDown()
    {
        if (defender)
        {
            SpawnDefender(GetSquareClicked());
            defender = null;
        }
    }

    public GameObject GetDefender() { return defender; }

    public void SetSelectedDefender(GameObject selected)
    {
        defender = selected;
    }

    private Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        float snappedX = Mathf.Floor(worldPos.x + 0.5f);
        float snappedY = Mathf.Floor(worldPos.y + 0.5f);
        return new Vector2(snappedX, snappedY);
    }

    private void SpawnDefender(Vector2 pos)
    {
        GameObject newDefender = Instantiate(defender.gameObject, pos, Quaternion.identity) as GameObject;
        economyManager.SpendCurrency(newDefender.GetComponent<Defender>().GetCost());
    }
}
