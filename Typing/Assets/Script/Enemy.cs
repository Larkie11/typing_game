using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Text.RegularExpressions;

public class Enemy : MonoBehaviour {
    Camera cam;
    GameObject player;
    Animator anim;
    AudioSource audio;
    [SerializeField]
    AudioClip dead;
    InputField input;
    bool isPlaying;
    [SerializeField]
    float speed;
    bool died;
    bool collided;
    bool grounded;
    string test2;
    string tempHolder;
    SurfaceEffector2D sf2;
    Text enemyText;
    Text child;
    [SerializeField]
    string clearTextColor;
    string rtOpenTag = "";   //Rich text opening tag
    string rtCloseTag = "</color>"; //Rich text closing tag

    // Use this for initialization
    void Start() {
        died = false;
        rtOpenTag = "<color=" + clearTextColor + ">";
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = gameObject.GetComponent<Animator>();
        audio = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
        input = GameObject.FindGameObjectWithTag("Input").GetComponent<InputField>();
        speed = Random.Range(13, 20);
        child = transform.GetChild(0).GetComponentInChildren<Text>();
        enemyText = transform.GetChild(0).GetChild(0).GetComponent<Text>();

        if (ListOFWords.words.Count > 0)
        {
            int random = Random.Range(0, ListOFWords.words.Count);
            string randomText = ListOFWords.words[random];
            if (child.text != randomText)
            {
                child.text = randomText;
                enemyText.text = randomText;
            }
            ListOFWords.words.RemoveAt(random);
            WordCheck.existingWords.Add(child.text);
        }
        foreach (string word in WordCheck.existingWords)
        {

        }
        child.color = Color.white;
        if (enemyText.text == "")
            enemyText.text = child.text;
        isPlaying = false;
        collided = false;
        tempHolder = enemyText.text;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.GetComponentInChildren<Text>().text != "")
        Global.health -= 10;
        if (!died)
        {
            Global.showBlood = true;
            if (collision.gameObject.tag == "Safety")
            collided = true;
        }
        if (collision.gameObject.tag == "SpawnPlatform")
            grounded = true;
        else
            grounded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "SpawnPlatform")
            grounded = true;
        else
            grounded = false;

        if (collision.gameObject.tag == "conveyer")
            sf2 = collision.gameObject.GetComponent<SurfaceEffector2D>();
    }
    // Update is called once per frame
    void Update () { 
       
        if (input.text != "" && child.text.StartsWith(input.text))
        {
            string modified = tempHolder.Insert(input.text.Length, rtCloseTag);
            test2 = modified.Insert(0, rtOpenTag);
            if (test2 != "" && test2 != enemyText.text)
                enemyText.text = test2;
        }
        if (input.text == "")
            enemyText.text = child.text;

        if (sf2 != null)
        {
            if ( sf2.speed > 0)
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            else
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (collided)
        {
            transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * 10;
            if (transform.localScale.x <= 0)
            {
                Destroy(gameObject);
            }
            WordCheck.existingWords.Remove(child.text);
            child.text = "";
        }
        if (child.text == "" || Global.killAll)
        {
            died = true;
            anim.SetInteger("State",1);
            if(Global.health <= 0)
                Destroy(gameObject);
            if (!isPlaying)
            {
                Global.multiplier = 2;
                audio.PlayOneShot(dead);
                isPlaying = true;
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Empty"))
            {
               Destroy(gameObject);
            }
        }
    }
}
