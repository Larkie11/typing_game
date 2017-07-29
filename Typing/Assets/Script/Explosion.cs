using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    Animator anim;
    [SerializeField]
    AudioClip explode;
    AudioSource audio;
    GameObject[] monsters;

    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        audio = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
        audio.PlayOneShot(explode);

        monsters = GameObject.FindGameObjectsWithTag("Monster");


        foreach (GameObject go in monsters)
        {
           // Destroy(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Empty"))
        {
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

            Destroy(gameObject);
        }
    }
}
