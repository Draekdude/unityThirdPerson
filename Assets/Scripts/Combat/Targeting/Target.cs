using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public event Action<Target> OnDestroyed;
    private void OnDestroy()
    {
        //var target = gameObject.GetComponent<Target>();
        OnDestroyed?.Invoke(this);
    }
}
