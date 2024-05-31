using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float maxHealth = 150f;
    private float minHealth = 20f;
    private float currentHealth;
    private float initialJumpForce = 1f; // ��������� ���� ������
    private PlayerMovementMus playerControl;
    [SerializeField] private TextMeshProUGUI HealthPoints; // ���������� TextMeshProUGUI ������ GameObject
    private Vector3 originalScale; // ������������ ������� ������
    private CharacterController characterController; // ������ �� CharacterController
    [SerializeField] GameObject Destroyobject100;
    [SerializeField] GameObject Destroyobject50;
    private void Start()
    {
        playerControl = GetComponent<PlayerMovementMus>();
        currentHealth = maxHealth; // ������������� ��������� �������� �� ��������
        originalScale = transform.localScale; // ��������� ������������ �������

        // �������� ������ �� CharacterController
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController not found on the player object.");
        }

        // ������������� ��������� ���� ������
        playerControl.jumpForce = initialJumpForce;

        UpdatePlayerStats();
    }

    public void ChangeHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, minHealth, maxHealth); // �������� ��������
        UpdatePlayerStats();

        // ���������, ���� �������� ���� ������������� ��������
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
        float newJumpForce = Mathf.Clamp(playerControl.jumpForce + amount, initialJumpForce, 15f); // �������� ���� ������
        playerControl.jumpForce = newJumpForce;
        UpdatePlayerStats();
    }

    private void UpdatePlayerStats()
    {
        float healthRatio = currentHealth / maxHealth;

        // ��������� ����� ��������
        if (HealthPoints != null)
        {
            HealthPoints.text = "HP: " + currentHealth;
        }

        // ��������� ������� ������
        float scaleFactor = 1f;
        if (healthRatio < 1f)
        {
            scaleFactor = Mathf.Lerp(1f, 1.5f, 1f - healthRatio); // ����������� �������, ���� �������� < 100%
        }
        transform.localScale = new Vector3(originalScale.x * scaleFactor, originalScale.y, originalScale.z * scaleFactor);

        // �������� ������ CharacterController � ����������� �� ��������, ���� �������� <= 100
        if (currentHealth <= 100)
        {
            float radiusHealthRatio = currentHealth / 100f;
            float newRadius = Mathf.Lerp(0.5f, 1f, 1f - radiusHealthRatio); // �������� �������� ��������
            characterController.radius = newRadius;
        }
    }

    private void DestroyPlayer()
    {
        // ������ ��� ����������� ������, ���� ��� �������� ������ ���� 50
        Destroy(gameObject);
    }
}
