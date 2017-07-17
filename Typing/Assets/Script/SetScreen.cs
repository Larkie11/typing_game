using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetScreen : MonoBehaviour {
    Resolution[] resolutions;
    [SerializeField]
    Text screenRes;
    int currSelection;
    [SerializeField]
    Toggle FS;
    [SerializeField]
    Slider volumeSlider;
    AudioSource audio;

    // Use this for initialization
    void Start () {
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
        resolutions = Screen.resolutions;
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
        currSelection = 0;
        foreach (Resolution res in resolutions)
        {
            print(res.width + "x" + res.height);
        }
        FS = FS.GetComponent<Toggle>();

        if (Screen.fullScreen)
            FS.isOn = true;
        else
            FS.isOn = false;
    }
    public void PreviousScreenSize()
    {
        if (currSelection > 0)
            currSelection--;
        else if (currSelection <= 0)
            currSelection = resolutions.Length;
    }
    public void NextScreenSize()
    {
        if (currSelection <= resolutions.Length)
            currSelection++;
        else if (currSelection > resolutions.Length)
            currSelection = 0;
    }
    public void ApplyChanges()
    {
        PlayerPrefs.SetInt("Width", resolutions[currSelection].width);
        PlayerPrefs.SetInt("Height", resolutions[currSelection].height);
        Screen.SetResolution(PlayerPrefs.GetInt("Width"), PlayerPrefs.GetInt("Height"), FS.isOn);
    }
    public void Back()
    {
        if (Screen.fullScreen)
            FS.isOn = true;
        else
            FS.isOn = false;
    }
    // Update is called once per frame
    void Update () {
        screenRes.text = resolutions[currSelection].width + " x " + resolutions[currSelection].height;

    }
}
