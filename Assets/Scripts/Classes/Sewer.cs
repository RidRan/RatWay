using System;
using System.Collections.Generic;
using UnityEngine;


public class Sewer
{
	public SewerRoom[,] rooms;

	public Vector2 size;

	public Sewer(int w, int h)
	{
		size = new Vector2(w, h);
		rooms = new SewerRoom[w, h];
		for (int i = 0; i < h; i++)
        {
			for (int j = 0; j < w; j++)
            {
				rooms[i, j] = new SewerRoom(j - w / 2, i - h / 2);
            }
        }
	}

	public SewerRoom this[int x, int y]
    {
		get => this.rooms[x, y];
		set => this.rooms[x, y] = value;
    }

	public SewerRoom At(Vector2 pos)
    {
		if (pos.x < size.x / 2 && pos.x > -size.x/2 && pos.y < size.y/2 && pos.y > -size.y/2)
        {
			return this[(int) (pos.x + size.x / 2), (int) (pos.y + size.y / 2)];
        }

		return null;
    }

	public SewerRoom At(SewerRoom sr, Vector2 v)
    {
		return At(sr.position + v);
    }

	public void Generate(SewerRoom origin, int prevIndex, int length, int branches)
    {
		if (length == 0)
        {
			return;
        }

		System.Random rnd = new System.Random();
		List<int> prevIndeces = new List<int>();
		prevIndeces.Add(prevIndex);
		
		for (int i = 0; i < branches; i++)
        {
			int index;
			SewerRoom next = null;
			do
			{
				index = rnd.Next(8);
				next = this.At(origin, SewerRoom.IndexToVector(index));
			}
			while (prevIndeces.Contains(index) || next == null);

			origin.Connect(next);

			Generate(next, SewerRoom.Opposite(index), length - 1, branches);
        }

		// attach sewer room graphic to each node
		// transform coordinates and display graphic
		// show connections between nodes
    }

	public void Clear()
    {
		for (int i = 0; i < size.y; i++)
        {
			for (int j = 0; j < size.x; j++)
            {
				rooms[i, j].Clear();
            }
        }
    }
}
