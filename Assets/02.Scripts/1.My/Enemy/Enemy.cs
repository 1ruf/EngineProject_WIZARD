using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Hp { get; private set; }
    public int Dmg { get; private set; }

    private float _atkSpeed;
}
