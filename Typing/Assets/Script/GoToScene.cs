using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour {
    [SerializeField]
    InputField name;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public static void GoTo(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
    public static string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
    public void GoTo2(string SceneName)
    {
        if(GetSceneName() == "3")
        {
            Highscores.AddNewHighscore(name.text, Global.score);
        }
        if (SceneName == "Menu")
        {
            Global.score = 0;
            Global.health = 100;
        }
        SceneManager.LoadScene(SceneName);
    }
}
