using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlayer : MonoBehaviour
{
    private GameObject _Player;



    private void Awake()
    {
        _Player = GameObject.Find("Player(Clone)");
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerScript>().pSyst.Play();
            collision.gameObject.GetComponent<PlayerScript>().Death();
        }
    }
}
