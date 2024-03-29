using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Square : MonoBehaviour
{

    [SerializeField] GameObject[] cubes;
    SpriteRenderer[] cubeSRs;
    Transform[] cubeTs;


    public AnimationCurve bounceCurve;
    void Start()
    {
        //初始化
        cubeSRs=new SpriteRenderer[cubes.Length];
        cubeTs=new Transform[cubes.Length];
        for (int i = 0; i < cubes.Length; i++)
        {
            cubeSRs[i]=cubes[i].GetComponent<SpriteRenderer>();
            cubeTs[i] = cubes[i].GetComponent<Transform>();
        }

        //Sp = GetComponent<SpriteRenderer>();
        EventCenter.Instance.AddEventListener<int>("节拍",(o) => squareColorChange(o));
        EventCenter.Instance.AddEventListener<int>("Tap判定", (o) => squareSizeChange(o));
    }

    void squareColorChange(int num)
    {
        switch(num)
        {
            case 0:
                for(int i = 0;i<cubes.Length;i++)
                {
                    cubeSRs[i].color = Color.white;
                }
                break;
            case 1:
                cubeSRs[0].color = Color.red; break;  
            case 2:
                cubeSRs[1].color = Color.red; break;
            case 3:
                cubeSRs[2].color = Color.red; break;

        }
    }
    void squareSizeChange(int t)
    {
        if (t != 0) 
        { 
            foreach(Transform cube in cubeTs)
            { 
                StartCoroutine(Bounce(cube));
            } 
        }
        
    }

    IEnumerator Bounce(Transform rt)
    {
        float durduration = 0.3f;//持续时长
        float timer=0;
        while (timer < 1)
        {
            rt.localScale = Vector3.one*bounceCurve.Evaluate(timer);
            timer += Time.deltaTime/durduration;
            yield return null;
        }
    }
}
