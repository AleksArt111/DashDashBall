using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMove : MonoBehaviour
{   
    void FixedUpdate()
    {
        StartCoroutine("Dead");
        gameObject.transform.Translate(-20 * Time.deltaTime, 0, 0);
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
