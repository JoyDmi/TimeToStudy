using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float maxHealth = 150f;
    private float minHealth = 20f;
    private float currentHealth;
    private float initialJumpForce = 1f; // Начальная сила прыжка
    private PlayerMovementMus playerControl;
    [SerializeField] private TextMeshProUGUI HealthPoints; // Используем TextMeshProUGUI вместо GameObject
    private Vector3 originalScale; // Оригинальный масштаб игрока
    private CharacterController characterController; // Ссылка на CharacterController
    [SerializeField] GameObject Destroyobject100;
    [SerializeField] GameObject Destroyobject50;
    private void Start()
    {
        playerControl = GetComponent<PlayerMovementMus>();
        currentHealth = maxHealth; // устанавливаем начальное здоровье на максимум
        originalScale = transform.localScale; // Сохраняем оригинальный масштаб

        // Получаем ссылку на CharacterController
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController not found on the player object.");
        }

        // Устанавливаем начальную силу прыжка
        playerControl.jumpForce = initialJumpForce;

        UpdatePlayerStats();
    }

    public void ChangeHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, minHealth, maxHealth); // изменяем здоровье
        UpdatePlayerStats();

        // Проверяем, если здоровье ниже определенного значения
        if (currentHealth <= 100f)
        {
            Destroy(Destroyobject100);
        }
        if (currentHealth <= 70f)
        {
            Destroy(Destroyobject50);
        }
        if (currentHealth <= 0f)
        {
            DestroyPlayer();
        }
    }

    public void ChangeJumpForce(float amount)
    {
        float newJumpForce = Mathf.Clamp(playerControl.jumpForce + amount, initialJumpForce, 15f); // изменяем силу прыжка
        playerControl.jumpForce = newJumpForce;
        UpdatePlayerStats();
    }

    private void UpdatePlayerStats()
    {
        float healthRatio = currentHealth / maxHealth;

        // Обновляем текст здоровья
        if (HealthPoints != null)
        {
            HealthPoints.text = "HP: " + currentHealth;
        }

        // Обновляем масштаб игрока
        float scaleFactor = 1f;
        if (healthRatio < 1f)
        {
            scaleFactor = Mathf.Lerp(1f, 1.5f, 1f - healthRatio); // Увеличиваем масштаб, если здоровье < 100%
        }
        transform.localScale = new Vector3(originalScale.x * scaleFactor, originalScale.y, originalScale.z * scaleFactor);

        // Изменяем радиус CharacterController в зависимости от здоровья, если здоровье <= 100
        if (currentHealth <= 100)
        {
            float radiusHealthRatio = currentHealth / 100f;
            float newRadius = Mathf.Lerp(0.5f, 1f, 1f - radiusHealthRatio); // Изменяем диапазон наоборот
            characterController.radius = newRadius;
        }
    }

    private void DestroyPlayer()
    {
        // Логика для уничтожения игрока, если его здоровье падает ниже 50
        Destroy(gameObject);
    }
}
