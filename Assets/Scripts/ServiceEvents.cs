using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceEvents
{
    private static ServiceEvents instance;
    public static ServiceEvents Instance
    {
        get
        {
            if (instance == null)
                instance = new ServiceEvents();
            return instance;
        }
    }

    public Action<int> OnEnemyDeath;
    public Action<int> OnShellFired;
}
