using UnityEngine;

public class UnitView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    public readonly int SpeedHash = Animator.StringToHash("Speed");
    public readonly int AttackHash = Animator.StringToHash("WeaponAttack");
    public readonly int CombatHash = Animator.StringToHash("InCombat");
    public readonly int CombatIdleHash = Animator.StringToHash("Hands_attack_idle");

    public void UpdateLocomotion(float speed) => _animator.SetFloat(SpeedHash, speed);
    public void SetBool(string boolName, bool value) =>  _animator.SetBool(boolName, value);
    public void SetTrigger(string triggerName) => _animator.SetTrigger(triggerName);
    public void SetInt(string intName, int value) => _animator.SetInteger(intName, value);
    public void PlayAnimation(int animation, float fadeTime)
    {
        _animator.Update(0);
        _animator.CrossFade(animation,fadeTime);
    } 

    public void EnterCombat() => _animator.SetBool(CombatHash, true);
    public void ExitCombat() => _animator.SetBool(CombatHash, false);
    public void Attack(int value) => _animator.SetInteger(AttackHash, value); 

    
}
