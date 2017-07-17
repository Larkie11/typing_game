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
    bool playOnce;
    bool collided;
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
        SFX.PlayOneShot(fire);
        playOnce = false;
        collided = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        Global.showBlood = true;
        collided = true;
        Global.health -= 5;
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
        if (gameObject.GetComponentInChildren<Text>().text == "")
        {
            transform.position = Vector3.MoveTowards(transform.position, boss.transform.position, 500 * Time.deltaTime);
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            gameObject.GetComponent<Rigidbody2D>().mass = 0;

            transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * 1;

            if (!playOnce)
            {
                SFX.PlayOneShot(bossHurt);
                Global.bossHealth -= 10;
                playOnce = true;
            }

            if (transform.localScale.x <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
