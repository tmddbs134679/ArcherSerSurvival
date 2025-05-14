using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCanvas : Singleton<WorldCanvas>
{
    public Canvas canvas;

    protected override void Awake()
    {
        base.Awake();
        canvas = GetComponent<Canvas>();
    }
}
