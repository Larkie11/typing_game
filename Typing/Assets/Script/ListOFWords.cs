using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

public class ListOFWords : MonoBehaviour {
    public static List<string> words = new List<string>();
    public static List<string> bossWords = new List<string>();
    public static List<string> loadedWords = new List<string>();
    int lowestWordCount = 0;

    [SerializeField]
    TextAsset filepath;
    void SetWords()
    {

        Regex r = new Regex("^[a-zA-Z0-9]*$");
        lowestWordCount = loadedWords[0].Length;

        foreach (string word in loadedWords)
        {
            int n = word.Length;
            StringBuilder sb = new StringBuilder(word);

            //Easy
            if (Global.difficultyLevel < 2)
            {
                if (GoToScene.GetSceneName() == "1")
                {
                    if (n >= 1 && n <= 2)
                    {
                        if (!Global.allowUpperCase)
                        {
                            if (word == word.ToLower())
                            {
                                words.Add(word);
                            }
                        }
                        else
                        {
                            words.Add(word);
                            for (int i = 0; i < n; i++)
                            {
                                int random = Random.Range(0, 100);
                                if (random < 20)
                                    sb[i] = char.ToUpper(word[i]);
                            }
                            words.Add(sb.ToString());
                        }
                    }

                }

                if (GoToScene.GetSceneName() == "2")
                {
                    if (n >= 2 && n <= 5)
                    {
                        if (!Global.allowUpperCase)
                        {
                            if (word == word.ToLower())
                            {
                                words.Add(word);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < n; i++)
                            {
                                int random = Random.Range(0, 100);
                                if (random < 20)
                                    sb[i] = char.ToUpper(word[i]);
                            }
                            words.Add(sb.ToString());
                        }
                    }
                }

                if (GoToScene.GetSceneName() == "3")
                {
                    if (n >= 6 && n <= 8)
                        bossWords.Add(word);
                    if (n >= 5 && n <= 7)
                    {
                        if (!Global.allowUpperCase)
                        {
                            words.Add(word);
                        }
                        else if (Global.allowUpperCase)
                        {
                            words.Add(word);
                            {
                                for (int i = 0; i < n; i++)
                                {
                                    int random = Random.Range(0, 100);
                                    if (random < 20)
                                        sb[i] = char.ToUpper(word[i]);
                                }
                                words.Add(sb.ToString());
                            }
                        }
                    }
                }
            }
            //Normal
            if (Global.difficultyLevel >= 2 && Global.difficultyLevel < 3)
            {
                if (GoToScene.GetSceneName() == "1")
                {
                    if (n >= 4 && n <= 6)
                    {
                        if (!Global.allowUpperCase)
                        {

                            words.Add(word);
                        }
                        else
                        {
                            words.Add(word);
                            {
                                for (int i = 0; i < n; i++)
                                {
                                    int random = Random.Range(0, 100);
                                    if (random < 40)
                                        sb[i] = char.ToUpper(word[i]);
                                }
                                words.Add(sb.ToString());
                            }
                        }
                    }
                }
                if (GoToScene.GetSceneName() == "2")
                {
                    if (n >= 6 && n <= 8)
                    {
                        if (!Global.allowUpperCase)
                        {
                            words.Add(word);
                        }
                        else
                        {
                            words.Add(word);
                            {
                                for (int i = 0; i < n; i++)
                                {
                                    int random = Random.Range(0, 100);
                                    if (random < 40)
                                        sb[i] = char.ToUpper(word[i]);
                                }
                                words.Add(sb.ToString());
                            }
                        }
                    }
                }
                if (GoToScene.GetSceneName() == "3")
                {
                    if (n >= 6 && n <= 8)
                    {
                        if (!Global.allowUpperCase)
                        {
                            words.Add(word);
                        }
                        else
                        {
                            words.Add(word);
                            {
                                for (int i = 0; i < n; i++)
                                {
                                    int random = Random.Range(0, 100);
                                    if (random < 40)
                                        sb[i] = char.ToUpper(word[i]);
                                }
                                words.Add(sb.ToString());
                            }
                        }
                        if (n >= 4)
                            bossWords.Add(word);
                    }
                }
            }

            //Hard
            if (Global.difficultyLevel >= 3)
            {
                if (GoToScene.GetSceneName() == "1")
                {
                    if (n >= 5 && n <= 7)
                    {
                        if (!Global.allowUpperCase)
                        {
                            if (word == word.ToLower())
                            {
                                words.Add(word);
                            }
                        }
                        else
                        {
                            words.Add(word);
                            {
                                for (int i = 0; i < n; i++)
                                {
                                    int random = Random.Range(0, 100);
                                    if (random < 70)
                                        sb[i] = char.ToUpper(word[i]);
                                }
                                words.Add(sb.ToString());
                            }
                        }
                    }
                }

                if (GoToScene.GetSceneName() == "2")
                {
                    if (n >= 6 && n <= 9)
                    {
                        if (!Global.allowUpperCase)
                        {
                            if (word == word.ToLower())
                            {
                                words.Add(word);
                            }
                        }
                        else
                        {
                            words.Add(word);
                            {
                                for (int i = 0; i < n; i++)
                                {
                                    int random = Random.Range(0, 100);
                                    if (random < 70)
                                        sb[i] = char.ToUpper(word[i]);
                                }
                                words.Add(sb.ToString());
                            }
                        }
                    }
                }
                if (GoToScene.GetSceneName() == "3")
                {
                    if (n >= 6)
                    {
                        if (!Global.allowUpperCase)
                        {
                            if (word == word.ToLower())
                            {
                                words.Add(word);
                            }
                        }
                        else
                        {
                            words.Add(word);
                            {
                                for (int i = 0; i < n; i++)
                                {
                                    int random = Random.Range(0, 100);
                                    if (random < 70)
                                        sb[i] = char.ToUpper(word[i]);
                                }
                                words.Add(sb.ToString());
                            }
                        }

                        if (n >= 6)
                            bossWords.Add(word);
                    }
                }
            }
        }
    }
    // Use this for initialization
    IEnumerator loadStreamingAsset(string fileName)
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);

        string result;
        if (filePath.Contains("://") || filePath.Contains(":///"))
        {
            WWW www = new WWW(filePath);
            yield return www;
            result = www.text;
            Debug.Log(result);
            if (Global.readFile == false)
            {
                using (StreamReader sr = new StreamReader(Application.streamingAssetsPath + www.text))
                {
                    string line; // this is the buffer for the content from your file
                    while ((line = sr.ReadLine()) != null)
                    {
                        loadedWords.Add(line);
                    }
                }
                Global.readFile = true;
            }
        }
        else
        {
            result = System.IO.File.ReadAllText(filePath);
            string[] fileContent = System.IO.File.ReadAllLines(filePath);
            loadedWords.AddRange(fileContent);
        }
    }
    void Start()
    {
        words.Clear();
        bossWords.Clear();



        StartCoroutine(loadStreamingAsset("English.txt"));
        if (PlayerPrefs.GetInt("AllowUpper") == 0)
            Global.allowUpperCase = true;
        else
            Global.allowUpperCase = false;


        if (PlayerPrefs.GetInt("AllowSymbols") == 0)
            Global.allowSymbols = true;
        else
            Global.allowSymbols = false;

        SetWords();
    }


    // Update is called once per frame
    void Update () {
        if (words.Count <= 0)
        {
            SetWords();
        }
    }
}
