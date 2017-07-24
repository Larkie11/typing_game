using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelfRandom : MonoBehaviour {

    [SerializeField]
    Sprite[] backgroundSprites;

    // Use this for initialization
    void Start () {
        gameObject.GetComponent<Image>().sprite = backgroundSprites[Random.Range(0, backgroundSprites.Length)];
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
