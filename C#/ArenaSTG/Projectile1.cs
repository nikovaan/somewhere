// Niko V‰‰n‰nen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile1 : MonoBehaviour
{
    public int baseDamage;
    public int fireRate;
    private IObjectPool<Projectile1> objectPool;
    private float timeToLive;
    public int totalDamage;
    public IObjectPool<Projectile1> ObjectPool { set => objectPool = value; }

    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(timeToLive));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // TODO: check for specific enemy types?
            //Debug.Log("projectile1 collided with tag enemy");
            //Debug.Log(totalDamage);
        }
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        rb2d.useFullKinematicContacts = true;
        rb2d.velocity = new Vector2(0f, 0f);
        rb2d.angularVelocity = 0f;
        objectPool.Release(this);
    }

    void OnEnable()
    {
        baseDamage = 1;
        fireRate = 10;
        timeToLive = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
