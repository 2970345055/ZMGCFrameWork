using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;
using Assembly = System.Reflection.Assembly;

public class TypeManager 
{
   
   public static void InitlizateWorldAssemblise(World world)
   {
         
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
       Type msgType = typeof(IDattaBehaviour);
       Type dataType = typeof(IMsgBehaviour);
        
       // 获取当前程序集下的所以类
       Type[] typearr = worldAssembly.GetTypes();

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
                   
               }else if (dataType.IsAssignableFrom(type))
               {
                   
               }else if (msgType.IsAssignableFrom(type))
               {
                   
               }
           }
       }
   }
}
