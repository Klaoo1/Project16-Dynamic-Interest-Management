using UnityEngine;
using TMPro;
using Mirror;

public class CullingDebugUI : MonoBehaviour
{
    public TMP_Text statusText;

    public float visibilityRange = 30f;

    void Update()
    {
        if (statusText == null)
            return;

        NetworkIdentity localPlayer = NetworkClient.localPlayer;

        if (localPlayer == null)
        {
            statusText.text =
                "DISTANCE CULLING\n" +
                "Waiting for player...";
            return;
        }

        NetworkIdentity[] players =
            FindObjectsByType<NetworkIdentity>(
                FindObjectsSortMode.None
            );

        NetworkIdentity closestPlayer = null;
        float closestDistance = Mathf.Infinity;

        foreach (NetworkIdentity player in players)
        {
            if (player == localPlayer)
                continue;

            float distance = Vector3.Distance(
                localPlayer.transform.position,
                player.transform.position
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player;
            }
        }

        if (closestPlayer == null)
        {
            statusText.text =
                "DISTANCE CULLING\n" +
                "Range: " + visibilityRange + "m\n" +
                "Distance: --\n" +
                "Status: NO REMOTE PLAYER";

            return;
        }

        bool active = closestDistance <= visibilityRange;

        statusText.text =
            "DISTANCE CULLING\n" +
            "Range: " + visibilityRange + "m\n" +
            "Distance: " +
            closestDistance.ToString("F1") + "m\n" +
            "Status: " +
            (active ? "ACTIVE" : "CULLED");
    }
}