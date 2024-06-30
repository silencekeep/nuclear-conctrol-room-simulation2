using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gSingleton<T> where T : class, new()
{
    private static T _instance;
    private static readonly object syslock = new object();

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (syslock)
                {
                    if (_instance == null)
                    {
                        _instance = new T();
                    }
                }
            }
            return _instance;
        }
    }
}
