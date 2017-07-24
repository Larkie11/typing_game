using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WordCheck : MonoBehaviour {

    string myText = "";
    public static List<string> existingWords = new List<string>();

    public Text textAutocomplete;
    [SerializeField]
    InputField input;
    GameObject[] objects;
    GameObject[] bossText;

    [SerializeField]
    string[] monsterSpawn;


    [SerializeField]
    SentenceGenerator sentences;

    [SerializeField]
    GameObject defeatCanvas;
    Canvas defeatC;
    [SerializeField]
    GameObject wonCanvas;
    Canvas wonC;
     [SerializeField]
    Text wonText;
    bool trigger;
    float timer;
    float posY;
    float timertospawn;
    public Canvas canvas;
    float waveStart;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text multiplierText;

    GameObject[] spawnPoints;

    bool died;

    [SerializeField]
    Text cooldownText;

    int previousTimer;

    [SerializeField]
    Camera cam;

    [SerializeField]
    AudioClip countdown1;
    [SerializeField]
    AudioClip countdown2;

    [SerializeField]
    AudioClip victory;
    [SerializeField]
    AudioClip defeat;

    AudioSource audio;
    AudioSource bgm;
    bool playCountdown2;

    [SerializeField]
    string ToSpawn;

    [SerializeField]
    Text wordsLeft;

    [SerializeField]
    Text playerHealth;

    [SerializeField]
    Text cleared;
    Canvas blood;
    bool showBlood;

    GameObject player;
    float multiplerTimer = 0;
    int wordsCleared;
   
    void Start()
    {
        blood = GameObject.FindGameObjectWithTag("Blood").GetComponent<Canvas>();
        defeatCanvas.SetActive(false);
        defeatC = defeatCanvas.GetComponent<Canvas>();
        if(GoToScene.GetSceneName() == "3")
        wonCanvas.SetActive(false);
        if (wonCanvas != null)
        {
            wonC = wonCanvas.GetComponent<Canvas>();
            wonC.GetComponent<CanvasGroup>().alpha = 0;
        }

        defeatC.GetComponent<CanvasGroup>().alpha = 0;
        blood.enabled = false;
        timertospawn = 1f;
        trigger = false;
        timer = 0.3f;
        waveStart = 3.5f;
        cooldownText = cooldownText.GetComponent<Text>();
        previousTimer = -1;
        audio = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
        bgm = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        playCountdown2 = false;
        wordsCleared = Global.wordLimit;
        Global.numberOfMonsters = Global.wordLimit;
        cleared.enabled = false;
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPlatform");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Delete) || Input.GetKeyUp(KeyCode.Tab))
        {
            input.text = "";
        }

        if (Time.timeScale != 0)
            input.ActivateInputField();
        else
            input.DeactivateInputField();

        if (Global.health <= 0)
      
            Global.health = 0;
        
        if (Global.health <= 0 && !defeatCanvas.active)
        {
            defeatCanvas.SetActive(true);
            if (defeat != null)
                audio.PlayOneShot(defeat);
        }

        if (Global.bossHealth <= 0 && !wonCanvas.active)
        {
            wonCanvas.SetActive(true);
            if(victory != null)
            audio.PlayOneShot(victory);
            Time.timeScale = 0;
        }
        playerHealth.text = "Health: " + Global.health.ToString();
       
        if (Global.numberOfMonsters >= 0)
            wordsLeft.text = Global.numberOfMonsters.ToString() + " Words Left";

        if (GoToScene.GetSceneName() == "3" && Global.bossHealth <= 0)
        {
            cleared.enabled = true;
            cleared.text = "Defeated Boss!";
            if (cleared.GetComponent<Text>().fontSize < 40)
            {
                cleared.GetComponent<Text>().fontSize += 1;
            }
            else
            {
                if (wonC.GetComponent<CanvasGroup>().alpha <= 1)
                    wonC.GetComponent<CanvasGroup>().alpha += 0.02f;
            }
        }

        if (Global.health <= 0)
        {
            cleared.enabled = true;
            cleared.text = "Defeated!";
            
            if (cleared.GetComponent<Text>().fontSize < 40)
                cleared.GetComponent<Text>().fontSize += 1;
            else
            {
                if (defeatC.GetComponent<CanvasGroup>().alpha <= 1)
                    defeatC.GetComponent<CanvasGroup>().alpha += 0.02f;
                Time.timeScale = 0;
            }

        }
        if (wordsCleared <= 0 && GoToScene.GetSceneName() != "3")
        {
            ChangeWave();
        }

        ShowBlood();
        SpawnMonsters();
       
        Global.timer += Time.deltaTime;

        if (GoToScene.GetSceneName() == "1")
            StartCountdown();

        else if (waveStart != -1)
            waveStart = -1;

        objects = GameObject.FindGameObjectsWithTag("Monster");

        if (GoToScene.GetSceneName() == "3")
            bossText = GameObject.FindGameObjectsWithTag("Boss");

        myText = input.text;
        AutoCompleteHint();

        scoreText.text = "Score: " + Global.score.ToString();
        if(wonText != null)
        wonText.text = "You defeated the boss!" + '\n' + "You score was " + Global.score.ToString();

        posY += Time.deltaTime * 10;
        ComboMultiplier();
        CheckWord();

        if (GoToScene.GetSceneName() == "3")
            CheckBossWord();
    }
    void ChangeWave()
    {
        cleared.enabled = true;
        if (cleared.GetComponent<Text>().fontSize < 40)
            cleared.GetComponent<Text>().fontSize += 1;

        if (cleared.GetComponent<Text>().fontSize >= 40)
        {
            Global.wordLimit = Global.wordLimitSet;
            if (GoToScene.GetSceneName() == "1")
            {
                Global.difficultyLevel += 0.1f;
                GoToScene.GoTo("2");
            }
            if (GoToScene.GetSceneName() == "2")
            {
                Global.difficultyLevel += 0.1f;
                GoToScene.GoTo("3");
            }
        }
    }

    void ShowBlood()
    {
        if (Global.showBlood && !blood.isActiveAndEnabled)
        {
            blood.enabled = true;
        }
        if (Global.showBlood && blood.isActiveAndEnabled)
            blood.GetComponent<CanvasGroup>().alpha += Time.deltaTime * 3;

        if (blood.GetComponent<CanvasGroup>().alpha >= 1)
        {
            Global.showBlood = false;
        }
        if (blood.isActiveAndEnabled && !Global.showBlood)
        {
            blood.GetComponent<CanvasGroup>().alpha -= Time.deltaTime * 3;
        }
        if (blood.isActiveAndEnabled && blood.GetComponent<CanvasGroup>().alpha <= 0)
            blood.enabled = false;
    }

    void SpawnMonsters()
    {
        if (timertospawn > 0)
            timertospawn -= Time.deltaTime;

        else if (timertospawn <= 0 && Global.numberOfMonsters > 0 && waveStart <= -1)
        {
            if (Global.difficultyLevel < 2)
                timertospawn = Random.Range(2.5f,3f);

            else if (Global.difficultyLevel >= 2 && Global.difficultyLevel < 3)
                timertospawn = Random.Range(1.5f,2f);

            else
                timertospawn = Random.Range(1f, 1.3f);
            if (GoToScene.GetSceneName() != "3")
            {
                int spawner = Random.Range(0, spawnPoints.Length);
                Vector3 spawnLocation;
                Debug.Log(spawnPoints[spawner].transform.position);

                //if (spawnPoints[spawner].transform.position.x > 0)

                //    xSpawn = spawnPoints[spawner].GetComponent<BoxCollider2D>().bounds.max.x * 2f;

                //else
                //    xSpawn = spawnPoints[spawner].GetComponent<BoxCollider2D>().bounds.min.x * 2f;

                //if (spawnPoints[spawner].gameObject.GetComponentInParent<RectTransform>().transform.position.y < 0)
                //{
                //    spawnLocation = new Vector3(xSpawn, -spawnPoints[spawner].transform.position.y + spawnPoints[spawner].GetComponent<BoxCollider2D>().size.y * 2, 0);

                //}
                //else
                //    spawnLocation = new Vector3(xSpawn, spawnPoints[spawner].transform.position.y + 200f, 0);


                spawnLocation = spawnPoints[spawner].transform.localPosition;

                GameObject myRoadInstance;
                if (monsterSpawn.Length <= 0)
                {
                    myRoadInstance =
                                Instantiate(Resources.Load(ToSpawn)) as GameObject;
                }
                else
                {
                    myRoadInstance =
                                   Instantiate(Resources.Load(monsterSpawn[Random.Range(0,monsterSpawn.Length)])) as GameObject;
                }


                    myRoadInstance.transform.localPosition = spawnLocation;

                if (myRoadInstance.transform.position.x > player.transform.position.x)
                    myRoadInstance.GetComponent<SpriteRenderer>().flipX = true;

                myRoadInstance.transform.SetParent(canvas.transform, false);

                Global.wordLimit--;
            }
        }
    }

    void StartCountdown()
    {
        waveStart -= Time.deltaTime;

        if (waveStart > -1)
        {
            cooldownText.text = Mathf.Round(waveStart).ToString();
            if (cooldownText.text != previousTimer.ToString() && waveStart > 0)
            {
                previousTimer = int.Parse(cooldownText.text);

                cooldownText.fontSize = 100;
                audio.PlayOneShot(countdown1);
            }

            if (cooldownText.fontSize > 0)
                cooldownText.fontSize -= 1;
        }
        else
        {
            cooldownText.enabled = false;
        }
        if (waveStart <= 0)
        {
            cooldownText.text = "START!";
            cooldownText.fontSize = 40;
            cooldownText.color = Color.red;
        }
        if (cooldownText.text == "START!" && !playCountdown2)
        {
            audio.PlayOneShot(countdown2);
            playCountdown2 = true;
        }
    }

    void AutoCompleteHint()
    {
        List<string> found = existingWords.FindAll(w => w.StartsWith(myText));
        if (!string.IsNullOrEmpty(myText))
        {
            if (found.Count > 0)
            {
                for (int i = 0; i < found.Count; i++)
                {
                    textAutocomplete.text = "<color=maroon>Auto Complete:</color> " + found[i];
                    print(found[i]);
                }
            }
        }
    }

    void ComboMultiplier()
    {
        if (multiplierText != null)
            if (Global.enemyKilledWithinMultipler > 0)
                multiplierText.text = "COMBO: x" + Global.enemyKilledWithinMultipler.ToString() + '\n' + " + " + (10 * Global.enemyKilledWithinMultipler).ToString() + " score!" ;
            else
                multiplierText.text = "";
        if (multiplerTimer > 0)
        {
            multiplerTimer -= Time.deltaTime;
        }
        else
        {
            Global.multiplier = 0;
            Global.enemyKilledWithinMultipler = 0;
        }
    }
    void CheckBossWord()
    {
        foreach (GameObject o in bossText)
        {
            if (o.GetComponentInChildren<Text>().text == input.text && o.GetComponentInChildren<Text>().text != "")
            {
                if (!trigger)
                {
                    trigger = true;
                    Global.numberOfMonsters--;
                }
                if (trigger)
                {
                    timer -= Time.fixedDeltaTime;
                    o.GetComponentInChildren<Text>().color = Color.blue;
                    Global.wordsCleared--;

                    if (timer <= 0)
                    {
                        o.GetComponentInChildren<Text>().text = "";
                        Global.bossHealth -= 20;
                        trigger = false;
                        input.text = "";
                        timer = 0.1f;
                        Global.health += 10;
                    }
                }
            }
            else
            {
                o.GetComponentInChildren<Text>().color = Color.white;
            }
        }
    }

    void CheckWord()
    {
        foreach (GameObject o in objects)
        {
            if (o.GetComponentInChildren<Text>().text == input.text && o.GetComponentInChildren<Text>().text != "")
            {
                if (!trigger)
                {
                    wordsCleared--;
                    Global.numberOfMonsters--;
                    trigger = true;
                    if(GoToScene.GetSceneName() != "3")
                    existingWords.Remove(o.GetComponentInChildren<Text>().text);
                   
                    multiplerTimer = 2;

                    if (multiplerTimer > 0)
                    {
                        Global.enemyKilledWithinMultipler++;
                        Global.score += 10 * Global.enemyKilledWithinMultipler;
                    }
                    else
                        Global.score += 10;
                }
                if (trigger)
                {
                    timer -= Time.deltaTime;
                    o.GetComponentInChildren<Text>().color = Color.red;

                    if (timer <= 0.1f)
                    {
                        o.GetComponentInChildren<Text>().text = "";
                        trigger = false;
                        input.text = "";
                        timer = 0.2f;
                    }
                }
            }
            else
            {
                o.GetComponentInChildren<Text>().color = Color.white;
            }
        }
    }
}

