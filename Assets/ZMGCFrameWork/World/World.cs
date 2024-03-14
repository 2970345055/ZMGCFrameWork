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
    /// 销毁世界
    /// </summary>
    public  void DestoryWorld(string nameSpace,object pars=null)
    {   
        //需要移除的列表
        List<string> needRemoveList = new List<string>();
    
        ///释放数据层的脚本
        foreach (var item in _dattaBehaviours)
        {
            if (string.Equals(item.Value.GetType().Namespace,nameSpace))
            {
                needRemoveList.Add(item.Key);
            }
        }
        
        foreach (var key in needRemoveList)
        {
            _dattaBehaviours[key].OnDestory();
            _dattaBehaviours.Remove(key);
        }
        needRemoveList.Clear();
        
        ///释放逻辑层脚本
        foreach (var item in _logicBehaviours)
        {
            if (string.Equals(item.Value.GetType().Namespace,nameSpace))
            {
                needRemoveList.Add(item.Key);
            }
        }
        
        foreach (var key in needRemoveList)
        {
            _logicBehaviours[key].OnDestory();
            _logicBehaviours.Remove(key);
        }
        needRemoveList.Clear();
        
        ///释放消息层脚本
        foreach (var item in _msgBehaviours)
        {
            if (string.Equals(item.Value.GetType().Namespace,nameSpace))
            {
                needRemoveList.Add(item.Key);
            }
        }

        foreach (var key in needRemoveList)
        {
            _msgBehaviours[key].OnDestory();
            _msgBehaviours.Remove(key);
        }
        needRemoveList.Clear();
        
        OnDestory();
        
        OnDestoryPostProcess(pars);
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
