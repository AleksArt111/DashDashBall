using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject _player;
    [SerializeField] private AudioSource _DeathSFX;


    public ParticleSystem pSyst;
    public GameObject[] TrailPlayer;
    public GameObject[] Skins;
    

    private Manager gameAI;


    bool IsTouch;
    bool CanTouch = true;

    public bool IsDead = false;


    private Touch theTouch;


    private void Awake()
    {
        gameAI = FindAnyObjectByType<Manager>();
    }

    void Update()
    {
        Gameplay();
    }

    void Gameplay()
    {
        if (Input.touchCount == 1)
        {
            theTouch = Input.GetTouch(0);
            if(theTouch.phase == TouchPhase.Began)
            {
                if (IsTouch == false)
                {
                    for (int i = 0; i< TrailPlayer.Length; i++)
                    {
                        TrailPlayer[i].SetActive(false);
                    }
                    rb.gravityScale *= -1;
                    IsTouch = true;
                    CanTouch = false;
                    StartCoroutine("Timer");            
                }

                if (IsTouch == true)
                {
                    for (int i = 0; i < TrailPlayer.Length; i++)
                    {
                        TrailPlayer[i].SetActive(false);
                    }
                    rb.gravityScale *= 1;
                    IsTouch = false;
                    CanTouch = false;
                    StartCoroutine("Timer");    
                }
            }
        }

        else if (Input.GetButtonDown("Fire1"))
        {
            if (IsTouch == false)
            {
                rb.gravityScale *= -1;
                IsTouch = true;
            }
            else if (IsTouch == true)
            {
                rb.gravityScale *= 1;
                IsTouch = false;
            }
        }
    }
   
    public void Death()
    {
        pSyst.Play();
        _DeathSFX.Play();
        StartCoroutine("KillPlayer");
        IsDead = true;
    }

   public void Skin1()
   {
        Skins[0].SetActive(true);
        Skins[1].SetActive(false);
   }
    
   public void Skin2()
   {
        Skins[0].SetActive(false);
        Skins[1].SetActive(true);
   }

    IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(0.005f);
        for (int i = 0; i < TrailPlayer.Length; i++)
        {
            TrailPlayer[i].SetActive(true);
        }
        CanTouch = true;
    }

    IEnumerator KillPlayer()
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < TrailPlayer.Length; i++)
        {
            TrailPlayer[i].SetActive(false);
        }
        Destroy(_player);
    }

  
}
