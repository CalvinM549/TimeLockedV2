using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBurstAttack : BossAttack
{
    public GameObject warningCircle;
    private GameObject warning;

    public Vector2 endArea;
    private Vector2 startArea;

    public AnimationCurve sizeCurve;
    public float elapsedTime = 0f;

    public float damageDelay;

    public override void StartAttack(Transform target)
    {
        base.StartAttack(target);
        Debug.Log("StartWindup");
        elapsedTime = 0f;


        endArea.Set(attackRange*2, attackRange* 2);
    }

    public override void ExecuteAttack()
    {
        attackInProgress = true;
        Debug.Log("ExecuteAttack");

        warning = Instantiate(warningCircle, transform.position, transform.rotation);
        StartCoroutine(AttackDelay());
        Debug.Log("Test");

    }

    public IEnumerator AttackDelay()
    {
        while(elapsedTime < damageDelay)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / windupTime;
            float curveValue = sizeCurve.Evaluate(t);

            Vector3 areaSize = Vector2.Lerp(startArea, endArea, curveValue);

            warning.transform.localScale = areaSize;

            yield return null;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                collider.GetComponent<Health>().TakeDamage(attackDamage);
            }
        }

        Destroy(warning);
        attackInProgress = false;

    }

    private void OnDestroy()
    {
        if(warning != null)
        {
            Destroy(warning);
        }
    }

}
