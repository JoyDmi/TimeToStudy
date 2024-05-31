using UnityEngine;
using UnityEngine.UI;

public class AsteroidEvent : MonoBehaviour
{
    [SerializeField] GameObject asteroidPrefab; // ������ ���������
    private int asteroidCount = 0; // ���������� ��������� ����������
    private int spawnRound = 1; // ���������� ����������, ����������� �� ���� ����� ������
    [SerializeField] public GameObject Earth; // ������ �����
    [SerializeField] private float spawnRadius = 20f; // ������ ������ ����������
    [SerializeField] private Image trackingMarkerPrefab; // ������ ������� ������������
    [SerializeField] private Camera mainCamera; // �������� ������
    private int destroyedAsteroids = 0; // ���������� ������������ ����������
    private bool isFirstQuestCompleted = false; // ����, �����������, ��������� �� ������ �������
    private bool isSpawningStarted = false; // ����, �����������, ����� �� ������� ������ ����������

    // ������ ������ ���������� ����� ���������� ������� �������
    public void StartSpawningAsteroids()
    {
        if (isFirstQuestCompleted)
        {
            isSpawningStarted = true;
            SpawnAsteroids();
        }
    }

    // ���������� ������� �������
    public void CompleteFirstQuest()
    {
        isFirstQuestCompleted = true;
    }

    // ��������� ���������� ������������ ����������
    public int GetDestroyedAsteroidsCount()
    {
        return destroyedAsteroids;
    }

    // �������� ���������� ������ �����
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

    // ���������� ����������� ���������
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
