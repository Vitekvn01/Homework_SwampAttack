using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    
    private bool _hasHit = false;

    private void Update()
    {       
        transform.Translate(-transform.right * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_hasHit == false)
        {
            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_damage);  
                Destroy(gameObject);
                _hasHit = true;
            }
        }

        

    }
}
