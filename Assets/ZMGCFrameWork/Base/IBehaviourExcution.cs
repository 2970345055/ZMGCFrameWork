using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface  IBehaviourExcution
{
    Type[] GetLogicBehaviourExcution();
    
    Type[] GetDataBehaviourExcution();
    
    Type[] GetMsgBehaviourExcution();
}
