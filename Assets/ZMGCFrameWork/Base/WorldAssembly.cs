using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class World 
{
    
    /// <summary>
    /// 添加逻辑类
    /// </summary>
    /// <param name="behaviour"></param>
    public  void AddLogicCrtl(ILogicBehaviour behaviour)
    {
        _logicBehaviours.Add(behaviour.GetType().Name,behaviour);
        behaviour.OnCreate();
    }
    /// <summary>
    /// 添加数据类
    /// </summary>
    /// <param name="behaviour"></param>
    public  void AddDataCrtl(IDataBehaviour behaviour)
    {
        _dattaBehaviours.Add(behaviour.GetType().Name,behaviour);
        behaviour.OnCreate();
    }
    /// <summary>
    /// 添加信息类
    /// </summary>
    /// <param name="behaviour"></param>
    public  void AddMsgCrtl(IMsgBehaviour behaviour)
    {
        _msgBehaviours.Add(behaviour.GetType().Name,behaviour);
        behaviour.OnCreate();
    }
}
