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
    public int allBeatNumber;//总节拍数
    public AudioSource music;
    public AnimationCurve ease;
    public static Conductor instance;

    private int trueBeatNumber;  //每小节的节拍
    private int everyNumber; //小节数

    private void Awake()
    {
        Application.targetFrameRate = 60;
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
        bpm = 120;
        float normalTime = (float)AudioSettings.dspTime;
        music.PlayScheduled(normalTime+3f);
        dsptimesong = (float)AudioSettings.dspTime;
        crotchet = 60 / bpm;
        allBeatNumber = 0;
        Debug.LogWarning(AudioSettings.dspTime);
    }

    // Update is called once per frame
    void Update()
    {
        //乐曲位置
        songposition = (float)(AudioSettings.dspTime - (dsptimesong+3f)) * music.pitch - offset;
        Debug.Log(songposition);
        
        if (songposition > allBeatNumber * crotchet)
        {
            allBeatNumber ++;
            trueBeatNumber++;
            if(trueBeatNumber == 4) 
            {
                everyNumber ++;
                trueBeatNumber = 0;
            }
            EventCenter.Instance.EventTrigger<int>("小节", everyNumber);
            EventCenter.Instance.EventTrigger<int>("节拍", trueBeatNumber);
        }
        
    }


}
