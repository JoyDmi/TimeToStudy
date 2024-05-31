using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    private float G = 0.01f; // �������������� ����������
    [SerializeField] GameObject[] Spaceobjects; // ������ ��� �������� ���� �������� ���
    [SerializeField] bool IsElipticalOrbit = false; // ����������, ������������, �������� �� ������� �� ������������� ������
    private Dictionary<GameObject, Vector3> totalForces = new Dictionary<GameObject, Vector3>(); // ������� ��� �������� ��������� ���, ����������� �� ������ �������� ����

    void Start()
    {
        SetInitialVelocity(); // ����� ������ ��� ��������� ��������� �������� ������� ��������� ����
    }

    void FixedUpdate()
    {
        Gravity(); // ���������� ���� ����������
        foreach (GameObject a in Spaceobjects) // ����� ���� �������� ���
        {
            // ���������� ������� �� ������ ������� ��������
            a.transform.position += a.GetComponent<Rigidbody>().velocity * Time.deltaTime;

            // ������ ��������� �� ������ ��������� ���� � �����
            Vector3 acceleration = totalForces[a] / a.GetComponent<Rigidbody>().mass;

            // ���������� �������� �� ������ ���������
            a.GetComponent<Rigidbody>().velocity += acceleration * Time.deltaTime;

            // �������� ��������� ���� ������ ����� ���
            a.transform.Rotate(Vector3.up, 50f * Time.deltaTime);
        }
    }

    void SetInitialVelocity()
    {
        foreach (GameObject a in Spaceobjects) // ����� ���� �������� ��� ��� ��������� ��������� ��������
        {
            foreach (GameObject b in Spaceobjects) // ����� ���� �������� ��� ��� ������� ��������� �������� ������������ ������ ���
            {
                if (!a.Equals(b)) // ���������� ������ ���� �� �������
                {
                    float m2 = b.GetComponent<Rigidbody>().mass; // ����� ������� ��������� ����
                    float r = Vector3.Distance(a.transform.position, b.transform.position); // ���������� ����� ������
                    a.transform.LookAt(b.transform); // ��������� ����������� ������� �� ������ ����

                    // ��������� ��������� �������� � ����������� �� ���� ������ (������������� ��� ��������)
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
        foreach (GameObject a in Spaceobjects) // ����� ���� �������� ��� ��� ������� ���� ����������
        {
            Vector3 totalForce = Vector3.zero; // ������������� ��������� ���� ��� �������� ����
            foreach (GameObject b in Spaceobjects) // ����� ���� �������� ��� ��� ������� ���� ����������
            {
                if (!a.Equals(b)) // ���������� ������ ���� �� �������
                {
                    float m1 = a.GetComponent<Rigidbody>().mass; // ����� ������� ��������� ����
                    float m2 = b.GetComponent<Rigidbody>().mass; // ����� ������� ��������� ����
                    float r = Vector3.Distance(a.transform.position, b.transform.position); // ���������� ����� ������

                    // ������ ���� ���������� � ���������� �� � ��������� ����
                    totalForce += (b.transform.position - a.transform.position).normalized * (G * (m1 * m2) / (r * r));
                }
            }
            totalForces[a] = totalForce; // ���������� ��������� ���� ��� �������� ����

            // ���������� ���� � ����
            a.GetComponent<Rigidbody>().AddForce(totalForce);
        }
    }
}
