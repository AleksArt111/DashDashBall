using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Manager manager;

    public float ChangeSpeed = 0;
    void Start()
    {
        manager = FindAnyObjectByType<Manager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine("Dead");
        if (manager.timeLived >= 20)
        {
            Invoke("RandomNumber", 1);
            if (ChangeSpeed < 2)
            {
                manager.timeToChangeSpeed = 0;
                gameObject.transform.Translate(-20 * Time.deltaTime, 0, 0);
            }
            else if (ChangeSpeed >= 3 ) 
            {
                manager.timeToChangeSpeed = 0;
                gameObject.transform.Translate(-25 * Time.deltaTime, 0, 0);
            }
            else if( ChangeSpeed  == 5) 
            { 
                gameObject.transform.Translate(-17 * Time.deltaTime, 0, 0);
            }   
        }
        else
        {
            gameObject.transform.Translate(-17 * Time.deltaTime, 0, 0);
        }
         
    }

    void RandomNumber()
    {
        ChangeSpeed = Random.RandomRange(0,6);
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    
}
