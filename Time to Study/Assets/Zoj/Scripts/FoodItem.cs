using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour, InterectableInterface
{
    public enum FoodType { Healthy, Unhealthy }
    public FoodType foodType;
    public string description;
    public float healthChangeAmount; // ���������� ��������� ��������
    public float jumpForceChangeAmount; // ���������� ��������� ���� ������
    [SerializeField] GameObject ChoiseFood;
    private AudioSource AudioEat;
    public AudioClip eatSound;
    public void Interact()
    {
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        if (playerStats != null)
        {
            if (foodType == FoodType.Healthy)
            {
                playerStats.ChangeHealth(healthChangeAmount); // ����������� ��������
                playerStats.ChangeJumpForce(jumpForceChangeAmount); // ����������� ���� ������
            }
            else
            {
                playerStats.ChangeHealth(-healthChangeAmount); // ��������� ��������
                playerStats.ChangeJumpForce(-jumpForceChangeAmount); // ��������� ���� ������
            }
            AudioEat.PlayOneShot(eatSound);
            Destroy(ChoiseFood);
            Destroy(gameObject);// ���������� ������ ����� ��������������
        }
    }

    public string GetDiscription()
    {
        return description;
    }
}
