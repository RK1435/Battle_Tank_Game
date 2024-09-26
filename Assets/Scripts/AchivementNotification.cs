using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AchivementNotification : MonoBehaviour
{
    public Animator anim;
    public Text HeadingText;
    public Text SubText;

    private void Start()
    {
        HeadingText.text = "Welcome";
        SubText.text = "Play the Game";
    }

    public void ShowAchivement(string Heading, string subString)
    {
        anim.ResetTrigger("NewNotification");
        HeadingText.text = Heading;
        SubText.text = subString;

        anim.SetTrigger("NewNotification");
        Debug.Log("triggred");
    }
}
