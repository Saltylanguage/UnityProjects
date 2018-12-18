using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencySpawner : MonoBehaviour
{

    [SerializeField] GameObject currency;
    [SerializeField] [Range(0, 10)] float horizontalSpeed = 1;
    [SerializeField] [Range(0, 3)]  float verticalSpread = 1;

    private void Start()
    {
        SpawnCurrency();
        StartCoroutine(SpawnCurrencyRandomly());
    }

    void SpawnCurrency()
    {
        GameObject newCoin = Instantiate(currency, transform.position + Vector3.up, Quaternion.identity);
        Vector3 randomVec = new Vector3(Random.Range(0, horizontalSpeed), Random.Range(0, verticalSpread), 0);
        newCoin.GetComponent<Rigidbody>().AddForce(randomVec, ForceMode.Impulse);
        Destroy(newCoin, 5f);
    }

    IEnumerator SpawnCurrencyRandomly()
    {
        while (gameObject)
        {
            SpawnCurrency();
            float delay = Random.Range(4, 6);
            yield return new WaitForSeconds(delay);
        }
    }

}
