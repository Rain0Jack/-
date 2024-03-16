using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Square : MonoBehaviour
{
    public AnimationCurve curve;
    void Start()
    {
        Conductor.instance.beat += square;
    }

    void square(int num)
    {
        if (num % 4 == 0) { StartCoroutine(Conductor.instance.Scale(transform, 1, curve)); }
        
    }
}
