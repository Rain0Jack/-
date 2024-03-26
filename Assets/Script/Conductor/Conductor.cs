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
    public float offset;//ƫ��
    public float addoffset;
    public float dsptimesong;//��Ƶ��ʼ����ʱ��
    public int allBeatNumber;//�ܽ�����
    public AudioSource music;
    public AnimationCurve ease;
    public static Conductor instance;

    private int trueBeatNumber;  //ÿС�ڵĽ���
    private int everyNumber; //С����

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
        //��Ƶ��ʼ��
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
        //����λ��
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
            EventCenter.Instance.EventTrigger<int>("С��", everyNumber);
            EventCenter.Instance.EventTrigger<int>("����", trueBeatNumber);
        }
        
    }


}
