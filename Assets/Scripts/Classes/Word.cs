

namespace Assets.Scripts.Classes
{
    public class Word
    {
        public string word;
        public string description;

        public Word()
        {
            word = "Word";
            description = "A unit of spoken or written communication.";
        }

        public string Decapitalize()
        {
            string copy = new string(word);
            char start = copy[0];
            if (start > 90)
            {
                start -= (char) 32;
                copy.Remove(0, 1);
                copy.Insert(0, new string(start, 1));
            }

            return copy;
        }
    }
}