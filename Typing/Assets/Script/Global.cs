using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {
    public static float difficultyLevel = 3.0f;
    public static float easy1 = 1.0f;
    public static float easy2 = 1.1f;
    public static float easy3 = 1.2f;
    public static int wordsCleared;
    public static float timer;
    public static int health = 100;
    public static float multiplier;
    public static int enemyKilledWithinMultipler = 0;
    public static int score;
    public static bool showBlood = false;
    public static int bossHealth = 150;
    public static float maxBossHealth;
    public static int wordLimitSet;
    public static int wordLimit = 5;
    public static bool allowSymbols = true;
    public static bool readFile = false;
    public static bool allowUpperCase = false;
    public static int numberOfMonsters;
    public static bool showingSentence = false;
    public static bool killAll = false;
	// Use this for initialization
	private void Awake () {
        wordsCleared = 0;
        timer = 0;
        wordLimitSet = wordLimit;
    }
    private void Start()
    {
        wordLimit = PlayerPrefs.GetInt("WordLimit");
    }
    public void SetDifficulty(int level)
    {
        difficultyLevel = level;
    }
	// Update is called once per frame
	void Update () {

	}
}
