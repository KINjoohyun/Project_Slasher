using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, ISlashable
{
    public MonsterID monsterID = MonsterID.None; // ���� ID

    public Queue<Pattern> queue = new Queue<Pattern>(); // ���� ť

    public int damage = 1; // ���ݷ�
    public int score = 1; // ����

    public event Action actionOnDeath;
    public event Action actionOnSlash; //�߰� ��� ���� ����

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
