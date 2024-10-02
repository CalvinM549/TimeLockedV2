using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTeleport : BossAttack
{
    private float elapsedTime;

    public Vector2[] teleportPoints;

    private int chosenPoint;
    private int lastPoint;

    // Warning visual variables
    public GameObject warning;
    public GameObject warningCircle;
    public AnimationCurve sizeCurve;
    public Vector2 endArea;
    private Vector2 startArea;

    // Start is called before the first frame update
    public override void StartAttack(Transform target)
    {
        endArea.Set(attackRange * 2, attackRange * 2);

        base.StartAttack(target);

        warning = Instantiate(warningCircle, transform.position, transform.rotation);
        StartCoroutine(DoWarning());
        // TODO create windup sprite
    }

    // Update is called once per frame
    public override void ExecuteAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach(Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                collider.GetComponent<Health>().TakeDamage(attackDamage);
            }
        }


        while(chosenPoint == lastPoint)
        {
            chosenPoint = Random.Range(0, teleportPoints.Length);
        }
        
        gameObject.transform.position = teleportPoints[chosenPoint];
        lastPoint = chosenPoint;

    }

    private IEnumerator DoWarning()
    {
        while (elapsedTime < windupTime)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / windupTime;
            float curveValue = sizeCurve.Evaluate(t);

            Vector3 areaSize = Vector2.Lerp(startArea, endArea, curveValue);

            warning.transform.localScale = areaSize;

            yield return null;
        }
        elapsedTime = 0;
        Destroy(warning);
        
    }
}
