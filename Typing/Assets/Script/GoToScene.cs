using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour {

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
        SceneManager.LoadScene(SceneName);
    }
}
