using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class World
{

    /// <summary>
    /// 逻辑存储所以的类
    /// </summary>
    private static Dictionary<string, ILogicBehaviour> _logicBehaviours = new Dictionary<string, ILogicBehaviour>();

    /// <summary>
    /// 数据层存储所以的类
    /// </summary>
    private static Dictionary<string, IDattaBehaviour> _dattaBehaviours = new Dictionary<string, IDattaBehaviour>();

    /// <summary>
    ///消息层存储所以的类
    /// </summary>
    private static Dictionary<string, IMsgBehaviour> _msgBehaviours = new Dictionary<string, IMsgBehaviour>();

    /// <summary>
    /// 世界构建触发
    /// </summary>
    public virtual void OnCreate()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public virtual void OnUpdate()
    {
    }

    /// <summary>
    /// 世界摧毁是调用
    /// </summary>
    public virtual void OnDestory()
    {
        
    }

    /// <summary>
    /// 世界摧毁后调用
    /// </summary>
    public virtual void OnDestoryPostProcess(object args)
    {
        
    }
    
    
    /// <summary>
    /// 获取逻辑层控制器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetExitsLogicCtrl<T>()where T: ILogicBehaviour
    {
        ILogicBehaviour logic = null;
        
        if (_logicBehaviours.TryGetValue(typeof(T).Name,out  logic))
        {
            return (T)logic;
        }
        
        Debug.LogError(typeof(T).Name+"没有找到该类");
        //如果字典中为空则返回原来的值
        return default(T);
    }
    
    /// <summary>
    /// 获取数据层管理器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetExitsDataCtrl<T>()where T: IDattaBehaviour
    {
        IDattaBehaviour logic = null;
        
        if (_dattaBehaviours.TryGetValue(typeof(T).Name,out  logic))
        {
            return (T)logic;
        }
        
        Debug.LogError(typeof(T).Name+"没有找到该类");
        //如果字典中为空则返回原来的值
        return default(T);
    }
    
    /// <summary>
    /// 获取消息层控制器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetExitsMsgCtrl<T>()where T: IMsgBehaviour
    {
        IMsgBehaviour logic = null;
        
        if (_msgBehaviours.TryGetValue(typeof(T).Name,out  logic))
        {
            return (T)logic;
        }
        
        Debug.LogError(typeof(T).Name+"没有找到该类");
        //如果字典中为空则返回原来的值
        return default(T);
    }
}
