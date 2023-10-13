using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, ISlashable
{
    public MonsterID monsterID = MonsterID.None; // 몬스터 ID

    public Queue<Pattern> queue = new Queue<Pattern>(); // 패턴 큐

    public int damage = 1; // 공격력
    public int score = 1; // 점수

    public event Action actionOnDeath;
    public event Action actionOnSlash; //추가 기능 구현 가능

    public Pattern GetPattern()
    {
        throw new System.NotImplementedException();
    }

    public float GetYPos()
    {
        throw new System.NotImplementedException();
    }

    public void OnSlashed(Pattern c)
    {
        throw new System.NotImplementedException();
    }
}
