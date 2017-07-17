using UnityEngine;
using System.Collections;

public class SafetyLine : MonoBehaviour {

    float y;
	// Use this for initialization
	void Start () {
	       
        if(Global.difficultyLevel >= 1f && Global.difficultyLevel < 2f)
        {
            y = -249;
        }
        if (Global.difficultyLevel >= 2f)
        {
            y = -181;
        }
        Vector3 newVec = new Vector3(gameObject.GetComponent<RectTransform>().position.x, y, 0);
        gameObject.transform.localPosition = newVec;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

}
