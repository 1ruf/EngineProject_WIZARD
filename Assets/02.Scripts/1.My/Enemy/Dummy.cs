using UnityEngine;

public class Dummy : MonoBehaviour, IDamageable
{
    [SerializeField] private TextComponent textM;//���߿� ������Ʈȭ
    public void Damage(int damage)
    {
        print("������� �޾Ҿ��(DUMMY)" + damage);
        textM.Damaged(damage);
    }
}
