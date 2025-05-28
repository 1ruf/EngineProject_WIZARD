using UnityEngine;

public class Dummy : MonoBehaviour, IDamageable
{
    public void Damage(int damage)
    {
        print("대미지를 받았어요(DUMMY)" + damage);
    }
}
