using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

    Canvas pauseGame;

    [SerializeField]
    GameObject volumeSettings;
    [SerializeField]
    GameObject mainPause;
    [SerializeField]
    GameObject confirmReturn;
    // Use this for initialization

    void Start () {
        pauseGame = gameObject.GetComponent<Canvas>();
        pauseGame.enabled = false;
        volumeSettings.SetActive(false);
        confirmReturn.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape) && !pauseGame.isActiveAndEnabled)
        {
            pauseGame.enabled = true;
            Time.timeScale = 0;
        }
       
        else if (Input.GetKeyUp(KeyCode.Escape) && pauseGame.isActiveAndEnabled && !volumeSettings.activeSelf)
        {
            pauseGame.enabled = false;
            Time.timeScale = 1;
            pauseGame.GetComponent<CanvasGroup>().alpha = 0;
            Reset();
        }
        if (pauseGame.isActiveAndEnabled && pauseGame.GetComponent<CanvasGroup>().alpha <= 1)
        {
            pauseGame.GetComponent<CanvasGroup>().alpha += 0.05f;
        }
        if (Input.GetKeyUp(KeyCode.Escape) && pauseGame.isActiveAndEnabled && volumeSettings.activeSelf)
        {
            Reset();
        }
    }
    private void Reset()
    {
        volumeSettings.SetActive(false);
        mainPause.SetActive(true);
        confirmReturn.SetActive(false);
    }
    public void ChangeVolume()
    {
        volumeSettings.SetActive(true);
        mainPause.SetActive(false);
    }
    public void ConfirmReturn()
    {
        confirmReturn.SetActive(true);
        mainPause.SetActive(false);
    }
    public void NotReturn()
    {
        Reset();
    }

    public void ReturnBackTime()
    {
        pauseGame.enabled = false;
        Time.timeScale = 1;
    }
}
