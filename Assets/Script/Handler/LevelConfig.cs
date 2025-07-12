using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelConfig
{
    
    public static float GetGameTime(int level)
    {
        if (level == 1)
        {
            return 0.5f;
        }
        
        
        return Mathf.Min(2 + level , 30);
    }

    public static Vector2 GetSpawnTime(int level)
    {
        if (level == 1)
        {
            return new Vector2(2, 4);
        }

        var min = 2f / (level * level);
        var max = 1f / level;
        min = Mathf.Min(min, max);
        return new Vector2(min, max);
    }

    public static Vector2 GetEnemyHp(int level)
    {
        if (level == 1)
        {
            return Vector2.one * 5;
        }
        
        
        var min = 5 * level;
        var max = min + 2 * level;
        return new Vector2(min, max);
    }
}
