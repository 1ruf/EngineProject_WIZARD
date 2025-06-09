using TMPro;
using UnityEngine;

public class TextComponent : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject textComponent;

    private GameObject InstantiateText()
    {
        GameObject textObj = Instantiate(textComponent,transform);
        textObj.SetActive(true);

        Vector3 origin = transform.position;
        textObj.transform.position = new Vector3(origin.x + GetRandom(-0.3f, 0.3f), origin.y + GetRandom(0f, 0.5f), origin.z + GetRandom(-0.3f, 0.3f));
        return textObj;
    }

    private float GetRandom(float min, float max)
    {
        return Random.Range(min, max);
    }

    public void Damage(int dmg)
    {
        if (InstantiateText().TryGetComponent(out DamageText damage))
        {
            damage.SetText(dmg.ToString(), () => Destroy(damage.gameObject));//³ªÁß¿¡ pooling
        }
    }
}
