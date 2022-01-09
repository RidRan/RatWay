using System;
using System.Collections.Generic;
using UnityEngine;

public class Direction : Word
{
    public Vector2 vector;

    public Direction(List<string> s, string d, Vector2 v) 
        : base(s, d)
    {
        vector = v;
    }
}
