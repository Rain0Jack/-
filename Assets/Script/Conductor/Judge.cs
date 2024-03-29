using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge : MonoBehaviour//�����ж�
{
    float songposition;

    public List<float> TapData;//Ŀǰֻ���˵����
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
        //EventCenter.Instance.AddEventListener<int>("����",(o)=>addJudge(o));
    }
    void Update()
    {
        songposition = Conductor.instance.songposition;
        TapJudge(LeftKeyInput()+RightKeyInput());
        /*if (Input.GetKeyDown(KeyCode.Space) && onJudge && songposition<=currentNote+0.25)
        {
            //����ϵͳ
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
        if (TapData.Count>0&&songposition >= TapData[0] - 0.25 )//�����ж�����
        {
            Debug.Log(songposition);
            while( songposition >= TapData[0] - 0.25)//���ж��б��м���
            {
                TapOnJudge.Add(TapData[0]);
                TapData.RemoveAt(0);

                if (TapData.Count == 0) { break; }
            }
        }
        else if (TapOnJudge.Count>0&&songposition > TapOnJudge[0] + 0.25)//���ж�����
        {
            while (songposition > TapOnJudge[0] + 0.25)//���ж��б����Ƴ�
            {
                TapOnJudge.RemoveAt(0);
                EventCenter.Instance.EventTrigger("Tap�ж�",0);
                Debug.Log(Judgement.Miss);
                if (TapOnJudge.Count == 0) { break; }
            }
        }
        if (input > 0 && TapOnJudge.Count>0)//����
        {
            
            for (int i = 0; i < input; i++)
            {    
                float offset = songposition - TapOnJudge[0];
                EventCenter.Instance.EventTrigger("Tap�ж�",(int)judge(offset));
                if (TapOnJudge.Count > 0) { TapOnJudge.RemoveAt(0); }                
                Debug.Log(judge(offset));
            }
        }
    }

    Judgement judge(float offset)//ƫ�ƣ�songposition-tap��
    {
        //���ƫ���ж�
        if (offset < -0.25) { return Judgement.Bad; }
        else if ((offset >=-0.25&&offset<-0.067)||(offset>0.067&&offset<=0.25)) { return Judgement.Great;}
        else if (offset >=-0.067&&offset<=0.067) {  return Judgement.Prefect; }
        else { return Judgement.Miss; }
    }
}
 