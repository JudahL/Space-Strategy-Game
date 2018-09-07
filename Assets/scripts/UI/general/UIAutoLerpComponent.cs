using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAutoLerpComponent : UILerpComponent
{
    private void StartLerpCoroutine()
    {
        Toggle();
    }

    protected override void OnComplete()
    {
        StartLerpCoroutine();
    }
}
