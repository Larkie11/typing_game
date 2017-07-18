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
	void Start () {
        if (ListOFWords.words.Count > 0)
        {
            int random = Random.Range(0, ListOFWords.words.Count);
            string randomText = ListOFWords.words[random];
            ListOFWords.words.Remove(randomText);
            gameObject.GetComponentInChildren<Text>().text = randomText;
        }
        boss = GameObject.FindGameObjectWithTag("Boss");
        SFX = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");

        SFX.PlayOneShot(fire);
        playOnce = false;
        collided = false;
        textCount = gameObject.GetComponentInChildren<Text>().text.Length;


        if (Global.difficultyLevel < 2)
            moveSpeed = 10;

        else if (Global.difficultyLevel >= 2 && Global.difficultyLevel < 3)
            moveSpeed = 24;
        else
            moveSpeed = 30;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
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
        if (gameObject.GetComponentInChildren<Text>().text == "" && !collided)
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
