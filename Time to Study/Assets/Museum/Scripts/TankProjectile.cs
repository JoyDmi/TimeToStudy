using UnityEngine;
using UnityEngine.ProBuilder;

public class ProjectileMotion : MonoBehaviour
{
    public float initialSpeed = 10f; // начальная скорость v0
    public float angle = 45f; // угол a
    public float mass = 1f; // масса m
    public float airResistanceCoefficient = 0.05f; // коэффициент сопротивления воздуха k
    public float gravity = 9.81f; // ускорение свободного падения g

    private Vector3 velocity; // скорость
    private Vector3 acceleration; // ускорение

    void Start()
    {
        Debug.Log("Time of Flight: " + CalculateTimeOfFlight());
        Debug.Log("Horizontal Range: " + CalculateHorizontalRange());
        Debug.Log("Angle for Maximum Range: " + CalculateAngleForMaxRange());
        Debug.Log("Is Model Adequate: " + IsModelAdequate());
        float angleInRadians = angle * Mathf.Deg2Rad; // перевод угла из градусов в радианы
        // вычисление начальной скорости по компонентам
        velocity = new Vector3(initialSpeed * Mathf.Cos(angleInRadians), 
            initialSpeed * Mathf.Sin(angleInRadians), 0); // x(t) = v0 cos(a)
    }

    void FixedUpdate()
    {
        RungeKutta4(); // метод Рунге-Кутты для численного интегрирования
        transform.position += velocity * Time.fixedDeltaTime; // обновление положения снаряда
    }

    void RungeKutta4()
    {
        // метод Рунге-Кутты для численного решения дифференциальных уравнений
        Vector3 k1 = GetAcceleration(velocity) * Time.fixedDeltaTime;
        Vector3 k2 = GetAcceleration(velocity + k1 / 2) * Time.fixedDeltaTime;
        Vector3 k3 = GetAcceleration(velocity + k2 / 2) * Time.fixedDeltaTime;
        Vector3 k4 = GetAcceleration(velocity + k3) * Time.fixedDeltaTime;

        velocity += (k1 + 2 * k2 + 2 * k3 + k4) / 6; // обновление скорости
    }

    Vector3 GetAcceleration(Vector3 velocity)
    {
        float speed = velocity.magnitude; // скорость х^2+у^2+z^2, где z=0

        // вычисление угла φ: tanφ = ky/mx
        float phi = Mathf.Atan2(airResistanceCoefficient * velocity.y, mass * velocity.x);

        // вычисление силы сопротивления воздуха с учетом угла φ: F = k sqrt(x^2+y^2)
        Vector3 drag = airResistanceCoefficient * speed * velocity / mass * Mathf.Cos(phi);

        // вычисление силы тяжести: my = -mg
        Vector3 gravityForce = new Vector3(0, -gravity * mass, 0);

        // вычисление ускорения с учетом силы тяжести и силы сопротивления воздуха: mx = -mg - F cosφ
        return (gravityForce - drag) / mass;
    }

    // Метод для вычисления времени полета
    float CalculateTimeOfFlight()
    {
        return (2 * initialSpeed * Mathf.Sin(angle * Mathf.Deg2Rad)) / gravity;
    }

    // Метод для вычисления дальности полета
    float CalculateHorizontalRange()
    {
        return initialSpeed * initialSpeed * Mathf.Sin(2 * angle * Mathf.Deg2Rad) / gravity;
    }

    // Метод для вычисления угла для максимальной дальности полета
    float CalculateAngleForMaxRange()
    {
        // Подставляем выражение для угла α, при котором достигается максимальная дальность полета,
        // из аналитического решения
        return Mathf.Atan(1 / airResistanceCoefficient) * Mathf.Rad2Deg;
    }

    // Метод для анализа адекватности модели
    bool IsModelAdequate()
    {
        // Проверка, является ли модель адекватной для реальных условий
        return CalculateAngleForMaxRange() > 45f;
    }
}