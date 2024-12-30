using ActiveRagdoll;
using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        if (Runner.LocalPlayer == player)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5)); 
            Quaternion spawnRotation = Quaternion.identity;

            NetworkObject character = Runner.Spawn(PlayerPrefab, spawnPosition, spawnRotation, player);

            
            var networkCharacter = character.GetComponent<NetworkCharacter>();
            if (networkCharacter != null)
            {
                networkCharacter.RPC_SynchronizeState();
            }

            var cameraModule = character.GetComponent<CameraModule>();
            if (cameraModule != null)
            {
                cameraModule.enabled = true; 
            }
        }
    }

}