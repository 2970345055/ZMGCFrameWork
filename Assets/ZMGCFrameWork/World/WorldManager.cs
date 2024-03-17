using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldManager
{

    private static List<World> mWorldList = new List<World>();
    
    /// <summary>
    /// 默认游戏世界
    /// </summary>
    public static World DefaultGameWorld
    {
        get;
        
        private set;
    }
    
    /// <summary>
    /// 构建一个world
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void CreateWorld<T>() where T : World, new()
    {
        T world = new T();

        DefaultGameWorld = world;
        
        ///调用初始化的脚本事件
        
        TypeManager.InitlizateWorldAssemblise(world,GetBehaviourExcution(world));
        
        world.OnCreate();
        
        mWorldList.Add(world);
    }
    
    
    public static IBehaviourExcution GetBehaviourExcution(World world)
    {
        if (world.GetType().Name=="HallWorld")
        {
            return new HallWorldScriptExecutionOrder();
        }

        return null;
    }


    /// <summary>
    /// 销毁对应的游戏世界
    /// </summary>
    /// <param name="world"></param>
    /// <typeparam name="T"></typeparam>
    public static void DestoryWorld<T>(World world) where T : World
    {
        for (int i = 0; i < mWorldList.Count; i++)
        {
            if (mWorldList[i]==world)
            {
                mWorldList[i].DestoryWorld(typeof(T).Namespace);
                mWorldList.Remove(mWorldList[i]);
                break;
            }
        }
    }
}
