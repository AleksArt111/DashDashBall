using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinClaimer : MonoBehaviour
{
    private Manager _manager;


    private void Awake()
    {
        _manager = FindObjectOfType<Manager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ClaimCoins();
        }
    }

    void ClaimCoins()
    {
        _manager.CollectCoins();
        _manager.money += 10;
        PlayerPrefs.SetInt("Money", _manager.money += 10);
        Destroy(gameObject);
    }
}
