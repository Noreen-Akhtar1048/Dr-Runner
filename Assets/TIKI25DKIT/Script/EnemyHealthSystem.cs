using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="RobotEnemy/Enemy")]
public class EnemyHealthSystem : ScriptableObject
{
    public EnemyHealth Enemy;
    public int MaxHealth; 
    public Image MaxHealthBar;


}
