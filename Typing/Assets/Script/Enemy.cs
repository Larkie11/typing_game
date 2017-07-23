using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    Camera cam;
    GameObject player;
    Animator anim;
    AudioSource audio;
    [SerializeField]
    AudioClip dead;
    bool isPlaying;
    [SerializeField]
    float speed;
    bool died;
    bool collided;
    bool grounded;
    SurfaceEffector2D sf2;
    // Use this for initialization
    void Start() {
        died = false;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = gameObject.GetComponent<Animator>();
        audio = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
        speed = Random.Range(13, 20);
        isPlaying = false;
        collided = false;
        if (ListOFWords.words.Count > 0)
        {
            int random = Random.Range(0, ListOFWords.words.Count);
            string randomText = ListOFWords.words[random];
            if (gameObject.GetComponentInChildren<Text>().text != randomText)
            {
                gameObject.GetComponentInChildren<Text>().text = randomText;
            }
            ListOFWords.words.RemoveAt(random);
            WordCheck.existingWords.Add(gameObject.GetComponentInChildren<Text>().text);
        }
        foreach (string word in WordCheck.existingWords)
        {
            Debug.Log(word);
        }
        gameObject.GetComponentInChildren<Text>().color = Color.white;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
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
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "SpawnPlatform")
            grounded = true;
        else
            grounded = false;

        if (collision.gameObject.tag == "conveyer")
            sf2 = collision.gameObject.GetComponent<SurfaceEffector2D>();
    }
    // Update is called once per frame
    void Update () {

        if (sf2 != null)
        {
            if ( sf2.speed > 0)
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            else
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        // transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        if (collided)
        {
            transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * 10;
            if (transform.localScale.x <= 0)
            {
                Destroy(gameObject);
            }
            WordCheck.existingWords.Remove(gameObject.GetComponentInChildren<Text>().text);
            gameObject.GetComponentInChildren<Text>().text = "";
        }
        if (gameObject.GetComponentInChildren<Text>().text == "")
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
