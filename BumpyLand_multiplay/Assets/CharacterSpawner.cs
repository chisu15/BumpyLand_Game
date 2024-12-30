using Fusion;
using UnityEngine;

public class CharacterSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject _characterPrefab;


    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            Debug.Log("Character Spawner has Authority");
            Runner.Spawn(_characterPrefab, transform.position, Quaternion.identity, Runner.LocalPlayer);
        }
        else
        {
            Debug.Log("Character Spawner does not have authority");
        }
    }
}