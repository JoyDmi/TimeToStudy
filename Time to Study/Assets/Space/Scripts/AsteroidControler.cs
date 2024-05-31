using UnityEngine;
using UnityEngine.UI;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private float tumble; // Скорость вращения астероида
    private GameObject spawnPoint; // Точка спауна астероида
    private Image trackingMarker; // Маркер отслеживания астероида
    private Camera mainCamera; // Основная камера

    // Флаг, указывающий, уничтожен ли астероид
    public bool IsDestroyed { get; set; } = false;

    // Уничтожение маркера отслеживания
    public void DestroyTrackingMarker()
    {
        if (trackingMarker != null)
        {
            Destroy(trackingMarker.gameObject);
        }
    }

    // Установка точки спауна астероида
    public void SetSpawnPoint(GameObject spawnPoint)
    {
        this.spawnPoint = spawnPoint;
    }

    // Установка маркера отслеживания
    public void SetTrackingMarker(Image marker)
    {
        trackingMarker = marker;
    }

    // Получение маркера отслеживания
    public Image GetTrackingMarker()
    {
        return trackingMarker;
    }

    // Обработчик столкновения астероида с пулей
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !IsDestroyed)
        {
            IsDestroyed = true;
            DestroyTrackingMarker();
            FindObjectOfType<AsteroidEvent>().AsteroidDestroyed();
            Destroy(gameObject);
        }
    }

    // Инициализация
    private void Start()
    {
        // Установка случайной позиции спауна
        Vector3 spawnPosition = spawnPoint.transform.position + Random.insideUnitSphere * 100f;
        transform.position = spawnPosition;

        // Назначение случайной скорости вращения
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumble;

        mainCamera = Camera.main;
    }

    // Обновление
    private void Update()
    {
        // Если астероид виден на экране, обновляем позицию маркера отслеживания
        if (IsObjectVisible())
        {
            UpdateTrackingMarkerPosition();
        }
    }

    // Проверка видимости астероида на экране
    private bool IsObjectVisible()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        return GeometryUtility.TestPlanesAABB(planes, GetComponent<Collider>().bounds);
    }

    // Обновление позиции маркера отслеживания
    private void UpdateTrackingMarkerPosition()
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position);
        screenPosition.x = Mathf.Clamp(screenPosition.x, 0, Screen.width);
        screenPosition.y = Mathf.Clamp(screenPosition.y, 0, Screen.height);
        trackingMarker.transform.position = screenPosition;
    }
}
