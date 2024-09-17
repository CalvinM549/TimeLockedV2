using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerRangedAttack : PlayerAttack
{
    public GameObject baseProjectile;
    public float projectileSpeed;

    public override void StartAttack()
    {
        Debug.Log("RangedAttackStart");
        base.StartAttack();

        // initialize windup things
    }

    public override void ExecuteAttack()
    {
        Debug.Log("RangedAttackExecute");
        Vector2 attackDirection = GetComponent<PlayerController>().PlayerToMouse();
        float rotation = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;


        GameObject projectile = Instantiate(baseProjectile, transform.position, Quaternion.Euler(0,0, rotation + -90));
        projectile.GetComponent<PlayerProjectileScript>().Initialize(attackDamage, projectileSpeed, attackDirection);
    }


}
