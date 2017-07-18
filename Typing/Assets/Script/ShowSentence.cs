using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowSentence : MonoBehaviour {

    [SerializeField]
    Text sentenceText;

    [SerializeField]
    Image timerBar;

    float timer;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (timer > 0 && sentenceText.text != "")
            timer -= Time.deltaTime;

        if (sentenceText.text == "")
        {
            timerBar.fillAmount = 0;
            timerBar.enabled = false;
            timer = 10;
        }
        else
            timerBar.enabled = true;

        if (timer <= 0)
        {
            sentenceText.text = "";
            timer = 10;
        }

        if(sentenceText.text != "")
        {
            timerBar.fillAmount = Mathf.Lerp(timerBar.fillAmount, timer / 10f, Time.deltaTime * 100);
        }
        //if (Global.bossHealth < 300 && sentenceText.text == "")
        //    sentenceText.text = sentences.ReturnSentence();
        
	}
}
