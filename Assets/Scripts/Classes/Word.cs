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

	public Word(string w, List<string> s)
	{
		word = w;
		synonyms = s;
		description = "";
	}

	public Word(string w, List<string> s, string d)
	{
		word = w;
		synonyms = s;
		description = d;
	}

	public void AddSynonym(string syn)
    {
		synonyms.Add(syn);
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
