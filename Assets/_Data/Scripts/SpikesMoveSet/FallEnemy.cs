using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class FallEnemy : MonoBehaviour
{
    [SerializeField] private AudioSource _FallSfx;
    [SerializeField] private Animator[] _Enemy;

    private int ChanceToFall;


    private bool inTrigger = false;
    private bool IsChooseNumber = false;

    private void Awake()
    {
        IsChooseNumber = false;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            FallSpike();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inTrigger = false;
    }

    void FallSpike()
    {
        if (IsChooseNumber == false)
        {
            ChanceToFall = Random.Range(0, 11);
            IsChooseNumber = true;
        }
        if (ChanceToFall > 5 && IsChooseNumber == true)
        {
            UnityEngine.Debug.Log(ChanceToFall);
            _FallSfx.Play();
            _Enemy[0].Play("FallSpike1");
        }
    }
    
}
