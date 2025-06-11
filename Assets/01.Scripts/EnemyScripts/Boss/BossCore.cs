using UnityEngine;

public class BossCore : BossPart
{
    [HideInInspector] public bool vulnerable = false;

    public override void TakeDamage(float amount)
    {
        if (!vulnerable)
        {
            Debug.Log("몸통은 아직 취약하지 않습니다.");
            return;
        }
        base.TakeDamage(amount);
    }
}