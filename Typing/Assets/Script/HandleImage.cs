using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HandleImage : MonoBehaviour {
    [SerializeField]
    GameObject handle;

    [SerializeField]
    string whatSound;

    [SerializeField]
    Sprite noVol;
    [SerializeField]
    Sprite gotVol;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetFloat(whatSound) <= 0)
        {
            handle.GetComponent<Image>().sprite = noVol;
        }
        else
        {
            handle.GetComponent<Image>().sprite = gotVol;
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if(PlayerPrefs.GetFloat(whatSound) <= 0)
        {
            handle.GetComponent<Image>().sprite = noVol;
        }
        else
        {
            handle.GetComponent<Image>().sprite = gotVol;
        }
    }
}
