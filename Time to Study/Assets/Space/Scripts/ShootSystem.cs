using UnityEngine;

public class ShootSystem : MonoBehaviour
{
    public Transform bulletSpawnPoint; // Точка спауна пули
    public GameObject bulletPrefab; // Префаб пули
    public AudioSource laserSound; // Звук выстрела
    public float bulletSpeed = 100f; // Скорость пули

    private float fireRate = 0.2f; // Частота выстрелов
    private float nextFireTime = 0f; // Время до следующего выстрела

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            Vector3 shootDirection = Camera.main.transform.forward; // Направление выстрела вперед от камеры

            Quaternion targetRotation = Quaternion.LookRotation(shootDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 5f); // Выравнивание оружия по направлению выстрела

            Quaternion spawnRotation = bulletSpawnPoint.rotation; // Вращение пули

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, spawnRotation); // Создание пули
            bullet.tag = "Bullet"; // Установка тега пули
            laserSound.Play(); // Воспроизведение звука выстрела
            bullet.GetComponent<Rigidbody>().velocity = shootDirection * bulletSpeed; // Назначение скорости пули
        }
    }
}
