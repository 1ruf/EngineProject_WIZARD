using Blade.Entities;
using System.Collections;
using UnityEngine;

public class DropItem : MonoBehaviour, ICollectable
{
    [SerializeField] private CrystalSO crystalSO;
    [SerializeField] private EntityFinderSO playerFinder;
    [SerializeField] private float moveSpeed;

    private Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        Follow();
    }
    private void Follow()
    {
        StartCoroutine(MoveToTartget(playerFinder.Target.transform));
    }
    private IEnumerator MoveToTartget(Transform target)
    {
        _rb.isKinematic = false;
        while (true)
        {
            yield return null;
            _rb.AddForce((target.position - transform.position) * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
    }

    public CrystalSO Collect()
    {
        Destroy(gameObject);
        return crystalSO;
    }
}
