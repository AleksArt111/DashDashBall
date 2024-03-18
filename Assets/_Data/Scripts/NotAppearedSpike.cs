using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotAppearedSpike : MonoBehaviour
{
    [SerializeField] private GameObject[] Spikes;

    private int ChanceToFall;

    private void Awake()
    {
        ChanceToFall = Random.Range(0,11);
    }

    void Update()
    {
        if (ChanceToFall > 5)
        {
            Spikes[0].SetActive(false);
        }
        else
        {
            Spikes[1].SetActive(false);
        }
    }
}
