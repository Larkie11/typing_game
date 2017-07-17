using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetGame : MonoBehaviour {

    [SerializeField]
    Text sliderText;
    Slider thisSlider;
    Toggle thisToggle;
    [SerializeField]
    string TypeOfToggle;
	// Use this for initialization
	void Start () {
        if (gameObject.GetComponent<Slider>() != null)
        {
            thisSlider = gameObject.GetComponent<Slider>();
            if (PlayerPrefs.HasKey("WordLimit"))
                thisSlider.value = PlayerPrefs.GetInt("WordLimit");
        }
        if (gameObject.GetComponent<Toggle>() != null && TypeOfToggle == "Symbols")
        {
            thisToggle = gameObject.GetComponent<Toggle>();
            if (PlayerPrefs.HasKey("AllowSymbols"))
                if(PlayerPrefs.GetInt("AllowSymbols") == 0)
                    thisToggle.isOn = true;
                 else
                     thisToggle.isOn = false;
        }
        if (gameObject.GetComponent<Toggle>() != null && TypeOfToggle == "Upper")
        {
            thisToggle = gameObject.GetComponent<Toggle>();
            if (PlayerPrefs.HasKey("AllowUpper"))
                if (PlayerPrefs.GetInt("AllowUpper") == 0)
                    thisToggle.isOn = true;
                else
                    thisToggle.isOn = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(thisSlider != null)
        sliderText.text = thisSlider.value.ToString();
	}

    public void SetWordLimit()
    {
        Global.wordLimit = (int)thisSlider.value;
        PlayerPrefs.SetInt("WordLimit", (int)thisSlider.value);
    }

    public void SetOnlyAlphabetic()
    {
        if (thisToggle != null && TypeOfToggle == "Symbols")
        {
            Global.allowSymbols = thisToggle.isOn;
            if(thisToggle.isOn)
                PlayerPrefs.SetInt("AllowSymbols", 0);
            else
                PlayerPrefs.SetInt("AllowSymbols", 1);
        }
    }
    public void AllowUpperCase()
    {
        if (thisToggle != null && TypeOfToggle == "Upper")
        {
            Global.allowUpperCase = thisToggle.isOn;
            if (thisToggle.isOn)
                PlayerPrefs.SetInt("AllowUpper", 0);
            else
                PlayerPrefs.SetInt("AllowUpper", 1);
        }
    }
}
