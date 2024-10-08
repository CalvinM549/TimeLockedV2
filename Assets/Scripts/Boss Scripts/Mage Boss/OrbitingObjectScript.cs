using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingObjectScript : MonoBehaviour
{
    private bool isInitialized;

    private Rigidbody2D rb;

    private BossAttack attack;
    public float delayBetweenAttacks;

    public GameObject centralObject;
    public Transform centralTransform;
    public float orbitDistance;
    public float orbitSpeed;
    public float smoothMovementSpeed;

    public float angle;

    void Start()
    {
        attack = GetComponent<MageWaveAttack>();
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(WaveAttacks());
    }

    public void Initialize(Transform target, float orbitDistance)
    {
        centralObject = target.gameObject;
        centralTransform = target;
        this.orbitDistance = orbitDistance;
        isInitialized = true;

    }

    void FixedUpdate()
    {
        if (isInitialized && centralObject != null) 
        {
            // Update angle based on orbit speed
            angle += orbitSpeed * Time.fixedDeltaTime;

            // Calculate the new desired position for smooth orbiting
            float radians = angle * Mathf.Deg2Rad;
            Vector2 targetPosition = new Vector2(
                centralTransform.position.x + Mathf.Cos(radians) * orbitDistance,
                centralTransform.position.y + Mathf.Sin(radians) * orbitDistance
            );

            // Smoothly move the Rigidbody towards the target position
            Vector2 currentPosition = rb.position;
            Vector2 newPosition = Vector2.Lerp(currentPosition, targetPosition, smoothMovementSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
        }
    }
    
    private IEnumerator WaveAttacks()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayBetweenAttacks);

            attack.StartAttack(centralTransform);
            yield return null;
            attack.ExecuteAttack();
        }
    }
}