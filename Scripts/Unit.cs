using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // �� �̸�
    public string Name { get; set; }
    // �� �ִ� HP
    public float MaxHP { get; set; }
    // �� ���� HP
    public float CurHP { get; set; }
    //���� ��
    public bool isAttack { get; set; }

    public virtual bool Attack(int n)
    {
        return true;
    }

    public virtual void TakeDamage(int n, int attackType)
    {

    }

}
