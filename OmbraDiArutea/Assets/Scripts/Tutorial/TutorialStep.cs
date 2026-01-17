using System;
using System.Collections.Generic;
using UnityEngine;

namespace OmbreDiAretua
{
    
public abstract class TutorialStep : MonoBehaviour
{
    public Action OnEndTutorialStep;
    
    public abstract void InitTutorialStep();

    public abstract void UnlcokStep();
}
}
