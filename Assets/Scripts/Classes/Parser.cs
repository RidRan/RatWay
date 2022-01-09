using System;
using System.Collections.Generic;
using UnityEngine;

public class Parser
{
	public List<Direction> directions;
    public List<Activity> activities;

	public Parser()
    {
        activities = new List<Activity>();
        AddActivities();
        directions = new List<Direction>();
        AddDirections();
    }

    public string Parse(string input)
    {
        string[] inputs = input.Split(' ');

        foreach(Activity a in activities)
        {
            if (a.Match(inputs[0]))
            {
                switch(a.word)
                {
                    case "move":
                        if (inputs.Length > 1)
                        {
                            foreach (Direction d in directions)
                            {
                                if (d.Match(inputs[1]))
                                {
                                    return "move " + d.vector.x + " " + d.vector.y;
                                }
                            }

                            return "move not";
                        }

                        return "move none";
                }
            }
        }

        return "not";
    }

    private void AddActivities()
    {
        Activity move = new Activity("move");
        move.AddSynonym("go");
        move.AddSynonym("walk");
        move.AddSynonym("run");
        activities.Add(move);
    }

    private void AddDirections()
    {
        directions.Add(new Direction("North", new Vector2(0, 1)));
        directions.Add(new Direction("Northeast", new Vector2(1, 1)));
        directions.Add(new Direction("East", new Vector2(1, 0)));
        directions.Add(new Direction("Southeast", new Vector2(1, -1)));
        directions.Add(new Direction("South", new Vector2(0, -1)));
        directions.Add(new Direction("Southwest", new Vector2(-1, -1)));
        directions.Add(new Direction("West", new Vector2(-1, 0)));
        directions.Add(new Direction("Northwest", new Vector2(-1, 1)));
    }
}
