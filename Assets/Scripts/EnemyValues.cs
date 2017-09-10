using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemysData", menuName = "Config/Enemys", order = 1)]
public class EnemyValues : ScriptableObject {
  
    public int life;

    public int attack;

    public float attackrange;

    public float recoverytime;

    public float viewdistance;

    public float chasedistance;

    public float acceleration;

    public float maxspeed;
}
