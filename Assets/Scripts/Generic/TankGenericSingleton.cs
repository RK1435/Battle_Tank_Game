using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGenericSingleton<T> : MonoBehaviour where T : TankGenericSingleton<T>
{
    private static T instance;

    public static T Instance { get { return instance; } }

    protected virtual void Awake()
    {
        CreateInstance();
    }

    private void CreateInstance()
    {
        if(instance == null)
        {
            instance = (T)this;
        }
        else
        {
            Destroy(this);
        }
    }
}
