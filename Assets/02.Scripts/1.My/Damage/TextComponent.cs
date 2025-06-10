using Blade.Entities;
using TMPro;
using UnityEngine;

public class TextComponent : MonoBehaviour, IEntityComponent
{
    [SerializeField] private GameObject textComponent;

    private GameObject InstantiateText()
    {
        GameObject textObj = Instantiate(textComponent,null);
        textObj.SetActive(true);

        Vector3 origin = transform.position;
        textObj.transform.position = new Vector3(origin.x + GetRandom(-0.3f, 0.3f), origin.y + GetRandom(1f, 2.5f), origin.z + GetRandom(-0.3f, 0.3f));
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

    public void Initialize(Entity entity)
    {
    }
}
