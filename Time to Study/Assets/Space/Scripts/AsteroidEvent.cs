using UnityEngine;
using UnityEngine.UI;

public class AsteroidEvent : MonoBehaviour
{
    [SerializeField] GameObject asteroidPrefab; // Префаб астероида
    private int asteroidCount = 0; // Количество созданных астероидов
    private int spawnRound = 1; // Количество астероидов, создаваемых за один раунд спауна
    [SerializeField] public GameObject Earth; // Объект Земли
    [SerializeField] private float spawnRadius = 20f; // Радиус спауна астероидов
    [SerializeField] private Image trackingMarkerPrefab; // Префаб маркера отслеживания
    [SerializeField] private Camera mainCamera; // Основная камера
    private int destroyedAsteroids = 0; // Количество уничтоженных астероидов
    private bool isFirstQuestCompleted = false; // Флаг, указывающий, завершено ли первое задание
    private bool isSpawningStarted = false; // Флаг, указывающий, начат ли процесс спауна астероидов

    // Начало спауна астероидов после завершения первого задания
    public void StartSpawningAsteroids()
    {
        if (isFirstQuestCompleted)
        {
            isSpawningStarted = true;
            SpawnAsteroids();
        }
    }

    // Завершение первого задания
    public void CompleteFirstQuest()
    {
        isFirstQuestCompleted = true;
    }

    // Получение количества уничтоженных астероидов
    public int GetDestroyedAsteroidsCount()
    {
        return destroyedAsteroids;
    }

    // Создание астероидов вокруг Земли
    private void SpawnAsteroids()
    {
        if (isSpawningStarted && asteroidCount < 10)
        {
            for (int i = 0; i < spawnRound; i++)
            {
                Vector3 spawnPosition = Earth.transform.position + Random.insideUnitSphere * spawnRadius;
                GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
                asteroidCount++;

                AsteroidController asteroidController = asteroid.GetComponent<AsteroidController>();
                asteroidController.SetSpawnPoint(Earth);

                Image trackingMarker = asteroidController.GetTrackingMarker();
                if (trackingMarker == null)
                {
                    trackingMarker = Instantiate(trackingMarkerPrefab);
                    trackingMarker.transform.SetParent(GameObject.Find("Canvas").transform);
                    asteroidController.SetTrackingMarker(trackingMarker);
                }
            }
        }
    }

    // Обработчик уничтожения астероида
    public void AsteroidDestroyed()
    {
        destroyedAsteroids++;
        if (isSpawningStarted)
        {
            spawnRound++;
            SpawnAsteroids();
        }
    }
}
