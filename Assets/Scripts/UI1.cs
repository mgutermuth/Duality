using UnityEngine;
using System.Collections;

public class UI1 : MonoBehaviour
{


    public Texture2D mainBackground;
    public Texture2D portrait;
    public Texture2D lightIndicator;
    public Texture2D darkIndicator;
    public Texture2D lightAbility;
    public Texture2D darkAbility;
    public Texture2D healthBar;
    public Texture2D timerBar;
    private Texture2D backGround;

    public GameObject playerOne;
   

    Vector2 pos = new Vector2(20, 20);

    void Start()
    {
      //  backGround = mainBackground;
	//	playerOne = GameObject.Find("PlayerOne");

    }
    void Update()
    {
      //  backGround = mainBackground;
    }
    // Use this for initialization
    void OnGUI()
    {

        GUI.BeginGroup(new Rect(0, 0, 400, 228));
        //GUI.Box(new Rect(0, 0, 424, 285), mainBackground);
        GUI.DrawTexture(new Rect(0, 0, 339, 200), mainBackground);
       // GUI.BeginGroup(new Rect(0, 0, 300, 300));
        GUI.DrawTexture(new Rect(12, -10, 90, 115), portrait);
        if (playerOne.GetComponent<PlayerOneController>().inLight)
        {
            GUI.DrawTexture(new Rect(100, 35, 35, 43), lightIndicator);
            GUI.DrawTexture(new Rect(73, 95, 53, 75), lightAbility);
        }
        else
        {
            GUI.DrawTexture(new Rect(100, 35, 35, 43), darkIndicator);

            GUI.DrawTexture(new Rect(73, 95, 53, 75), darkAbility);
        }
       
        //health bar
      //  GUI.BeginGroup(new Rect(145, 57, 160, 22));
        GUI.DrawTexture(new Rect(145, 57, 160 * playerOne.GetComponent<PlayerOneController>().health / playerOne.GetComponent<PlayerOneController>().maxHealth, 22), healthBar);
      //  GUI.EndGroup();


        float tmp = playerOne.GetComponentInChildren<Shooter>().abilityOneCoolDown;
        GUI.BeginGroup(new Rect(145, 98, 108, 200));
        GUI.DrawTexture(new Rect(0, 0, 108 * ((tmp - playerOne.GetComponentInChildren<Shooter>().timeCooledDown) / tmp), 25), timerBar);
     //   GUI.EndGroup();

        float tmp2 = playerOne.GetComponentInChildren<Shooter>().abilityTwoCoolDown;
        //GUI.BeginGroup(new Rect(145, 250, 108, 22));
        GUI.DrawTexture(new Rect(0, 47, 108 * ((tmp2 - playerOne.GetComponentInChildren<Shooter>().timeCooledDown2) / tmp2), 25), timerBar);
        GUI.EndGroup();



// GUI.EndGroup();
        //draw the filled-in part:
        // GUI.BeginGroup(new Rect(0, 0, 424 * barDisplay, 285));
        //GUI.Box(new Rect(0, 0, 424, 285), fullTex);
        // GUI.EndGroup();
        GUI.EndGroup();
    }
}
