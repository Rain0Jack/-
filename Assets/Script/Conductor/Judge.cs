using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge : MonoBehaviour//单键判定
{
    float songposition;

    public List<float> TapData;//目前只做了单轨的
    public List<float> TapOnJudge;
    public enum Judgement
    {
        Miss,
        Bad,
        Great,
        Prefect       
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            TapData.Add(Conductor.instance.crotchet*(4*i+3));
        }
        //EventCenter.Instance.AddEventListener<int>("节拍",(o)=>addJudge(o));
    }
    void Update()
    {
        songposition = Conductor.instance.songposition;
        TapJudge(LeftKeyInput()+RightKeyInput());
        /*if (Input.GetKeyDown(KeyCode.Space) && onJudge && songposition<=currentNote+0.25)
        {
            //积分系统
            Debug.Log(judge(songposition-currentNote));
            onJudge = false;
        }
        else if (songposition > currentNote + 0.25&&onJudge)
        {
            //Miss
            Debug.Log(Judgement.Miss);
            onJudge = false;
        }*/
    }
    int LeftKeyInput()
    {
        int i=0;
        i += Input.GetKeyDown(KeyCode.D) ? 1 : 0;
        i += Input.GetKeyDown(KeyCode.H) ? 1 : 0;
        return i;
    }

    int RightKeyInput()
    {
        int i = 0;
        i += Input.GetKeyDown(KeyCode.J) ? 1 : 0;
        i += Input.GetKeyDown(KeyCode.K) ? 1 : 0;
        return i;
    }
    void TapJudge(int input)
    {
        if (TapData.Count>0&&songposition >= TapData[0] - 0.25 )//进入判定区间
        {
            Debug.Log(songposition);
            while( songposition >= TapData[0] - 0.25)//在判定列表中加载
            {
                TapOnJudge.Add(TapData[0]);
                TapData.RemoveAt(0);

                if (TapData.Count == 0) { break; }
            }
        }
        else if (TapOnJudge.Count>0&&songposition > TapOnJudge[0] + 0.25)//出判定区间
        {
            while (songposition > TapOnJudge[0] + 0.25)//在判定列表中移除
            {
                TapOnJudge.RemoveAt(0);
                EventCenter.Instance.EventTrigger("Tap判定",0);
                Debug.Log(Judgement.Miss);
                if (TapOnJudge.Count == 0) { break; }
            }
        }
        if (input > 0 && TapOnJudge.Count>0)//按下
        {
            
            for (int i = 0; i < input; i++)
            {    
                float offset = songposition - TapOnJudge[0];
                EventCenter.Instance.EventTrigger("Tap判定",(int)judge(offset));
                if (TapOnJudge.Count > 0) { TapOnJudge.RemoveAt(0); }                
                Debug.Log(judge(offset));
            }
        }
    }

    Judgement judge(float offset)//偏移（songposition-tap）
    {
        //点击偏移判定
        if (offset < -0.25) { return Judgement.Bad; }
        else if ((offset >=-0.25&&offset<-0.067)||(offset>0.067&&offset<=0.25)) { return Judgement.Great;}
        else if (offset >=-0.067&&offset<=0.067) {  return Judgement.Prefect; }
        else { return Judgement.Miss; }
    }
}
 