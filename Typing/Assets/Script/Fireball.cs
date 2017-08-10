using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fireball : MonoBehaviour {

    // Use this for initialization
    GameObject boss;
    AudioSource SFX;
    [SerializeField]
    AudioClip fire;
    [SerializeField]
    AudioClip bossHurt;
    GameObject player;
    
    bool playOnce;
    bool collided;
    int textCount;
    float moveSpeed;
    Text clearText;
    Text child;
    InputField input;
    string tempHolder;
    string rtOpenTag = "<color=red>";   //Rich text opening tag
    string rtCloseTag = "</color>"; //Rich text closing tag

    void Start () {
        child = transform.GetChild(0).GetComponentInChildren<Text>();
        clearText = transform.GetChild(0).GetChild(0).GetComponent<Text>();

        if (ListOFWords.words.Count > 0)
        {
            int random = Random.Range(0, ListOFWords.words.Count);
            string randomText = ListOFWords.words[random];
            ListOFWords.words.Remove(randomText);
            child.text = randomText;
            clearText.text = tempHolder = randomText;
        }
        boss = GameObject.FindGameObjectWithTag("Boss");
        SFX = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        input = GameObject.FindGameObjectWithTag("Input").GetComponent<InputField>();

        SFX.PlayOneShot(fire);
        playOnce = false;
        collided = false;
        textCount = gameObject.GetComponentInChildren<Text>().text.Length;

        if (Global.difficultyLevel < 2)
            moveSpeed = 10;

        else if (Global.difficultyLevel >= 2 && Global.difficultyLevel < 3)
            moveSpeed = 15;
        else
            moveSpeed = 17;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Global.showBlood = true;

        if (!collided)
        {
            Global.health -= textCount;

            collided = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (input.text != "" && child.text.StartsWith(input.text))
        { 
            string modified = tempHolder.Insert(input.text.Length, rtCloseTag);
            string test2 = modified.Insert(0, rtOpenTag);
            if (test2 != "" && test2 != clearText.text)
                clearText.text = test2;
        }
        if (input.text == "")
            clearText.text = child.text;

        if (collided)
        {
            transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * 2;
            if (transform.localScale.x <= 0)
            {
                Destroy(gameObject);
            }
        }

        if(Global.bossHealth <= 0 || Global.health <= 0)
        {
            Destroy(gameObject);
        }
        if (child.text == "" && !collided)
        {
            transform.position = Vector3.MoveTowards(transform.position, boss.transform.position, 500 * Time.deltaTime);
            transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * 1;

            if (!playOnce)
            {
                SFX.PlayOneShot(bossHurt);
                Global.bossHealth -= textCount;
                playOnce = true;
            }

            if (transform.localScale.x <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, player.transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }
    }
}
