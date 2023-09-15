using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using HTNWIC.Items;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HTNWIC.Player
{
    [RequireComponent(typeof(PlayerMotor))]
    [RequireComponent(typeof(PlayerAnimations))]
    [RequireComponent(typeof(WeaponManager))]
    public class PlayerController : NetworkBehaviour
    {
        private PlayerMotor playerMotor;
        private PlayerAnimations playerAnimations;
        private WeaponManager weaponManager;

        private Vector2 move;

        [Header("Attack")]
        [SerializeField]
        private Transform attackPoint;
        [SerializeField]
        private float attackRange = 3f;

        [SerializeField]
        private LayerMask damageableLayer;

        public bool isMoving { get; private set; } = false;
        public bool isAttacking { get; private set; } = false;

        public void OnMove(InputAction.CallbackContext context)
        {
            move = context.ReadValue<Vector2>();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                if (weaponManager.CurrentWeapon == null || isAttacking) return;
                StartCoroutine(Attack());
            }
        }

        private void Start()
        {
            // get components
            playerMotor = GetComponent<PlayerMotor>();
            playerAnimations = GetComponent<PlayerAnimations>();
            weaponManager = GetComponent<WeaponManager>();
            // setup attack point
            Debug.Log(attackPoint.position);
            // divide by 2 because the player has a scale of 0,5
            attackPoint.SetPositionAndRotation(new Vector3(0f, 2f, attackRange-1), Quaternion.identity);
            Debug.Log(attackPoint.position);
        }

        private void Update()
        {
            if (!isLocalPlayer) return;
            Vector3 movementDirection = new Vector3(move.x, 0f, move.y);
            movementDirection.Normalize();

            playerMotor.Move(movementDirection);

            playerMotor.Rotate(movementDirection);

            if (move != Vector2.zero)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
        }

        IEnumerator Attack() {
            isAttacking = true;
            switch (weaponManager.CurrentWeapon.type)
            {
                case WeaponType.OneHanded:
                    playerAnimations.PlayAttackOHAnimation();
                    break;
                default:
                    goto case WeaponType.OneHanded;
            }
            // deal damage
            Collider[] damageableHited = new Collider[10];
            Physics.OverlapSphereNonAlloc(attackPoint.position, attackRange, damageableHited, damageableLayer);
            StartCoroutine(DealDamageToTargets(damageableHited));
            // wait for animation to finish
            yield return new WaitForSeconds(0.84f);
            isAttacking = false;
        }

        IEnumerator DealDamageToTargets(Collider[] targets)
        {
            foreach(Collider target in targets)
            {
                if (target == null) continue;
                CmdDealDamage(target.gameObject, this.gameObject);
            }
            yield return null;
        }

        [Command]
        public void CmdDealDamage(GameObject target, GameObject attacker)
        {
            if(weaponManager == null)
            {
                Debug.LogError("Weapon manager is null");
                return;
            }
            if(weaponManager.CurrentWeapon == null)
            {
                Debug.LogError("Current weapon is null");
                return;
            }
            // calculate damage
            DamageData damageData = new DamageData(attacker, weaponManager.CurrentWeapon.physicalDamage, weaponManager.CurrentWeapon.magicalDamage, weaponManager.CurrentWeapon.trueDamage, 0f, 0f, 0f);
            Debug.Log("Dealing damage to " + target.name + " with " + damageData.PhysicalDamageAmount + " physical damage, " + damageData.MagicalDamageAmount + " magical damage and " + damageData.TrueDamageAmount + " true damage.");
            if(target.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damageData);
            }
            else
            {
                Debug.LogError("Target " + target.name + " does not have a IDamageable component");
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
