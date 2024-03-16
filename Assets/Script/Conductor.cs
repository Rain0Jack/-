using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    public float bpm;
    public float crotchet;
    public float songposition;
    //public float deltasongpos;
    //public float lasthit;
    public float offset;//偏移
    public float addoffset;
    public float dsptimesong;//音频开始播放时间
    public int beatnumber;//节拍数
    public AudioSource music;
    public delegate void onBeat(int num);
    public onBeat beat;
    public AnimationCurve ease;
    public static Conductor instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        //音频初始化
        bpm = 140; 
        Thread.Sleep(TimeSpan.FromSeconds(3));
        music.Play();
        dsptimesong = (float)AudioSettings.dspTime;
        crotchet = 60 / bpm;
        beatnumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //乐曲位置
        songposition = (float)(AudioSettings.dspTime - dsptimesong) * music.pitch - offset;
        if (songposition > beatnumber * crotchet)
        {
            beatnumber ++;
            beat(beatnumber);
        }
        
    }

    public IEnumerator Scale(Transform trans,float time,AnimationCurve curve)
    { 
        float timer = 0;
        Vector3 scale = trans.localScale;
        while (timer < 1)
        {
            //Debug.Log(curve.Evaluate(timer));
            trans.localScale = scale*curve.Evaluate(timer);
            timer += Time.deltaTime/time;
            yield return null;
        }

    }
}
