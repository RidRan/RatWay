using System;
using System.Collections.Generic;

public class Word
{
	public string word;
	public List<string> synonyms;
	public string description;

	public Word(string w)
    {
		word = w;
		synonyms = new List<string>();
		description = "";
    }

	public Word(string w)
	{
		word = w;
		synonyms = new List<string>();
		description = "";
	}

	public static string Normalize(string s)
    {
		return s.ToLower().Trim();
    } 

	public bool Match(string s)
    {
		if (Normalize(s) == Normalize(word))
        {
			return true;
        }

		foreach(string syn in synonyms) {
			if (s == syn)
            {
				return true;
            }
        }

		return false;
    }
}
