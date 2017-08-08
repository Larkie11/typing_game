using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour {

    [SerializeField]
    Slider volumeSlider;
    [SerializeField]
    string whatSound;

    // Use this for initialization
    private void Awake()
    {
    }
    private void Start()
    {
        volumeSlider = volumeSlider.GetComponent<Slider>();
        if (PlayerPrefs.HasKey(whatSound))
            volumeSlider.value = PlayerPrefs.GetFloat(whatSound);
        else
        {
            if (whatSound == "volume")
            {
                volumeSlider.value = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>().volume;
            }
            if (whatSound == "sfx")
            {
                volumeSlider.value = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>().volume;
            }

        }
    }
    // Update is called once per frame
    void Update () {
        if(PlayerPrefs.HasKey(whatSound))
        gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat(whatSound);

    }
    public void OnValueChangeVol()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volume");
    }
    public void OnValueChangeSFX()
    {
        PlayerPrefs.SetFloat("sfx", volumeSlider.value);
        gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("sfx");
    }
}
