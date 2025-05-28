using UnityEngine;

public class PlayerAnimation : MonoBehaviour, IPlayerComponent
{
    private Animator _animator;
    public void Initialize(Player player)
    {

    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetParam(string name,bool value) => _animator.SetBool(name, value);
    public void SetParam(string name,float value) => _animator.SetFloat(name, value);
    public void SetParam(string name, int value) => _animator.SetInteger(name, value);
    public void SetParam(string name) => _animator.SetTrigger(name);
}
