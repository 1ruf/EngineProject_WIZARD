using UnityEngine;

public class Dummy : MonoBehaviour, IDamageable
{
    public void Damage(int damage)
    {
        print("������� �޾Ҿ��(DUMMY)" + damage);
    }
}
