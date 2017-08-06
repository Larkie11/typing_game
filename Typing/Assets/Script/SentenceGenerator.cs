using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class SentenceGenerator : MonoBehaviour {


    public static List<string> starter1 = new List<string>();
    public static List<string> adverbs1 = new List<string>();
    public static List<string> verbs1 = new List<string>();

    string randomSentence;

    // Use this for initialization
    void Start () {

        using (StreamReader sr = new StreamReader(Application.streamingAssetsPath + "/starter1.txt"))
        {
            string line; // this is the buffer for the content from your file
            while ((line = sr.ReadLine()) != null)
            {
                starter1.Add(line);
            }
        }

        using (StreamReader sr = new StreamReader(Application.streamingAssetsPath + "/adverbs1.txt"))
        {
            string line; // this is the buffer for the content from your file
            while ((line = sr.ReadLine()) != null)
            {
                adverbs1.Add(line);
            }
        }

        using (StreamReader sr = new StreamReader(Application.streamingAssetsPath + "/verbs1.txt"))
        {
            string line; // this is the buffer for the content from your file
            while ((line = sr.ReadLine()) != null)
            {
                verbs1.Add(line);
            }
        }
        RandomSentence();
    }
	

    public string ReturnSentence()
    {
        if (randomSentence != "")
            return randomSentence;
        else
            return "";
    }

    public void RandomSentence()
    {
            randomSentence = starter1[Random.Range(0, starter1.Count)] + " " + adverbs1[Random.Range(0, adverbs1.Count)] + " " + verbs1[Random.Range(0, verbs1.Count)];
    }
    // Update is called once per frame
    void Update () {
	
	}
}
