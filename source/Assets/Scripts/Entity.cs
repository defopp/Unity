using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected int lives;
    
    public virtual void GetDamage()
    {
        lives--;
        Debug.Log("У противника " + lives);
        if (lives < 1)
            Die();
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
        gameObject.tag = "enemy_dead";
        LevelController.Instance.EnemiesCount();
    }
}

