using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Won : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    Text wonText;

    void Start () {

    }

    // Update is called once per frame
    void Update () {
	
	}
    public void BackToMenu()
    {
        GoToScene.GoTo("Menu");
        gameObject.SetActive(false);
        Global.wordLimit = Global.wordLimitSet;
        Global.health = 100;
        Global.bossHealth = 300;
        Time.timeScale = 1;
    }

}
