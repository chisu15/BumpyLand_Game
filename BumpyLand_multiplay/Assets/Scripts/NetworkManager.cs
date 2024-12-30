using Fusion;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActiveRagdoll;
using Fusion.Protocol;
using Fusion.Sockets;
using UnityEngine.Playables; 

public class NetworkManager : MonoBehaviour, INetworkRunnerCallbacks
{
    public NetworkRunner Runner;
    public GameObject CharacterPrefab;  


     public struct NetworkInputData : INetworkInput
    {
          public Vector2 MoveDirection;
          public bool IsActionPressed;
    }

    void Start()
    {
        Runner = gameObject.AddComponent<NetworkRunner>();
        Runner.ProvideInput = true;

        Runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.AutoHostOrClient,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
            SessionName = "TestSession"
        });
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        
        if (runner.IsServer)
        {
            runner.Spawn(CharacterPrefab, Vector3.zero, Quaternion.identity, player);
        }
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
        throw new NotImplementedException();
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if (input.TryGet<NetworkInputData>(out NetworkInputData data))
        {
            
            foreach (var no in runner.GetAllNetworkObjects())
            {
                if (no.TryGetComponent<CharacterMovement>(out CharacterMovement moveScript))
                {
                    
                    moveScript.Move(data.MoveDirection, data.IsActionPressed);
                }
            }
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, DisconnectReason reason) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        throw new NotImplementedException();
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        throw new NotImplementedException();
    }

    public void OnConnectFailed(NetworkRunner runner, DisconnectReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
        throw new NotImplementedException();
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void StartHost() {
         Runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Host,
            SceneManager = gameObject.GetComponent<NetworkSceneManagerDefault>(),
            SessionName = "TestSession"
        });
    }
    public void StartClient() {
        Runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Client,
            SceneManager = gameObject.GetComponent<NetworkSceneManagerDefault>(),
            SessionName = "TestSession"
        });
    }
}