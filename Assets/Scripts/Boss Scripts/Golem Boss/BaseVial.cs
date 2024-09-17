using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseVial : MonoBehaviour
{
    private Vector2 targetPosition;
    private Vector2 startPosition;

    public AnimationCurve velocityCurve;

    private float timeToImpact;
    public Vector3 rotationSpeed;
    private float areaSize;

    public float elapsedTime = 0f;

    private GolemVialAttack.Vial chosenVial;

    public virtual void Initialize(Vector2 target, Vector3 rotation, float targetTime, float area, int damage, GolemVialAttack.Vial currentVial)
    {
        Debug.Log("VialSpawned");

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
        while (elapsedTime < timeToImpact)
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

    protected virtual void OnImpact()
    {
        
        Destroy(gameObject);
    }

    private void LightningVial()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(targetPosition, areaSize);
        foreach (Collider2D collider in colliders)
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
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.sprite =
        Sprite newSprite;
        CircleCollider2D circleCollider2D = new CircleCollider2D();
        circleCollider2D.enabled = true;

        circleCollider2D.radius = areaSize;

    }
    public void IceVial()
    {

    }


}
