using UnityEngine;

public class BulletEvent : MonoBehaviour
{
    public GameObject explosionPrefab; // ������ ������
    public float life = 10f; // ����� ����� ����

    private void Awake()
    {
        Destroy(gameObject, life); // ����������� ���� �� ��������� ������� �����
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; // ����������� ����������� ������������
        rb.interpolation = RigidbodyInterpolation.Interpolate; // ������������ ��� �������� ��������
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // �������� ������������ � ������
        {
            AsteroidController asteroid = collision.gameObject.GetComponent<AsteroidController>();
            if (asteroid != null && !asteroid.IsDestroyed)
            {
                asteroid.IsDestroyed = true; // ��������� ����� ����������� ��� �����
                asteroid.DestroyTrackingMarker(); // �������� ������� ������������
                FindObjectOfType<AsteroidEvent>().AsteroidDestroyed(); // ����� ������, ����������� �� ����������� �����
            }
            Instantiate(explosionPrefab, collision.transform.position, Quaternion.identity); // �������� ������
            Destroy(collision.gameObject); // ����������� �����
            Destroy(gameObject); // ����������� ����
        }
        else
        {
            Destroy(gameObject); // ����������� ���� ��� ������������ � ������ ��������
        }
    }
}
