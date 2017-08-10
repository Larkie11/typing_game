using UnityEngine;
using System.Collections;

public class Defeated : MonoBehaviour {
    [SerializeField]
    Canvas confirmToMenu;
    // Use this for initialization
    void Start () {
        confirmToMenu = confirmToMenu.GetComponent<Canvas>();
        confirmToMenu.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void RestartGame()
    {
        GoToScene.GoTo(GoToScene.GetSceneName());
        Global.health = 100;
        Global.wordLimit = Global.wordLimitSet;
        Global.bossHealth = 300;
        Global.score -= 100;
        Time.timeScale = 1;
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
