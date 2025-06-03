using UnityEngine;

public class TextComponent : MonoBehaviour
{
    [SerializeField] private GameObject textComponent;
    public void Damaged(int dmg)//나중에 컴포넌트
    {
        if (InstantiateText().TryGetComponent(out DamageText damage))
        {
            damage.SetText(dmg.ToString(), ()=>Destroy(damage.gameObject));//나중에 pooling
        }
    }
    private GameObject InstantiateText()
    {
        GameObject textObj = Instantiate(textComponent,transform);
        textObj.transform.position = transform.position;
        textObj.SetActive(true);
        return textObj;
    }
}
