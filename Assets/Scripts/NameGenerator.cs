using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

class Character
{
    public char Letter;
    public int Instances;
    public float Probability;
}


public class NameGenerator : MonoBehaviour
{
    int order;
    System.Random random;
    Dictionary<string, List<Character>> chains = new Dictionary<string, List<Character>>();
    HashSet<string> wordPatterns = new HashSet<string>();

    private void Start()
    {
        NameGenerator firstnameGen = new NameGenerator("/first-names.txt");
        NameGenerator surnameGen = new NameGenerator("/names.txt");

        string firstname = firstnameGen.GenerateRandomWord();
        string surname = surnameGen.GenerateRandomWord();

        Text text = this.GetComponent<Text>();
        text.text = "Suspect has stolen an Oscar award for Oscarless " + firstname + " " + surname + " according to sources.";
    }

    public NameGenerator(string[] words, int order = 2, System.Random random = null)
    {
        Initialize(words, order, random);
    }


    public NameGenerator(string filename, int order = 2, System.Random random = null)
    {
        string inputfile = Application.dataPath + filename;
        string[] words = File.ReadAllLines(inputfile);
        Initialize(words, order, random);
    }


    void Initialize(string[] words, int order, System.Random random)
    {
        this.random = random == null ? new System.Random() : random;
        AnalyzeWords(words, order);
    }


    bool IsVowel(char ch)
    {
        return "aeiou".Contains(ch) || "àáâãäèéêëìíîïòóôõöùúûü".Contains(ch);
    }


    string GetWordPattern(string word)
    {
        string pattern = "";

        foreach (char ch in word)
        {
            pattern += IsVowel(ch) ? "v" : "c";
        }

        return pattern;
    }


    void IdentifyWordPattern(string word)
    {
        wordPatterns.Add(GetWordPattern(word));
    }


    void AddCharacter(string key, char ch)
    {
        List<Character> chain;
        if (!chains.TryGetValue(key, out chain))
        {
            chain = new List<Character>();
            chains.Add(key, chain);
        }


        Character letter = chain.Find(l => l.Letter == ch);
        if (letter == null)
        {
            letter = new Character { Letter = ch };
            chain.Add(letter);
        }

        letter.Instances++;
    }


    void ProcessWord(string word)
    {
        word = new String('_', order) + word + "_";

        for (int i = 0; i < word.Length - order; i++)
        {
            string key = word.Substring(i, order);
            AddCharacter(key, word[i + order]);
        }
    }


    void AnalyzeWord(string word)
    {
        IdentifyWordPattern(word);
        ProcessWord(word);
    }


    void CalculateProbability()
    {
        Dictionary<string, List<Character>> newChains = new Dictionary<string, List<Character>>();

        foreach (KeyValuePair<string, List<Character>> pair in chains)
        {
            float totalInstances = pair.Value.Sum(l => l.Instances);
            pair.Value.ForEach(l => l.Probability = l.Instances / totalInstances);
            newChains[pair.Key] = pair.Value.OrderBy(l => l.Probability).ToList();
        }

        chains.Clear();
        chains = newChains;
    }


    void AnalyzeWords(string[] words, int order)
    {
        this.order = order;

        chains.Clear();
        wordPatterns.Clear();

        foreach (string word in words)
        {
            AnalyzeWord(word.ToLower());
        }

        CalculateProbability();
    }


    Character GetCharacterByProbability(string key, double probability)
    {
        List<Character> chain;
        if (!chains.TryGetValue(key, out chain)) return null;

        float cumulative = 0;
        Character result = null;

        for (int i = 0; i < chain.Count; i++)
        {
            cumulative += chain[i].Probability;
            if (probability < cumulative)
            {
                result = chain[i];
                break;
            }
        }

        return result;
    }


    string GenerateRandomWord(int minLength, int maxLength)
    {
        string result;

        result = "";
        string key = new String('_', order);

        while (result.Length < maxLength)
        {
            Character character = GetCharacterByProbability(key, random.NextDouble());
            char ch = character == null ? '_' : character.Letter;
            if (ch == '_') break;

            result += ch;
            key += ch;

            key = key.Substring(key.Length - order);
        }

        return result.Substring(0, 1).ToUpper() + result.Substring(1);
    }


    public string GenerateRandomWord(int minLength = 5, int maxLength = 16, bool matchWordPattern = true)
    {
        for (int i = 0; i < 64; i++)
        {
            string word = GenerateRandomWord(minLength, maxLength);
            if (word.Length < minLength) continue;
            if (!matchWordPattern) return word;
            if (wordPatterns.Contains(GetWordPattern(word))) return word;
        }

        return "";
    }
}
