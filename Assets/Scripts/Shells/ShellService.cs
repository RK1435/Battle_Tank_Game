using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellService : TankGenericSingleton<ShellService>
{
    [SerializeField]
    private ShellFactory shellFactory;

    private ObjectPool<ShellExplosion> shellPool;
    private int playerShellFiredCount = 0;


    void Start()
    {
        shellPool = new ObjectPool<ShellExplosion>();
    }

    public void ShellFired(Transform exitPoint, float multipler, float damage)
    {
        ShellExplosion shell = shellPool.GetItem();
        if(shell == default(ShellExplosion))
        {
            shell = shellFactory.CreateBullet();
            shellPool.NewItem(shell);
        }

        shell.SetShellProperties(exitPoint, multipler, damage);
    }

    public void PlayerFiredShell()
    {
        playerShellFiredCount++;
        ServiceEvents.Instance.OnShellFired(playerShellFiredCount);
    }

    internal void FreeShell(ShellExplosion shell)
    {
        shellPool.FreeItem(shell);
    }
}
