using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
   public static event Action OnFinish;
   private void AnimationFinishTrigger() =>  OnFinish.Invoke();

  
}
