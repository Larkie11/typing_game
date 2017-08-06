using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class TitleRotation : MonoBehaviour {

    Vector3 currentAngle;
    float timer;
    bool rotateRight;
    float scale;
    [SerializeField]
    float rotationZ;
    [SerializeField]
    float maxRotation;
    [SerializeField]
    float minRotation;
    string myString;
    Text myText;
    [SerializeField]
    AudioClip typingSound;
    AudioSource audio;
    bool red = false;
    bool ignore = false;
    float speedTyping;
    // Use this for initialization
    void Start() {
        audio = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
        myText = gameObject.GetComponent<Text>();
        myString = myText.text;
        if (typingSound != null)
        {
            myText.text = "";
            StartCoroutine(AutoType());
        }
        rotateRight = false;
        currentAngle = transform.eulerAngles;
        scale = transform.localScale.x;
        timer = 0f;
        speedTyping = 0.2f;
    }
    IEnumerator AutoType()
    {
        foreach (char letter in myString.ToCharArray())
        {
            switch (letter)
            {
                case '@':
                    ignore = true;
                    red = !red;
                    break;
            }

            string temp = letter.ToString();

            if (!ignore)
            {
                if (red)
                    temp = "<color=red>" + letter + "</color>";

                myText.text += temp;
                if(speedTyping > 0)
                audio.PlayOneShot(typingSound);
            }
            ignore = false;
       
            yield return new WaitForSeconds(speedTyping);
        }
        if (speedTyping <= 0)
            audio.PlayOneShot(typingSound);
    }
    // Update is called once per frame
    void Update () {
        if(Input.GetMouseButtonDown(0))
        {
            speedTyping = 0;
        }

        if (rotationZ >= maxRotation && !rotateRight)
        {
            rotationZ -= Time.deltaTime * 10;
            scale += Time.deltaTime * 0.05f;
        }
        if (rotationZ <= maxRotation + 1)
        {
            rotateRight = true;
        }
        if(rotateRight && rotationZ <= minRotation)
        {
            rotationZ += Time.deltaTime * 10;
            scale -= Time.deltaTime * 0.05f;
        }
        if (rotationZ >= minRotation)
            rotateRight = false;
        timer -= Time.deltaTime;
        transform.localScale = new Vector3(scale,scale, transform.localScale.z);
        currentAngle = new Vector3(currentAngle.x, currentAngle.y, rotationZ);
        transform.eulerAngles = currentAngle;
	}
}
