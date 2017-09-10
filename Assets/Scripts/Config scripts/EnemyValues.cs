using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Config/Enemys", order = 1)]
public class EnemyValues : ScriptableObject
{

    public int life;

    public int attack;

    public float speed;

    public float recoverytime;

    public float attackrange;

    public float viewdistance;

    public float chasedistance;
	
	
}
