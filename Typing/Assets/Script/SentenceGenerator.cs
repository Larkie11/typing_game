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
        TextAsset starter = Resources.Load("starter1") as TextAsset;
        string[] starterContent = starter.text.Split(new[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None);
        starter1.AddRange(starterContent);

        TextAsset adverbs = Resources.Load("adverbs1") as TextAsset;
        string[] adverbsContent = adverbs.text.Split(new[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None);
        adverbs1.AddRange(adverbsContent);

        TextAsset verbs = Resources.Load("verbs1") as TextAsset;
        string[] verbsContent = verbs.text.Split(new[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None);
        verbs1.AddRange(verbsContent);

        Debug.Log(starter.text);
        foreach(string i in starter1)
        {
            Debug.Log("starter1_" + i);
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
