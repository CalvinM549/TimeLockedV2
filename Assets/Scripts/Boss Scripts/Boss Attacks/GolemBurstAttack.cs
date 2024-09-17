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

    public override void StartAttack(Transform target)
    {
        elapsedTime = 0f;
        Debug.Log("StartAttack");

        base.StartAttack(target);

        endArea.Set(attackRange*2, attackRange* 2);

        warning = Instantiate(warningCircle, transform.position, transform.rotation);
        StartCoroutine(WarningArea());
        Destroy(warning, (windupTime+0.01f));
    }

    public override void ExecuteAttack()
    {
        attackInProgress = true;
        Debug.Log("ExecuteAttack");

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                collider.GetComponent<Health>().TakeDamage(attackDamage);
            }
        }
    }

    public IEnumerator WarningArea()
    {
        while(elapsedTime < windupTime)
        {
            if(warning == null)
            {
                yield break;
            }

            elapsedTime += Time.deltaTime;

            float t = elapsedTime / windupTime;
            float curveValue = sizeCurve.Evaluate(t);

            Vector3 areaSize = Vector2.Lerp(startArea, endArea, curveValue);

            warning.transform.localScale = areaSize;

            yield return null;
        }
        
    }

}
