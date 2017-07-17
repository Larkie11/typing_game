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
        volumeSlider.value = PlayerPrefs.GetFloat(whatSound);
    }
    // Update is called once per frame
    void Update () {
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
