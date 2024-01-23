using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombatController : MonoBehaviour
{
   [SerializeField] private WeaponSettings _weaponSettings;
   [SerializeField] private ArmorSettings _armorSettings;
   [SerializeField,Range(1, 100)] private float _agroRange;
   [SerializeField] private float _agroDelay;
   [SerializeField] private float _inCombatAgroDelay;
   [SerializeField] private bool _inCombat;
   [Header("Debug")]
   [SerializeField] private Mesh _agroMesh;

   private float _currentAgroTime;
   private int _controllableAmount;
   public bool _alreadyHasAttackCommand;

   private Unit  _currentUnit;
   private IDamagable _currentDamagable;

   public event Action OnCritAttack;

   public WeaponSettings Weapon => _weaponSettings;
   public ArmorSettings Armor => _armorSettings;

   public void Init(Unit unit)
   {
      _currentUnit = unit;
      _weaponSettings.Init(_currentUnit);
      _armorSettings.Init();
   }


   private void Update()
   {
      TryAgro();
      TryAttackTarget();
   }


   private void TryAgro()
   {
      _currentAgroTime += Time.deltaTime;
      CheckNearTargets();
   }

   private void CheckNearTargets()
   {
      // if(_currentDamagable != null)
      //    return;

      // if(_currentAgroTime > _agroDelay)
      //    return;

      // Collider[] colls = Physics.OverlapSphere(transform.position, _agroRange);
      // List<Unit> damagables = new List<Unit>();
      // if(colls.Length > 0)
      // {
      //    for (int i = 0; i < colls.Length; i++)
      //       if(colls[i].TryGetComponent(out Unit unit) && _currentUnit.Faction != unit.Faction)
      //          damagables.Add(unit);

      //    if(damagables.Count > 0)
      //       _currentDamagable = damagables[0].GetComponent<IDamagable>();
      // }
   }

   public void AttackCommand(IDamagable unitToAttack, int controllableAmount)
   {
      _currentDamagable = unitToAttack;
      _controllableAmount = controllableAmount;
      _currentAgroTime = _inCombatAgroDelay;
   }


   private void TryAttackTarget()
   {
         if(_currentDamagable == null)
            return;

         if(!_currentDamagable.CanBeDamaged)
         {
            ExitCombatState();
            return;
         }

         var unitToAttackPos = _currentDamagable.GameObject.transform.position;
         if(Vector3.Distance(transform.position, unitToAttackPos) <= Weapon.CurrentWeapon.Range)
         {
            _currentUnit.Locomotion.StopAgent(true);
            _currentAgroTime = 0;
            RotateTowardsTarget(unitToAttackPos);
            AttackTarget();
         }
         else if(Vector3.Distance(transform.position, unitToAttackPos) >  Weapon.CurrentWeapon.Range && _currentAgroTime > _inCombatAgroDelay)
         {
            _currentUnit.Locomotion.StopAgent(false);
            _currentUnit.View.Attack(-1);
            _currentUnit.View.ExitCombat();
            _currentUnit.Locomotion.ToggleRotation(true);
            _currentUnit.Locomotion.MoveAgent(unitToAttackPos);
         }
   }
   
   private void AttackTarget()
   {
      _inCombat = true;
      _currentUnit.View.EnterCombat();

      if(Weapon.CurrentWeaponAttackTime <= 0)
      {
         _currentUnit.View.Attack(Random.Range(0,  Weapon.CurrentWeapon.AnimationAttackTriggers.Length));
         Weapon.CurrentWeaponAttackTime =  Weapon.CurrentWeapon.TimeToAttack;
      }
      else
         Weapon.CurrentWeaponAttackTime -= Time.deltaTime;
   }

   private void RotateTowardsTarget(Vector3 unitToAttackPos)
   {
      _currentUnit.Locomotion.ToggleRotation(false);
      var dir = unitToAttackPos - transform.position;
      Quaternion desiredRot = Quaternion.LookRotation(dir, Vector3.up);
      transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, 600 * Time.deltaTime);
   }

   public void DamageEvent()
   {
      var combatComponent = _currentDamagable.GameObject.GetComponent<CombatController>();
      float endDamage = 0;
      if(combatComponent != null)
      {
         endDamage = Weapon.CurrentWeapon.IsCrit(OnCritAttack)
         ? DamageCalculator.CalculateCrit(Weapon.CurrentWeapon, _currentUnit.Stats, combatComponent.Armor.Defence) :
         DamageCalculator.CalculateDamage(Weapon.CurrentWeapon, _currentUnit.Stats, combatComponent.Armor.Defence);
      }
      else
         endDamage = Weapon.CurrentWeapon.DefaultDamage;

      _currentDamagable?.Damage(endDamage, this);
   }

   public void ExitCombatState()
   {
      _inCombat = false;
      _currentDamagable = null;
      _currentAgroTime = 0;
      _currentUnit.View.ExitCombat();
      StopAllCoroutines();
   }

   private void OnDrawGizmosSelected()
   {
      Gizmos.color = Color.red;
      Gizmos.DrawMesh(_agroMesh, new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z),
       Quaternion.identity, new Vector3(_agroRange, _agroRange, _agroRange));

      if( Weapon.CurrentWeapon == null)
         return;

      Gizmos.color = Color.blue;
      Gizmos.DrawWireSphere(transform.position,  Weapon.CurrentWeapon.Range);
   }
}
