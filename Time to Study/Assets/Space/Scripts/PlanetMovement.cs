using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    private float G = 0.01f; // Гравитационная постоянная
    [SerializeField] GameObject[] Spaceobjects; // Массив для хранения всех небесных тел
    [SerializeField] bool IsElipticalOrbit = false; // Переменная, определяющая, движется ли планета по эллиптической орбите
    private Dictionary<GameObject, Vector3> totalForces = new Dictionary<GameObject, Vector3>(); // Словарь для хранения суммарных сил, действующих на каждое небесное тело

    void Start()
    {
        SetInitialVelocity(); // Вызов метода для установки начальной скорости каждого небесного тела
    }

    void FixedUpdate()
    {
        Gravity(); // Обновление силы гравитации
        foreach (GameObject a in Spaceobjects) // Обход всех небесных тел
        {
            // Обновление позиции на основе текущей скорости
            a.transform.position += a.GetComponent<Rigidbody>().velocity * Time.deltaTime;

            // Расчет ускорения на основе суммарной силы и массы
            Vector3 acceleration = totalForces[a] / a.GetComponent<Rigidbody>().mass;

            // Обновление скорости на основе ускорения
            a.GetComponent<Rigidbody>().velocity += acceleration * Time.deltaTime;

            // Вращение небесного тела вокруг своей оси
            a.transform.Rotate(Vector3.up, 50f * Time.deltaTime);
        }
    }

    void SetInitialVelocity()
    {
        foreach (GameObject a in Spaceobjects) // Обход всех небесных тел для установки начальной скорости
        {
            foreach (GameObject b in Spaceobjects) // Обход всех небесных тел для расчета начальной скорости относительно других тел
            {
                if (!a.Equals(b)) // Исключение самого себя из расчета
                {
                    float m2 = b.GetComponent<Rigidbody>().mass; // Масса второго небесного тела
                    float r = Vector3.Distance(a.transform.position, b.transform.position); // Расстояние между телами
                    a.transform.LookAt(b.transform); // Установка направления взгляда на второе тело

                    // Установка начальной скорости в зависимости от типа орбиты (эллиптическая или круговая)
                    if (IsElipticalOrbit)
                    {
                        a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((G * m2) * ((2 / r) - (1 / (r * 1.5f))));
                    }
                    else
                    {
                        a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((G * m2) / r);
                    }
                }
            }
        }
    }

    void Gravity()
    {
        foreach (GameObject a in Spaceobjects) // Обход всех небесных тел для расчета силы гравитации
        {
            Vector3 totalForce = Vector3.zero; // Инициализация суммарной силы для текущего тела
            foreach (GameObject b in Spaceobjects) // Обход всех небесных тел для расчета силы гравитации
            {
                if (!a.Equals(b)) // Исключение самого себя из расчета
                {
                    float m1 = a.GetComponent<Rigidbody>().mass; // Масса первого небесного тела
                    float m2 = b.GetComponent<Rigidbody>().mass; // Масса второго небесного тела
                    float r = Vector3.Distance(a.transform.position, b.transform.position); // Расстояние между телами

                    // Расчет силы гравитации и добавление ее к суммарной силе
                    totalForce += (b.transform.position - a.transform.position).normalized * (G * (m1 * m2) / (r * r));
                }
            }
            totalForces[a] = totalForce; // Сохранение суммарной силы для текущего тела

            // Применение силы к телу
            a.GetComponent<Rigidbody>().AddForce(totalForce);
        }
    }
}
