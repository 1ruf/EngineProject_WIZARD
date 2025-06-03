using UnityEngine;

public class Dummy : MonoBehaviour, IDamageable
{
    [SerializeField] private TextComponent textM;//나중에 컴포넌트화
    public void Damage(int damage)
    {
        print("대미지를 받았어요(DUMMY)" + damage);
        textM.Damaged(damage);
    }
}
