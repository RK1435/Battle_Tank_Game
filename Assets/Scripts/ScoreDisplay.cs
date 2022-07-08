using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreDisplay : MonoBehaviour
{
    public Text shotCount;
    public Text killCount;

    private void Start()
    {
        shotCount.text = $"Shot Count = {0}";
        killCount.text = $"Kill Count = {0}";
    }

    public void SetKillCount(int count)
    {
        killCount.text = $"Kill Count = {count}";
    }

    public void SetShotCount(int count)
    {
        shotCount.text = $"Shot Count = {count}";
    }
        
}
