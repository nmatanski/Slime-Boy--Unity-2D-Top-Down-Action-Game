using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wave
{
    [SerializeField]
#pragma warning disable CA2235
    private List<Enemy> enemies;
#pragma warning restore CA2235
    public List<Enemy> Enemies
    {
        get { return enemies; }
        set { enemies = value; }
    }

    [SerializeField]
    private int count;
    public int Count
    {
        get { return count; }
        set { count = value; }
    }

    [SerializeField]
    private float timeBetweenSpawns;
    public float TimeBetweenSpawns
    {
        get { return timeBetweenSpawns; }
        set { timeBetweenSpawns = value; }
    }
}
