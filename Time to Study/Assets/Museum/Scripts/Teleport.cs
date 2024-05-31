using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] GameObject player; // —сылка на игрока
    [SerializeField] Camera playerCamera; // —сылка на текущую камеру игрока
    [SerializeField] Camera teleportCamera; // —сылка на новую камеру
    public bool teleportTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            // ќтключаем игрока и его камеру
            player.SetActive(false);
            playerCamera.gameObject.SetActive(false);
            teleportTrigger = true;
            // ¬ключаем новую камеру
            teleportCamera.gameObject.SetActive(true);
        }
    }

    public bool IsTeleportTriggered()
    {
        return teleportTrigger;
    }
}
