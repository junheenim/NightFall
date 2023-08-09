using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // 적 이름
    public string Name { get; set; }
    // 적 최대 HP
    public float MaxHP { get; set; }
    // 적 현재 HP
    public float CurHP { get; set; }
    //공격 중
    public bool isAttack { get; set; }

    public virtual bool Attack(int n)
    {
        return true;
    }

    public virtual void TakeDamage(int n, int attackType)
    {

    }

}
