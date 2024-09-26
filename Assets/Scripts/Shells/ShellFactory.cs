using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellFactory : MonoBehaviour
{
    [SerializeField]
    private ShellExplosion shellPrefab;

    public ShellExplosion CreateBullet()
    {
        return Instantiate(shellPrefab);
    }
}
