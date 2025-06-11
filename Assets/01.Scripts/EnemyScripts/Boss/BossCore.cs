using UnityEngine;

public class BossCore : BossPart
{
    [HideInInspector] public bool vulnerable = false;

    public override void TakeDamage(float amount)
    {
        if (!vulnerable)
        {
            Debug.Log("������ ���� ������� �ʽ��ϴ�.");
            return;
        }
        base.TakeDamage(amount);
    }
}