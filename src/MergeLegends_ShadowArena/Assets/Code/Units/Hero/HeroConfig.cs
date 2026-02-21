using System;
using UnityEngine;

[Serializable]
public class HeroConfig
{
    [Range(1,100)]
    public int Hp;
    
    [Range(1,30)]
    public float Damage;

    [Range(0,10)]
    public float MoveSpeed;

    public GameObject HeroPrefab;
}
