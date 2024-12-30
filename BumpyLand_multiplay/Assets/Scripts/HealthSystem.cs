using Fusion;
using UnityEngine;

public class HealthSystem : NetworkBehaviour
{
    [Networked] public int CurrentHealth { get; private set; }
    [SerializeField] private int maxHealth = 100;

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            CurrentHealth = maxHealth; // Khởi tạo máu
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_TakeDamage(int damage)
    {
        if (!HasStateAuthority) return;

        CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
        Debug.Log($"[HealthSystem] New Health: {CurrentHealth}");

        if (CurrentHealth <= 0)
        {
            HandleDeath();
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_Heal(int amount)
    {
        if (!HasStateAuthority) return;

        CurrentHealth = Mathf.Min(maxHealth, CurrentHealth + amount);
        Debug.Log($"[HealthSystem] Healed. New Health: {CurrentHealth}");
    }

    private void HandleDeath()
    {
        Debug.Log("Character Died!");
        // Logic xử lý cái chết, như respawn hoặc vô hiệu hóa nhân vật
    }
}