using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterFSM { None, Move, Targeting, PullOut }

public partial class MonsterAI : MonoBehaviour  //Data Field
{
    private MonsterFSM fsm = MonsterFSM.None;

    public MonsterFSM FSM //네트워크 동기화
    {
        get
        {
            return fsm;
        }
        set
        {
            fsm = value;
            switch (value)
            {
                case MonsterFSM.Move: MoveInitialize(); break;
                case MonsterFSM.Targeting: TargetingInitialize(); break;
                case MonsterFSM.PullOut: PullOutInitialize(); break;
            }
        }
    }

    public List<Character> senseMonsters { get; private set; } = new List<Character>(); //네트워크 동기화
    public List<Character> sensePlayers { get; private set; } = new List<Character>();  //네트워크 동기화
    public Character CurrentTarget { get; private set; }    //네트워크 동기화
    public int CurrentPathIndex { get; private set; } = 0;  //네트워크 동기화

    [SerializeField] private Transform[] MovePath;
}

public partial class MonsterAI : MonoBehaviour  //Master Function Field
{
    public void Initialize()
    {
        //방장만

        FSM = MonsterFSM.Move;
        senseMonsters.Clear();
        sensePlayers.Clear();
        CurrentTarget = null;
    }

    public void SenseCharacterAdd(Character _addCharacter)
    {
        //방장만

        if (_addCharacter.Kind == Kind.Player && sensePlayers.Contains(_addCharacter) == false)
        {
            sensePlayers.Add(_addCharacter);
            TargetRefresh();
        }

        if (_addCharacter.Kind == Kind.Moster && senseMonsters.Contains(_addCharacter) == false)
        {
            sensePlayers.Add(_addCharacter);
            TargetRefresh();
        }
    }

    private void TargetRefresh()
    {
        //방장만

        bool _isTargeting = false;

        if (FSM == MonsterFSM.Move)
        {
            if (senseMonsters.Count > 0)
            {
                CurrentTarget = GetMinDistanceTarget(senseMonsters);
                FSM = MonsterFSM.Targeting;
                _isTargeting = true;
            }
            else if(sensePlayers.Count > 0)
            {
                CurrentTarget = GetMinDistanceTarget(sensePlayers);
                FSM = MonsterFSM.Targeting;
                _isTargeting = true;
            }
        }

        if (_isTargeting == false)
            FSM = MonsterFSM.Move;
    }
  

    private void MoveUpdate()
    {
        //방장만
    }
    private void TargetingUpdate()
    {
        //방장만

        //타겟이 Die 로 바뀌었을 경우 다음 타겟 서치

        //마지막 Path 로 부터 일정 거리가 멀어졌을 경우 PullOut으로 변경
    }
    private void PullOutUpdate()
    {
        //방장만
    }
}

public partial class MonsterAI : MonoBehaviour  //Network Function Field
{
    private void MoveInitialize()
    {
    }

    private void TargetingInitialize()
    {
        //타겟팅된 대상에게 이동
    }

    private void PullOutInitialize()
    {

    }
}

public partial class MonsterAI : MonoBehaviour  //Property Function Field
{
    private Character GetMinDistanceTarget(List<Character> _searchList)
    {
        Character _closestTarget = null;
        float _closestDistanceSqr = Mathf.Infinity;

        foreach (Character _item in _searchList)
        {
            float distanceSqr = (_item.transform.position - transform.position).sqrMagnitude;

            if (distanceSqr < _closestDistanceSqr)
            {
                _closestTarget = _item;
                _closestDistanceSqr = distanceSqr;
            }
        }

        return _closestTarget;
    }
}

public partial class MonsterAI : MonoBehaviour  //Unity Function Field
{

    private void Update()
    {
        switch (FSM)
        {
            case MonsterFSM.Move: MoveUpdate(); break;
            case MonsterFSM.Targeting: TargetingUpdate(); break;
            case MonsterFSM.PullOut: PullOutUpdate(); break;
        }
    }
}