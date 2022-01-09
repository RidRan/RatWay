using System;
using System.Collections.Generic;
using UnityEngine;


public class SewerRoom
{
	const int NUM_CONNECTIONS = 8;

	public Vector2 position;

	public SewerRoom[] connections;

	public SewerRoom(int x, int y)
	{
		position = new Vector2(x, y);
		connections = new SewerRoom[NUM_CONNECTIONS];
	}

	public SewerRoom this[int index]
    {
		get => this.connections[index];
		set => this.connections[index] = value;
    }

	public void Clear()
    {
		for (int i = 0; i < NUM_CONNECTIONS; i++)
        {
			this[i] = null;
        }
    }
	
	public SewerRoom Next()
    {
		for (int i = 0; i < NUM_CONNECTIONS; i++)
        {
			if (this[i] != null)
            {
				return this[i];
            }
        }

		return null;
    }

	public SewerRoom Next(int prev)
    {
		for (int i = 0; i < NUM_CONNECTIONS; i++)
        {
			if (i != prev && this[i] != null)
            {
				return this[i];
            }
        }

		return null;
    }

	public int NumConnections()
    {
		int total = 0;
		for (int i = 0; i < NUM_CONNECTIONS; i++)
        {
			if (this[i] != null)
            {
				total++;
            }
        }

		return total;
    }

	public static int Opposite(int index)
    {
		index += NUM_CONNECTIONS / 2;
		if (index > NUM_CONNECTIONS - 1)
        {
			index -= NUM_CONNECTIONS;
        }

		return index;
    }

	public float Distance(SewerRoom sr)
    {
		return Vector2.Distance(this.position, sr.position);
    }

	public bool Adjacent(SewerRoom sr)
    {
		return this.Distance(sr) < 1.5f;
    }

	public static Vector2 IndexToVector(int index)
    {
		List<Vector2> directions = new List<Vector2>();
		directions.Add(new Vector2(0, 1));
		directions.Add(new Vector2(1, 1));
		directions.Add(new Vector2(1, 0));
		directions.Add(new Vector2(1, -1));
		directions.Add(new Vector2(0, -1));
		directions.Add(new Vector2(-1, -1));
		directions.Add(new Vector2(-1, 0));
		directions.Add(new Vector2(-1, 1));

		Vector2 direction = directions[index];
		directions.Clear();

		return direction;
	}

	public static int VectorToIndex(Vector2 vector)
    {
		List<Vector2> directions = new List<Vector2>();
		directions.Add(new Vector2(0, 1));
		directions.Add(new Vector2(1, 1));
		directions.Add(new Vector2(1, 0));
		directions.Add(new Vector2(1, -1));
		directions.Add(new Vector2(0, -1));
		directions.Add(new Vector2(-1, -1));
		directions.Add(new Vector2(-1, 0));
		directions.Add(new Vector2(-1, 1));

		for (int i = 0; i < directions.Count; i++)
        {
			if (directions[i] == vector)
            {
				directions.Clear();
				return i;
            }
        }

		return -1;		
	}

	public bool Connect(SewerRoom sr)
    {
		if (this.Adjacent(sr))
        {
			Vector2 diff = sr.position - this.position;

			for (int i = 0; i < 8; i++)
            {
				if (diff == IndexToVector(i))
                {
					this[i] = sr;
					sr[Opposite(i)] = this;
					break;
                }
            }

			return true;
        }
		else
        {
			return false;
        }
    }
}
