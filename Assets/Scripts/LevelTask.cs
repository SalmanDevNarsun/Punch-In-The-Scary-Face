using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTask : MonoBehaviour
{
    public static LevelTask Instance;

    public int EnemiesToKill;
    public int TotalEnemiesKilled;
    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        if(EnemiesToKill == TotalEnemiesKilled)
        {
            Debug.Log("Level Complete");

            TotalEnemiesKilled = 0;
        }
    }
}
