using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationForBall : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, 0, 10);
    }
}
