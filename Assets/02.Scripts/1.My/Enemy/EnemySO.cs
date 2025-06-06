using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "SO/Enemy")]
public class EnemySO : ScriptableObject
{
    public int Hp;
    public int Dmg;

    public float AttackSpeed;
    public float WalkSpeed;
}
