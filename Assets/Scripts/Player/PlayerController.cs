using System.Collections;
using HTNWIC.Items;
using FishNet.Object;
using FishNet.Object.Synchronizing;
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
        private WeaponManager weaponManager;
        private PlayerAnimations playerAnimations;

        private Vector2 move;

        [Header("Attack")]
        [SerializeField]
        private Transform attackPoint;
        [SerializeField]
        private float attackRange = 3f;

        [SerializeField]
        private LayerMask damageableLayer;

        [field: SyncVar(OnChange = nameof(OnIsMovingChange))]
        public bool isMoving { get; [ServerRpc] set; } = false;
        
        [field: SyncVar(OnChange = nameof(OnIsAttackingChange))]
        public bool isAttacking { get; [ServerRpc] set; } = false;
        
        public void OnMove(InputAction.CallbackContext context)
        {
            if(!base.IsOwner) return;
            move = context.ReadValue<Vector2>();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if(!base.IsOwner) return;
            if(context.performed)
            {
                if (weaponManager.CurrentWeapon == null || isAttacking) return;
                StartCoroutine(Attack());
            }
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            // get components
            playerMotor = GetComponent<PlayerMotor>();
            weaponManager = GetComponent<WeaponManager>();
            playerAnimations = GetComponent<PlayerAnimations>();
            // setup attack point
            // divide by 2 because the player has a scale of 0,5
            attackPoint.SetPositionAndRotation(new Vector3(0f, 2f, attackRange-1), Quaternion.identity);
        }

        public override void OnStartClient()
        {
            base.OnStartClient();
            // setup attack point
            // divide by 2 because the player has a scale of 0,5
            attackPoint.SetPositionAndRotation(new Vector3(0f, 2f, attackRange - 1), Quaternion.identity);
            playerAnimations = GetComponent<PlayerAnimations>();
            weaponManager = GetComponent<WeaponManager>();
            if(!base.IsOwner) return;
            // get components
            playerMotor = GetComponent<PlayerMotor>();
           }

        private void Update()
        {
            if (!base.IsOwner) return;
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

        private void OnIsMovingChange(bool oldIsMoving, bool newIsMoving, bool asServer)
        {
            playerAnimations.isMovingAnimation = newIsMoving;
        }
        
        private void OnIsAttackingChange(bool oldIsAttacking, bool newIsAttacking, bool asServer)
        {
            Debug.Log("OnIsAttackingChange: " + newIsAttacking);
            if (newIsAttacking)
            {
                switch (weaponManager.CurrentWeapon.type)
                {
                    case WeaponType.OneHanded:
                        playerAnimations.PlayAttackOHAnimation();
                        break;
                    default:
                        goto case WeaponType.OneHanded;
                }
            }
        }

        IEnumerator Attack() {
            isAttacking = true;
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

        [ServerRpc]
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
