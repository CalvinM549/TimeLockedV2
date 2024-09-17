using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VialPrefabScript : MonoBehaviour
{
    private Vector2 targetPosition;
    private Vector2 startPosition;

    public AnimationCurve velocityCurve;

    private float timeToImpact;
    public  Vector3 rotationSpeed;
    private float areaSize;
    private int damage;

    public float elapsedTime = 0f;

    private GolemVialAttack.Vial chosenVial;

    public void Initialize(Vector2 target, Vector3 rotation, float targetTime, float area, int damage, GolemVialAttack.Vial currentVial)
    {
        Debug.Log("VialSpawned");

        this.damage = damage;
        areaSize = area;
        rotationSpeed = rotation;
        targetPosition = target;
        timeToImpact = targetTime;
        startPosition = transform.position;
        chosenVial = currentVial;

        StartCoroutine(MoveOverTime());
    }

    private IEnumerator MoveOverTime()
    {
        while(elapsedTime < timeToImpact)
        {
            elapsedTime += Time.deltaTime;

            transform.Rotate(rotationSpeed * Time.deltaTime);

            float t = elapsedTime / timeToImpact;
            float curveValue = velocityCurve.Evaluate(t);

            transform.position = Vector2.Lerp(startPosition, targetPosition, curveValue);
            yield return null;
        }
        OnImpact();
    }

    private void OnImpact()
    {
        switch (chosenVial)
        {
            case GolemVialAttack.Vial.LIGHTNING:
                LightningVial();
                break;

            case GolemVialAttack.Vial.ACID:
                break;

            case GolemVialAttack.Vial.ICE:
                break;
        }
        Destroy(gameObject);
    }    

    private void LightningVial()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(targetPosition, areaSize);
        foreach(Collider2D collider in colliders)
        {

            Debug.DrawLine(transform.position, collider.transform.position, Color.yellow, 2f);
            if (collider.CompareTag("Player"))
            {
                collider.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    private void AcidVial()
    {

    }
    public void IceVial()
    {

    }
    
    
}
