using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;
    public float lifeTime;
    private float _lifeLeft;
    ObjectPool<Bullet> _pool;
    private void Start()
    {
        SetVelocity();
    }
    private void Update()
    {
        _lifeLeft -= Time.deltaTime;
        if (_lifeLeft <= 0)
            _pool.Release(this);
    }
    public void SetPool(ObjectPool<Bullet> pool)
    {
        Debug.Log("setpool");
        _pool = pool;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Health>().ActualHealth -= damage;
        }
        //else if (CompareTag("Lever"))
        //{

        //}
        _pool.Release(this);
    }
    public void ResetLifeLeft()
    {
        _lifeLeft = lifeTime;
    }
    public void SetVelocity()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }
}
