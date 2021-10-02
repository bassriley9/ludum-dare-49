using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public abstract class BaseState 
{
    public BaseState(GameObject gameobject)
    {
        this.gameobject = gameobject;
        this.transform = gameobject.transform;
    }

    protected GameObject gameobject;
    protected Transform transform;

    public abstract Type Tick();
}
