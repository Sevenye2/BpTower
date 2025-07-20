using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public static class LevelConfig
{
    
    public static float GetGameTime(int level)
    {
        return level switch
        {
            1 => 0.5f,
            2 => 0.8f,
            3 => 0.9f,
            4 => 1f,
            5 => 1.2f,
            6 => 1.4f,
            7 => 2f,
            8 => 2.5f,
            9 => 2.8f,
            10 => 3f,
            _ => Mathf.Min(level - 7, 30)
        };
    }

    public static Vector2 GetSpawnTime(int level)
    {
        return level switch
        {
            1 => new Vector2(2, 4),
            2 => new Vector2(2, 3),
            3 => new Vector2(1.5f, 3f),
            4 => new Vector2(1.5f, 2.5f),
            5 => new Vector2(1.5f, 2),
            6 => new Vector2(1.2f, 1.5f),
            7 => new Vector2(1f, 1.5f),
            8 => new Vector2(1f, 1.2f),
            9 => new Vector2(0.5f, 1f),
            10 => new Vector2(0.2f, 0.8f),
            _ => new Vector2(0.1f, 0.1f)
        };
    }

    public static Vector2 GetEnemyHp(int level)
    {
        return level switch
        {
            1 => new Vector2(5, 5),
            2 => new Vector2(5, 5),
            3 => new Vector2(5, 5),
            4 => new Vector2(6, 10),
            5 => new Vector2(10, 20),
            6 => new Vector2(15, 30),
            7 => new Vector2(20, 40),
            8 => new Vector2(50, 80),
            9 => new Vector2(100, 200),
            10 => new Vector2(500, 1000),
            _ => new Vector2(50 * level, 100 * level)
        };
    }
}
