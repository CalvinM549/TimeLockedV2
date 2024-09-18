using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseVial : MonoBehaviour
{
    public Sprite areaSprite;

    protected SpriteRenderer spriteRenderer;
    protected CircleCollider2D circleCollider;

    protected Vector2 target;
    protected Vector2 startPosition;

    public AnimationCurve velocityCurve;

    protected float timeToImpact;
    protected Vector3 rotationSpeed;
    protected float area;

    protected int damage;
    protected float vialDuration;

    protected float elapsedTime = 0f;


    public virtual void Initialize(int damage, float area, Vector2 target, Vector3 rotationSpeed, float timeToImpact, float vialDuration)
    {
        this.damage = damage;
        this.area = area;
        this.rotationSpeed = rotationSpeed;
        this.target = target;
        this.timeToImpact = timeToImpact;
        this.vialDuration = vialDuration;

        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius = area;

        spriteRenderer = GetComponent<SpriteRenderer>();

        startPosition = transform.position;

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

            transform.position = Vector2.Lerp(startPosition, target, curveValue);
            yield return null;
        }
        OnImpact();
    }

    protected abstract void OnImpact();

    protected abstract void VialEffects();


 


}
//int damage, float area, Vector2 target, Vector3 rotation, float timeToImpact