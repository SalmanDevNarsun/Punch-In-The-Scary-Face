using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public Animator _animator;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6)
        {
            LevelTask.Instance.TotalEnemiesKilled ++;
            Debug.Log("Enemy Ragdoll");
        }
    }
}
