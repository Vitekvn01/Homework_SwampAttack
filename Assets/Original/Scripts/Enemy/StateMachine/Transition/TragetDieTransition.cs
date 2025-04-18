using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TragetDieTransition : Transition
{
    private void Update()
    {
        if (Target == null)
        {
            NeedTranzit = true;  
        }
    }
}
