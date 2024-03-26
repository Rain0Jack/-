using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge : MonoBehaviour//�����ж�
{
    public bool onJudge;
    public float currentNote;
    public enum Judgement
    {
        Bad,
        Great,
        Prefect,
        Miss
    }
    // Start is called before the first frame update
    void Start()
    {
        EventCenter.Instance.AddEventListener<int>("����",(o)=>addJudge(o));
    }
    void Update()
    {
        float songposition = Conductor.instance.songposition;   
        if (Input.GetKeyDown(KeyCode.Space) && onJudge && songposition<=currentNote+0.25)
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
        }
    }

    void addJudge(int num)//����ж�(��ʱ)
    {
        if(num == 3)
        {
            currentNote = (num + 1) * Conductor.instance.crotchet;
            onJudge = true;
        }
    }

    Judgement judge(float offset)
    {
        //���ƫ���ж�
        if (offset < -0.25) { return Judgement.Bad; }
        else if ((offset >=-0.25&&offset<-0.067)||(offset>0.067&&offset<=0.25)) { return Judgement.Great;}
        else if (offset >=-0.067&&offset<=0.067) {  return Judgement.Prefect; }
        else { return Judgement.Miss; }
    }
}
 