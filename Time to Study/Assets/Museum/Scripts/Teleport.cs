using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] GameObject player; // ������ �� ������
    [SerializeField] Camera playerCamera; // ������ �� ������� ������ ������
    [SerializeField] Camera teleportCamera; // ������ �� ����� ������
    [SerializeField] GameObject shootquest; // ������ �� ����� ������
    public bool teleportTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            // ��������� ������ � ��� ������
            player.SetActive(false);
            playerCamera.gameObject.SetActive(false);
            teleportTrigger = true;
            // �������� ����� ������
            teleportCamera.gameObject.SetActive(true);
            shootquest.SetActive(true);
        }
    }

    public bool IsTeleportTriggered()
    {
        return teleportTrigger;
    }
}
