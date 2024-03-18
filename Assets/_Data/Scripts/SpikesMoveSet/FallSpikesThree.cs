using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSpikesThree : MonoBehaviour
{
    [SerializeField] private GameObject[] _Enemy;
    [SerializeField] private AudioSource _FallSFX;

    private int ChanceToFall;
    private int WhatSpikeFall;


    private bool inTrigger = false;
    private bool IsChoosenNumber = false;
    private bool IsChoosenNumberToSpikes = false;

    private void Awake()
    {
        IsChoosenNumber = false;

        WhatSpikeFall = Random.Range(0, 15);

        ChanceToFall = Random.Range(0, 2);

        Debug.Log(WhatSpikeFall);
        Debug.Log(ChanceToFall);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            ThreeSpikesFall();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inTrigger = false;
    }

    void ThreeSpikesFall()
    {
        if (ChanceToFall == 1)
        {
            if (WhatSpikeFall <= 6)
            {
                _FallSFX.Play();
                _Enemy[0].GetComponent<Animator>().Play("FallSpike1");
            }

            else if (WhatSpikeFall > 6)
            {
                _FallSFX.Play();
                _Enemy[1].GetComponent<Animator>().Play("2Spike");
            }

            else if (WhatSpikeFall > 9)
            {
                _FallSFX.Play();
                _Enemy[2].GetComponent<Animator>().Play("1stSpike");
            }

        }
    }
}
