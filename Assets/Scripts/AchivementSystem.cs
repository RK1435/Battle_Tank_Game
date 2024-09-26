using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementSystem : TankGenericSingleton<AchivementSystem>
{
    public List<string> Achived;

    public AchivementSystem()
    {
        Achived = new List<string>();
    }

    private void OnEnable()
    {
        ServiceEvents.Instance.OnShellFired += BulletShootAchivement;
        ServiceEvents.Instance.OnEnemyDeath += EnemyKilledAchivement;
    }

    private void OnDisable ()
    {
        ServiceEvents.Instance.OnShellFired -= BulletShootAchivement;
        ServiceEvents.Instance.OnEnemyDeath -= EnemyKilledAchivement;
    }
    
    private void BulletShootAchivement(int count)
    {
        bool trigger = true;
        string achivementName = "ShellShoot";
        string achHeading = "";
        string achSubText = "";

        switch(count)
        {
            case 1: achivementName += "1";
                achHeading = "Fire Shell";
                achSubText = "Shoot you first shell";
                break;
            case 10: achivementName += "10";
                achHeading = "Shell Shooter Novoice";
                achSubText = "Shoot 10 shells";
                break;
            case 20: achivementName += "20";
                achHeading = "Shell Shooter Pro";
                achSubText = "Shot 20 shells";
                break;
            default: trigger = false;
                break;
        }

        if(trigger)
        {
            if (!IsAchived(achivementName))
                UIService.Instance.ShowAchivement(achHeading, achSubText);
            
        }
    }

    private bool IsAchived(string achivementName)
    {
        if (Achived.Contains(achivementName))
            return true;

        Achived.Add(achivementName);
        return false;
    }

    private void EnemyKilledAchivement(int count)
    {
        bool trigger = true;
        string achivementName = "Enemy Killed";
        string achHeading = "";
        string achSubText = "";
        switch(count)
        {
            case 1:
                achivementName += "1";
                achHeading = "First Kill";
                achSubText = "Kill you Enemy Shell";
                break;
            case 2:
                achivementName += "2";
                achHeading = "Enemy Killer Novoice";
                achSubText = "Kill 2 Enemy";
                break;
            case 3:
                achivementName += "3";
                achHeading = "Enemy Exterminator";
                achSubText = "Kill All Enemies";
                break;
            default:
                trigger = false;
                break;
        }

        if (trigger)
        {
            if (!IsAchived(achivementName))
                UIService.Instance.ShowAchivement(achHeading, achSubText);
        }
    }
}
