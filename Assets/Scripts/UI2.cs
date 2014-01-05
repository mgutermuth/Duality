using UnityEngine;
using System.Collections;

public class UI2 : MonoBehaviour {

    public Texture2D mainBackground;
    public Texture2D portrait;
    public Texture2D lightIndicator;
    public Texture2D darkIndicator;
    public Texture2D lightAbility;
    public Texture2D darkAbility;
    public Texture2D healthBar;
    public Texture2D timerBar;
    private Texture2D backGround;

    public GameObject playerTwo;
   

    Vector2 pos = new Vector2(20, 20);

    void Start()
    {
        backGround = mainBackground;

    }
    void Update()
    {
        backGround = mainBackground;
    }
    // Use this for initialization
    void OnGUI()
    {

        GUI.BeginGroup(new Rect(Screen.width - 339, 0, 600, 228));
        GUI.DrawTexture(new Rect(0, 0, 339, 200), backGround);//the main back part
       // GUI.BeginGroup(new Rect(0, 0, 300, 300));
       GUI.DrawTexture(new Rect(240, -10, 90, 115), portrait);//char portrait

       if (playerTwo.GetComponent<PlayerTwoController>().inLight)//if they're in light..
        {
            GUI.DrawTexture(new Rect(210, 35, 35, 43), lightIndicator);
            GUI.DrawTexture(new Rect(218, 95, 53, 75), lightAbility);
        }
        else //if not
        {
            GUI.DrawTexture(new Rect(210, 35, 35, 43), darkIndicator);

            GUI.DrawTexture(new Rect(218, 95, 53, 75), darkAbility);
        }

        //health bar
      //  GUI.BeginGroup(new Rect(145, 57, 220, 22));
        float maxHealth = playerTwo.GetComponent<PlayerTwoController>().maxHealth;
        float curHealth = playerTwo.GetComponent<PlayerTwoController>().health;
        GUI.DrawTexture(new Rect(42 + (160 - (160 *(curHealth / maxHealth))), 57, 160 * curHealth / maxHealth, 22), healthBar);
        //GUI.EndGroup();

        //ability one bar
        float maxTime = playerTwo.GetComponent<PlayerTwoController>().abilityOneCoolDown;
        float currentTimer = playerTwo.GetComponentInChildren<PlayerTwoController>().timeCooledDown;
        //GUI.BeginGroup(new Rect(145, 98, 108, 200));
        GUI.DrawTexture(new Rect(92 + (108 - (108 * ((maxTime - currentTimer)/maxTime))), 98, 108 * ((maxTime - currentTimer) / maxTime), 25), timerBar);


        float maxTime2 = playerTwo.GetComponent<PlayerTwoController>().abilityTwoCoolDown;
        float currentTimer2 = playerTwo.GetComponent<PlayerTwoController>().timeCooledDown2;
        GUI.DrawTexture(new Rect(92  + (108 - (108 * ((maxTime2 - currentTimer2)/maxTime2))), 145, 108 * ((maxTime2 - currentTimer2) / maxTime2), 25), timerBar);




      //  GUI.EndGroup();
        GUI.EndGroup();
    }
}
