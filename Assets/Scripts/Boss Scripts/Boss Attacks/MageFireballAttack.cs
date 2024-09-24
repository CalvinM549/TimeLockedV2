using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageFireballAttack : BossAttack
{
    public GameObject warningCircle;

    private Transform target;

    private Vector3 targetArea;

    private Vector2 areaSize;

    //Fireball phase logic
    public int fireballCount;
    public float impactDelay;
    public float attackDuration;

    public override void StartAttack(Transform target)
    {
        this.target = target;
        Debug.Log("StartAttack");

        base.StartAttack(target);

        areaSize.Set(attackRange * 2, attackRange * 2);
    }

    public override void ExecuteAttack()
    {
        attackInProgress = true;
        Debug.Log("ExecuteAttack");

        StartCoroutine(ShootFireballs());
    }

    public IEnumerator ShootFireballs()
    {
        float delayBetween = attackDuration / fireballCount;

        Debug.Log("Start Shooting");

        for(int i=0; i<fireballCount; i++)
        {
            Debug.Log("Fireball #"+(i+1));
            targetArea = target.position;
            GameObject warning = Instantiate(warningCircle, targetArea, transform.rotation);
            StartCoroutine(FireBallDamage(warning));
            warning.transform.localScale = areaSize;
            yield return new WaitForSeconds(delayBetween);
        }

        Debug.Log("Finished Shooting");
        attackInProgress = false;
    }

    private IEnumerator FireBallDamage(GameObject warning)
    {
        yield return new WaitForSeconds(impactDelay);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(targetArea, attackRange);
        foreach(Collider2D collider in colliders)
        {
            if(collider.gameObject.tag == "Player")
            {
                collider.GetComponent<Health>().TakeDamage(attackDamage);
            }
        }
        Destroy(warning);
    }


}
