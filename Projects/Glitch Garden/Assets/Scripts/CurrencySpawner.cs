using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencySpawner : MonoBehaviour
{

    [SerializeField] GameObject currency;
    [SerializeField] [Range(3, 10)] float horizontalSpeed;
    [SerializeField] [Range(0, 3)]  float verticalSpread;

    private void Start()
    {
        SpawnCurrency();
        StartCoroutine(SpawnCurrencyRandomly());
    }

    void SpawnCurrency()
    {
        GameObject newCoin = Instantiate(currency, transform.position + Vector3.up, Quaternion.identity);
        Vector3 randomVec = new Vector3(Random.Range(0.3f, horizontalSpeed), Random.Range(0.1f, verticalSpread), 0);
        newCoin.GetComponent<Rigidbody2D>().AddForce(randomVec, ForceMode2D.Impulse);
        Destroy(newCoin, 2);
    }

    IEnumerator SpawnCurrencyRandomly()
    {
        while (gameObject)
        {
            SpawnCurrency();
            float delay = Random.Range(5, 13);
            yield return new WaitForSeconds(delay);
        }
    }

}
