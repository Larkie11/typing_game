using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    [SerializeField]
    Canvas canvas;
    Animator anim;
    [SerializeField]
    Text text;

    [SerializeField]
    SentenceGenerator sentences;


    bool changeState;
    enum States
    {
        Idle,
        Attack,
        SpawnWord,
    }
    States state;
    Vector3 currScale;
    bool smaller = false;
    float timer;

    [SerializeField]
    Text bossHealth;

    float aa = 10;

    GameObject fireballSpawn;

    // Use this for initialization
    void Start () {
        state = States.Idle;
        if (Global.difficultyLevel >= 1.0 && Global.difficultyLevel < 2)
        {
            timer = 5f;
            Global.bossHealth = 150;
        }
        else if (Global.difficultyLevel >= 2)
        {
            timer = 3f;
            Global.bossHealth = 300;
        }
        Global.maxBossHealth = Global.bossHealth;
        anim = gameObject.GetComponent<Animator>();
        changeState = false;
        currScale = gameObject.transform.localScale;
        fireballSpawn = GameObject.FindGameObjectWithTag("BallPlatform");
    }
    static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }
    // Update is called once per frame
    void Update()
    {
            Debug.Log(aa -= Time.fixedDeltaTime);
            if (Global.bossHealth <= 0)
            {
                Global.bossHealth = 0;
                text.text = "";
            }
            bossHealth.text = Global.bossHealth.ToString();
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                state = GetRandomEnum<States>();

            if (Global.difficultyLevel < 2)
                timer = 5;

            else if (Global.difficultyLevel >= 2 && Global.difficultyLevel < 3)
                timer = 3;
            else
                timer = 3;
            smaller = false;
            }

            if (state == States.SpawnWord)
            {
                Debug.Log(ListOFWords.bossWords.Count);
                if (ListOFWords.bossWords.Count > 0 && text.text == "")
                {
                string randomText;
                if (Global.difficultyLevel < 2)
                {
                    int random = Random.Range(0, ListOFWords.bossWords.Count);
                    randomText = ListOFWords.bossWords[random];
                }
                else
                {
                    sentences.RandomSentence();
                    randomText = sentences.ReturnSentence();
                }
                    Debug.Log(randomText);
                    text.text = randomText;
                    state = States.Idle;
                }
                if (text.text != "")
                {
                    state = States.Idle;
                }
            }
            if (state == States.Idle)
            {
                anim.speed = 1;

                if (timer >= 5)
                    timer = 1;
            }
            if (state == States.Attack)
            {
                if (!changeState)
                {
                    changeState = true;
                }
            }
            if (changeState && !smaller)
            {
                transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * 3;

                if (transform.localScale.x >= 3)
                {
                    smaller = true;
                    GameObject germSpawned = Instantiate(Resources.Load("Fireball")) as GameObject;
                    germSpawned.transform.SetParent(canvas.transform);
                    Vector3 screenPosition = new Vector3(Random.Range(fireballSpawn.GetComponent<BoxCollider2D>().bounds.min.x, fireballSpawn.GetComponent<BoxCollider2D>().bounds.max.x), Random.Range(Screen.height / 2 - 50, Screen.height / 2 + 10), 0);
                    germSpawned.transform.localPosition = screenPosition;
                    germSpawned.transform.localRotation = Quaternion.identity;
                    germSpawned.transform.localScale = new Vector3(1, 1, 1);
                    anim.speed = 1;
                }
            }
            if (smaller && transform.localScale.x >= 2)
            {
                transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * 3;
                if (transform.localScale.x <= 3)
                {
                    changeState = false;
                }
            }
    }
}
