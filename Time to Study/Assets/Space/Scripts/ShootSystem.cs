using UnityEngine;

public class ShootSystem : MonoBehaviour
{
    public Transform bulletSpawnPoint; // ����� ������ ����
    public GameObject bulletPrefab; // ������ ����
    public AudioSource laserSound; // ���� ��������
    public float bulletSpeed = 100f; // �������� ����

    private float fireRate = 0.2f; // ������� ���������
    private float nextFireTime = 0f; // ����� �� ���������� ��������

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            Vector3 shootDirection = Camera.main.transform.forward; // ����������� �������� ������ �� ������

            Quaternion targetRotation = Quaternion.LookRotation(shootDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 5f); // ������������ ������ �� ����������� ��������

            Quaternion spawnRotation = bulletSpawnPoint.rotation; // �������� ����

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, spawnRotation); // �������� ����
            bullet.tag = "Bullet"; // ��������� ���� ����
            laserSound.Play(); // ��������������� ����� ��������
            bullet.GetComponent<Rigidbody>().velocity = shootDirection * bulletSpeed; // ���������� �������� ����
        }
    }
}
