using System;
using System.Collections.Generic;
using UnityEngine;

public class Parser
{
	public List<Direction> directions;

	public Parser()
    {
        directions = new List<Direction>();
        List<string> synonyms = new List<string>();
        directions.Add(new Direction(synonyms, 
            "North", new Vector2(0 , 1))
    }
}
