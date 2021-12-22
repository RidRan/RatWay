using System;
using System.Collections.Generic;

public class Word
{
	public string word;
	public List<string> synonyms;
	public string description;

	public Word(List<string> s, string d)
    {
		word = "Word";
		synonyms = s;
		description = d;
    }

	public bool Match(string s)
    {
		if (s == word)
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
