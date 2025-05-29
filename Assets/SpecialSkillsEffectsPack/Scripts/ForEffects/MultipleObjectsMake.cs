using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleObjectsMake : _ObjectsMakeBase
{
    public float m_startDelay;
    public int m_makeCount;
    public float m_makeDelay;
    public Vector3 m_randomPos;
    public Vector3 m_randomRot;
    public Vector3 m_randomScale;
    public bool isObjectAttachToParent = true;

    float m_startTime;
    float m_lastMakeTime;
    int m_currentCount;
    float m_scalefactor;

    void Start()
    {
        m_startTime = Time.time;
        m_lastMakeTime = m_startTime;
        m_scalefactor = VariousEffectsScene.m_gaph_scenesizefactor;
        m_currentCount = 0;
    }

    void Update()
    {
        if (!CanStart()) return;
        if (CanMakeNext())
        {
            MakeObjects();
            m_lastMakeTime = Time.time;
            m_currentCount++;
        }
    }

    bool CanStart()
    {
        return Time.time > m_startTime + m_startDelay;
    }

    bool CanMakeNext()
    {
        return m_currentCount < m_makeCount && Time.time > m_lastMakeTime + m_makeDelay;
    }

    void MakeObjects()
    {
        Vector3 basePos = transform.position + GetRandomVector(m_randomPos) * m_scalefactor;
        Quaternion baseRot = transform.rotation * Quaternion.Euler(GetRandomVector(m_randomRot));

        foreach (var prefab in m_makeObjs)
        {
            GameObject obj = Instantiate(prefab, basePos, baseRot);
            Vector3 scale = prefab.transform.localScale + GetRandomVector2(m_randomScale);
            if (isObjectAttachToParent)
                obj.transform.parent = this.transform;
            obj.transform.localScale = scale;
            if (prefab.TryGetComponent(out DamageCaster caster))
            {
                caster.DamageDealer(5);//테스트 지우ㅡㅓ
            }
        }
    }
}
