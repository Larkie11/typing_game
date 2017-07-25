using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    [SerializeField]
    Image healthBar;
    [SerializeField]
    string bossOrPlayer;
    float speed;
	// Use this for initialization
	void Start () {
        speed = 5;
	}
	
	// Update is called once per frame
	void Update () {
        if (bossOrPlayer == "player")
        {
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, Global.health / 100f, Time.deltaTime * speed);

            if (Global.health <= 0)
                healthBar.fillAmount = 0;

            if (Global.health <= 30)
                healthBar.color = Color.red;
            else
                healthBar.color = Color.green;
        }
        else
        {

            if (Global.bossHealth <= 0)
                healthBar.fillAmount = 0;

            float hello = Global.bossHealth / Global.maxBossHealth;
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, hello, Time.deltaTime * speed);

            if (Global.bossHealth <= Global.maxBossHealth/2)
                healthBar.color = Color.red;
            else
                healthBar.color = Color.green;
        }
	}
}
