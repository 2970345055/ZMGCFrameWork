using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;
using Assembly = System.Reflection.Assembly;

public class TypeManager
{

    private static IBehaviourExcution mBehaviourExcution;
   
   public static void InitlizateWorldAssemblise(World world,IBehaviourExcution behaviourExcution )
   {
       mBehaviourExcution = behaviourExcution;
      //Unity和我们创建的脚本所在的程序集
       
       Assembly[] assemblies =AppDomain.CurrentDomain.GetAssemblies();

       Assembly worldAssembly = null;
       
       //获取当前脚本运行 的程序集
       foreach (var assemblie in assemblies)
       {
           if (assemblie.GetName().Name=="Assembly-CSharp")
           {
               worldAssembly = assemblie;
               break;
           }
       }

       if (worldAssembly==null)
       {
           Debug.LogError("worldAssembly 为空 请检查创建程序集");
           return;
       }
        
       //获取当前游戏世界的命名空间
       //然后获取该命名空间下的脚本
       //判断当前脚本是否继承 Behaviour  如果继承就是框架框架脚本，就需要维护创建和销毁的任务
       string NameSpace = world.GetType().Namespace;

       Type logicType = typeof(ILogicBehaviour);
       Type msgType = typeof(IDataBehaviour);
       Type dataType = typeof(IMsgBehaviour);
        
       // 获取当前程序集下的所以类
       Type[] typearr = worldAssembly.GetTypes();

       List<TypeOrder> logicBehaviourList = new List<TypeOrder>();
       List<TypeOrder> dataBehaviourList = new List<TypeOrder>();
       List<TypeOrder>msgBehaviourList = new List<TypeOrder>();
       
       foreach (var type in typearr)
       {
           string space = type.Namespace;
           if (type.Namespace==NameSpace)
           {
               if (type.IsAbstract)
               {
                   continue;
               }
               
               if (logicType.IsAssignableFrom(type))
               {  
                   ///获取当前类的执行顺序
                   int order = GetLogicBehaviourOrderIndex(type);
                   TypeOrder typeOrder = new TypeOrder(order,type);
                   logicBehaviourList.Add(typeOrder);

               }else if (dataType.IsAssignableFrom(type))
               {
                   
                   int order = GetDataBehaviourOrderIndex(type);
                   TypeOrder typeOrder = new TypeOrder(order,type);
                   dataBehaviourList.Add(typeOrder);
                   
               }else if (msgType.IsAssignableFrom(type))
               {
                   int order = GetMsgBehaviourOrderIndex(type);
                   TypeOrder typeOrder = new TypeOrder(order,type);
                   msgBehaviourList.Add(typeOrder);

               }
           }
       }
       
       ///list中的类按照order 进行排序  降序 最小的在最前面
       logicBehaviourList.Sort((a,b)=>a.order.CompareTo(b.order));
       dataBehaviourList.Sort((a,b)=>a.order.CompareTo(b.order));
       msgBehaviourList.Sort((a,b)=>a.order.CompareTo(b.order));
       
       //初始化数据层脚本，消息层脚本，逻辑层脚本
       for (int i = 0; i < dataBehaviourList.Count; i++)
       {
           IDataBehaviour data =Activator.CreateInstance(dataBehaviourList[i].type) as IDataBehaviour;
           
           world.AddDataCrtl(data);
       }
       
       for (int i = 0; i < msgBehaviourList.Count; i++)
       {
           IMsgBehaviour msg =Activator.CreateInstance(dataBehaviourList[i].type) as IMsgBehaviour;
           world.AddMsgCrtl(msg);
           
       }
       for (int i = 0; i < logicBehaviourList.Count; i++)
       {
           ILogicBehaviour logic =Activator.CreateInstance(dataBehaviourList[i].type) as ILogicBehaviour;
           
           world.AddLogicCrtl(logic);
           
       }
       
       dataBehaviourList.Clear();
       msgBehaviourList.Clear();
       logicBehaviourList.Clear();
       mBehaviourExcution = null;
   }
   
    /// <summary>
    /// 获取逻基层的脚本执行顺序
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
   public static int GetLogicBehaviourOrderIndex(Type Logictype)
   {
       Type[] logicTypes = mBehaviourExcution.GetLogicBehaviourExcution();

       for (int i = 0; i < logicTypes.Length; i++)
       {
           if (logicTypes[i]==Logictype)
           {
               return i;
           }
       }
       return 999;
   }
    /// <summary>
    /// 获取数据层基层的脚本执行顺序
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static int GetDataBehaviourOrderIndex(Type Datatype)
    {
        Type[] logicTypes = mBehaviourExcution.GetLogicBehaviourExcution();

        for (int i = 0; i < logicTypes.Length; i++)
        {
            if (logicTypes[i]==Datatype)
            {
                return i;
            }
        }
        return 999;
    }
    
    /// <summary>
    /// 获取消息层的脚本执行顺序
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static int GetMsgBehaviourOrderIndex(Type Msgtype)
    {
        Type[] logicTypes = mBehaviourExcution.GetLogicBehaviourExcution();

        for (int i = 0; i < logicTypes.Length; i++)
        {
            if (logicTypes[i]==Msgtype)
            {
                return i;
            }
        }
        return 999;
    }
}
