using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Transform startPoint; // Начальная точка движения
    public Transform endPoint; // Конечная точка движения
    public float speed = 3f; // Скорость движения платформы

    private Vector3 currentTarget; // Текущая целевая позиция

    void Start()
    {
        currentTarget = endPoint.position; // Начинаем с движения к конечной точке
    }

    void Update()
    {
        // Перемещаем платформу к текущей целевой позиции
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        // Если платформа достигла целевой позиции, меняем цель на противоположную
        if (Vector3.Distance(transform.position, currentTarget) < 0.01f)
        {
            if (currentTarget == startPoint.position)
                currentTarget = endPoint.position;
            else
                currentTarget = startPoint.position;
        }
    }
}
