using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEventInfo
{

}

/// <summary>
/// 泛型观察者
/// </summary>
/// <typeparam name="T"></typeparam>
public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;

    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}


/// <summary>
/// 一般观察者
/// </summary>

public class EventInfo : IEventInfo
{
    public UnityAction actions;

    public EventInfo(UnityAction action)
    {
        actions += action;
    }

    
   
}

public class EventCenter
{
    
    private static EventCenter instance;
    public static EventCenter Instance { get => instance ?? (instance = new EventCenter()); }
    //单例模式

    /// <summary>
    /// key:事件名  IEventInfo:事件
    /// </summary>
    private static Dictionary<string , IEventInfo> eventDic = new Dictionary<string , IEventInfo>();
   
    public void AddEventListener<T>(string name , UnityAction<T> action)
    {
        if(eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions += action;
        }
        else
        {
            eventDic.Add(name , new EventInfo<T>(action));
        }
    }

    public void AddEventListener(string name , UnityAction action)
    {
        if(eventDic.ContainsKey(name)) 
        {
            (eventDic[name] as EventInfo).actions += action;
        }
        else
        {
            eventDic.Add(name , new EventInfo(action));
        }
    }


    public void RemoveEventListener<T>(string name , UnityAction<T> action)
    {
        if(eventDic.ContainsKey(name)) 
        {
            (eventDic[name] as EventInfo<T>).actions -= action;
        }
        else
        {
            Debug.LogWarning("事件不存在，无法删除");
        }
    }

    public void RemoveEventListener(string name , UnityAction action)
    {
        if(eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions -= action;
        }
    }

    public void EventTrigger<T>(string name ,T info)
    {
        if(eventDic.ContainsKey(name)) 
        {
            if ((eventDic[name] as EventInfo<T>).actions!= null)
            {
                (eventDic[name] as EventInfo<T>).actions.Invoke(info);
            }
        }
    }

    public void EventTrigger(string name)
    {
        if (eventDic.ContainsKey(name))
        {
            if ((eventDic[name] as EventInfo).actions != null)
            {
                (eventDic[name] as EventInfo).actions.Invoke();
            }
        }
    }

    public void Clear()
    {
        eventDic.Clear();   
    }
}



