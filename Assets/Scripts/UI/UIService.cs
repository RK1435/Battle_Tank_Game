using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIService : TankGenericSingleton<UIService>
{
    private TankView tankView;

    [SerializeField]
    private Joystick joystick;

    [SerializeField]
    private Joybutton joybutton;

    [SerializeField]
    private AchivementNotification notification;

    [SerializeField]
    private ScoreDisplay scoreDisplay;


    private void OnEnable()
    {
        ServiceEvents.Instance.OnShellFired += SetShellShotCount;
        ServiceEvents.Instance.OnEnemyDeath += SetEnemyKillCount;
    }

    private void OnDisable()
    {
        ServiceEvents.Instance.OnShellFired += SetShellShotCount;
        ServiceEvents.Instance.OnEnemyDeath += SetEnemyKillCount;
    }

    public Vector3? GetJoyMoveDirection() => joystick.Direction;
    public bool IsFirePressed() => joybutton.Pressed;

    public void SetShellShotCount(int count)
    {
        scoreDisplay.SetShotCount(count);
    }

    public void SetEnemyKillCount(int count)
    {
        scoreDisplay.SetKillCount(count);
    }

    public void ShowAchivement(string mainText, string subText)
    {
        notification.ShowAchivement(mainText, subText);
        Debug.Log("show ach");
    }
}
