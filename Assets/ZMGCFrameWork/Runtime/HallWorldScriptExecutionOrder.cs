using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallWorldScriptExecutionOrder: IBehaviourExcution

{
   private static Type[] logicBehaviorExecutions = new Type[]
   {
       
   };
   
   private static Type[] DataBehaviorExecutions = new Type[]
   {
       
   };
   private static Type[] MsgBehaviorExecutions = new Type[]
   {
       
   };

   public Type[] GetLogicBehaviourExcution()
   {
      return logicBehaviorExecutions;
   }

   public Type[] GetDataBehaviourExcution()
   {
      return DataBehaviorExecutions;
   }

   public Type[] GetMsgBehaviourExcution()
   {
      return MsgBehaviorExecutions;
   }
}
