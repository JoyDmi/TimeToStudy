using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour, InterectableInterface
{
    public enum FoodType { Healthy, Unhealthy }
    public FoodType foodType;
    public string description;
    public float healthChangeAmount; // Количество изменения здоровья
    public float jumpForceChangeAmount; // Количество изменения силы прыжка
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
                playerStats.ChangeHealth(healthChangeAmount); // Увеличиваем здоровье
                playerStats.ChangeJumpForce(jumpForceChangeAmount); // Увеличиваем силу прыжка
            }
            else
            {
                playerStats.ChangeHealth(-healthChangeAmount); // Уменьшаем здоровье
                playerStats.ChangeJumpForce(-jumpForceChangeAmount); // Уменьшаем силу прыжка
            }
            AudioEat.PlayOneShot(eatSound);
            Destroy(ChoiseFood);
            Destroy(gameObject);// уничтожаем объект после взаимодействия
        }
    }

    public string GetDiscription()
    {
        return description;
    }
}
