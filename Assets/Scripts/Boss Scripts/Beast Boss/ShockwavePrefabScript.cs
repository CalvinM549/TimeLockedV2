using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwavePrefabScript : MonoBehaviour
{
    private float expansionSpeed;
    private float maxAreaSize;
    private float shockwaveDuration;
    private int attackDamage;

    private Vector2 startAreaSize;

    // Start is called before the first frame update
    public void Initialize(float expansionSpeed, float maxAreaSize, float shockwaveDuration, int attackDamage)
    {
        this.expansionSpeed = expansionSpeed;
        this.maxAreaSize = maxAreaSize; 
        this.shockwaveDuration = shockwaveDuration;
        this.attackDamage = attackDamage;

        startAreaSize = transform.localScale;

        StartCoroutine(ExpandArea());
    }

    private IEnumerator ExpandArea()
    {
        while (transform.localScale.x < maxAreaSize)
        {
            float areaExpansionAmount = expansionSpeed * Time.deltaTime;
            transform.localScale += new Vector3(areaExpansionAmount, areaExpansionAmount, 0);
            yield return null;
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health>().TakeDamage(attackDamage);
        }
    }
}
