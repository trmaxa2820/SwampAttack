using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;

    private void OnEnable()
    {
        StartCoroutine(DestroyAfterLife());
    }

    private void Update()
    {
        transform.position += -transform.right * _speed * Time.deltaTime;
    }

    private IEnumerator DestroyAfterLife()
    {
        float currenLifeTime = 0;
        while(currenLifeTime < _lifeTime)
        {
            currenLifeTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
