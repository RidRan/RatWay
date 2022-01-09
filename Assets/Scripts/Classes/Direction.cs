using System;
using System.Collections.Generic;
using UnityEngine;

public class Direction : Word
{
    public Vector2 vector;

    public Direction(string w, Vector2 v) 
        : base(w)
    {
        vector = v;
    }
}
