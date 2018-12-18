using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderIcon : MonoBehaviour
{
    [SerializeField] GameObject defenderPrefab;
    public Color32 unselectedColor;

    public GameObject GetDefenderPrefab() { return defenderPrefab; }

}
