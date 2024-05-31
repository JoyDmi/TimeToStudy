using UnityEngine;

public class BulletEvent : MonoBehaviour
{
    public GameObject explosionPrefab; // Префаб взрыва
    public float life = 10f; // Время жизни пули

    private void Awake()
    {
        Destroy(gameObject, life); // Уничтожение пули по истечении времени жизни
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; // Непрерывное обнаружение столкновений
        rb.interpolation = RigidbodyInterpolation.Interpolate; // Интерполяция для плавного движения
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Проверка столкновения с врагом
        {
            AsteroidController asteroid = collision.gameObject.GetComponent<AsteroidController>();
            if (asteroid != null && !asteroid.IsDestroyed)
            {
                asteroid.IsDestroyed = true; // Установка флага уничтожения для врага
                asteroid.DestroyTrackingMarker(); // Удаление маркера отслеживания
                FindObjectOfType<AsteroidEvent>().AsteroidDestroyed(); // Вызов метода, сообщающего об уничтожении врага
            }
            Instantiate(explosionPrefab, collision.transform.position, Quaternion.identity); // Создание взрыва
            Destroy(collision.gameObject); // Уничтожение врага
            Destroy(gameObject); // Уничтожение пули
        }
        else
        {
            Destroy(gameObject); // Уничтожение пули при столкновении с другим объектом
        }
    }
}
