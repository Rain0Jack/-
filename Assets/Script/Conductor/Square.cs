using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Square : MonoBehaviour
{
    SpriteRenderer Sp;
    [SerializeField] SpriteRenderer[] cubes;
    void Start()
    {
        Sp = GetComponent<SpriteRenderer>();
        EventCenter.Instance.AddEventListener<int>("╫зед",(o)=>square(o));
    }

    void square(int num)
    {
        switch(num)
        {
            case 0:
                for(int i = 0;i<4;i++)
                {
                    cubes[i].color = Color.white;
                }
                break;
            case 1:
                cubes[0].color = Color.red; break;  
            case 2:
                cubes[1].color = Color.red; break;
            case 3:
                cubes[2].color = Color.red; break;
            case 4:
                cubes[3].color = Color.red; break;
        }
    }
}
