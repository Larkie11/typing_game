using UnityEngine;
using System.Collections;

public class GetVolume : MonoBehaviour {

    [SerializeField]
    string whatSound;
    // Use this for initialization
    private void Awake()
    {
        gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat(whatSound);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
