using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace OmbreDiAretua
{
    
public abstract class AutoInteractable : MonoBehaviour
{
    [SerializeField] LayerMask maskToInteract;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((maskToInteract.value & (1 << other.gameObject.layer)) != 0)
        {
            Execute();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ((maskToInteract.value & (1 << other.gameObject.layer)) != 0)
        {
            Close();
        }  
    }

    public abstract void Execute();

    public abstract void Close();
}
}
