// Niko V‰‰n‰nen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Projectile1 projectilePrefab;

    private IObjectPool<Projectile1> objectPool;
    private bool collectionCheck = true;
    private int defaultCapacity = 100;
    private int maxCapacity = 200;
    private PlayerScript mainScript;
    private Rigidbody2D rb;
    private int damage;

    private Projectile1 CreateProjectile()
    {
        Projectile1 projectileInstance = Instantiate(projectilePrefab);
        projectileInstance.ObjectPool = objectPool;
        return projectileInstance;
    }

    private void OnReleaseToPool(Projectile1 pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    private void OnGetFromPool(Projectile1 pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    private void OnDestroyPooledObject(Projectile1 pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    public void ShootRight(int weaponLevel)
    {
        Projectile1 bulletObject = objectPool.Get();
        bulletObject.totalDamage = bulletObject.baseDamage * weaponLevel;
        bulletObject.transform.position = rb.position;
        bulletObject.GetComponent<Rigidbody2D>().velocity = new Vector2(15f, 0f);
        bulletObject.Deactivate();
    }

    public void ShootLeft(int weaponLevel)
    {
        Projectile1 bulletObject = objectPool.Get();
        bulletObject.totalDamage = bulletObject.baseDamage * weaponLevel;
       //Debug.Log(bulletObject.baseDamage + "  " + weaponLevel + "  " + bulletObject.totalDamage);
        bulletObject.transform.position = rb.position;
        bulletObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-15f, 0f);
        bulletObject.Deactivate();
    }

    private void Awake()
    {
        mainScript = gameObject.GetComponent<PlayerScript>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        objectPool = new ObjectPool<Projectile1>(CreateProjectile,
            OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
            collectionCheck, defaultCapacity, maxCapacity);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
