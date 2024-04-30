using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Fusion;
using UnityEngine.AI;


public enum MonsterFSM { None, Move, Targeting, PullOut }

public partial class MonsterAI : NetworkBehaviour  //Data Field
{
    private float pullOutDistance = 10;

    [Networked, OnChangedRender(nameof(FSMChanged))]
    public MonsterFSM FSM { get; set; } = MonsterFSM.None;
    private void FSMChanged()
    {
        switch (FSM)
        {
            case MonsterFSM.Move: MoveInitialize(); break;
            case MonsterFSM.Targeting: TargetingInitialize(); break;
            case MonsterFSM.PullOut: PullOutInitialize(); break;
        }
    }

    
    public List<Character> senseMonsters { get; private set; } = new List<Character>(); //네트워크 동기화
    public List<Character> sensePlayers { get; private set; } = new List<Character>();  //네트워크 동기화
    public Character CurrentTarget { get; private set; } = null;    //네트워크 동기화
    [Networked] public int CurrentPathIndex { get; private set; } = -1;  //네트워크 동기화

    [SerializeField] private Transform[] MovePath;
    [SerializeField] private NavMeshAgent navMesh;
 }

public partial class MonsterAI : NetworkBehaviour  //Master Function Field
{
    public void Initialize()
    {
        //방장만
        if (HasStateAuthority == false)
            return;

        FSM = MonsterFSM.Move;
        senseMonsters.Clear();
        sensePlayers.Clear();
        CurrentTarget = null;
    }

    public void SenseCharacterAdd(Character _addCharacter)
    {
        //방장만
        if (HasStateAuthority == false || _addCharacter.isDeath)
            return;

        if (_addCharacter.Kind == Kind.Player && sensePlayers.Contains(_addCharacter) == false)
        {
            sensePlayers.Add(_addCharacter);
            RPC_SenseCharacterAdd(_addCharacter);
            TargetRefresh();
        }

        if (_addCharacter.Kind == Kind.Moster && senseMonsters.Contains(_addCharacter) == false)
        {
            sensePlayers.Add(_addCharacter);
            RPC_SenseCharacterAdd(_addCharacter);
            TargetRefresh();
        }
    }

    public void SenseCharacterRemove(Character _removeCharacter)
    {
        //방장만
        if (HasStateAuthority == false)
            return;

        if (_removeCharacter.Kind == Kind.Player && sensePlayers.Contains(_removeCharacter))
        {
            sensePlayers.Remove(_removeCharacter);
            SenseCharacterRemove(_removeCharacter);
            TargetRefresh();
        }

        if (_removeCharacter.Kind == Kind.Moster && senseMonsters.Contains(_removeCharacter))
        {
            sensePlayers.Remove(_removeCharacter);
            SenseCharacterRemove(_removeCharacter);
            TargetRefresh();
        }
    }

    private void TargetRefresh()
    {
        //방장만
        if (HasStateAuthority == false)
            return;

        bool _isTargeting = false;

        if (FSM == MonsterFSM.Move)
        {
            if (senseMonsters.Count > 0)
            {
                CurrentTarget = GetMinDistanceTarget(senseMonsters);
                RPC_SetCurrentTarget(CurrentTarget);
                FSM = MonsterFSM.Targeting;
                _isTargeting = true;
            }
            else if(sensePlayers.Count > 0)
            {
                CurrentTarget = GetMinDistanceTarget(sensePlayers);
                RPC_SetCurrentTarget(CurrentTarget);
                FSM = MonsterFSM.Targeting;
                _isTargeting = true;
            }
        }

        if (_isTargeting == false)
            FSM = MonsterFSM.Move;
    }

    public void Damage(Character _character)
    {
        if (HasStateAuthority == false)
            return;
        //대미지를 입었을때 여기서도 실행시켜주어야함

        if (FSM == MonsterFSM.PullOut)
        {
            //타겟을 _character로 옮긴후 FSM 를 타겟으로 변경해준다
            CurrentTarget = _character;
            RPC_SetCurrentTarget(CurrentTarget);
            FSM = MonsterFSM.Targeting;
        }
    }

    private void MoveUpdate()
    {
        //방장만
        if (HasStateAuthority == false)
            return;

        //딱히 없음
    }
    private void TargetingUpdate()
    {
        //방장만
        if (HasStateAuthority == false)
            return;

        //타겟이 나의 공격 범위 안에 들어왔을 경우 멈춘뒤 공격
        //나의 공격 범위보다 멀어졌을 경우 다시 뒤쫓아감


        //타겟이 Die 로 바뀌었을 경우 다음 타겟 서치
        if (CurrentTarget.isDeath)
            SenseCharacterRemove(CurrentTarget);

        //마지막 Path 로 부터 일정 거리가 멀어졌을 경우 PullOut으로 변경
        float _distanceToPath = (MovePath[CurrentPathIndex].position - transform.position).sqrMagnitude;
        if (_distanceToPath < Mathf.Pow(pullOutDistance, 2))
            FSM = MonsterFSM.PullOut;
    }
    private void PullOutUpdate()
    {
        //방장만
        if (HasStateAuthority == false)
            return;
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_SenseCharacterAdd(NetworkBehaviour _addCharacter)
    {
        Character _character = _addCharacter.GetBehaviour<Character>();

        switch (_character.Kind)
        {
            case Kind.Moster: senseMonsters.Add(_character); break;
            case Kind.Player: sensePlayers.Add(_character); break;
        }
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_SenseCharacterRemove(NetworkBehaviour _removeCharacter)
    {
        Character _character = _removeCharacter.GetBehaviour<Character>();


        switch (_character.Kind)
        {
            case Kind.Moster: senseMonsters.Remove(_character); break;
            case Kind.Player: sensePlayers.Remove(_character); break;
        }
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_SetCurrentTarget(NetworkBehaviour _target)
    {
        CurrentTarget = _target.GetBehaviour<Character>();
    }
}

public partial class MonsterAI : NetworkBehaviour  //Network Function Field
{
    private void MoveInitialize()
    {
        //현재 인덱스 보다 높은 인덱스 중에 제일 가까운 인덱스의 패스를 타겟으로 삼고 이동한다
        CurrentPathIndex = GetMinDistancePath();
        navMesh.SetDestination(MovePath[CurrentPathIndex].position);
        navMesh.isStopped = false;
    }

    private void TargetingInitialize()
    {
        //타겟팅된 대상에게 이동
        navMesh.SetDestination(CurrentTarget.transform.position);
        navMesh.isStopped = false;
    }

    private void PullOutInitialize()
    {
        navMesh.SetDestination(MovePath[CurrentPathIndex].position);
        navMesh.isStopped = false;
    }
}

public partial class MonsterAI : NetworkBehaviour  //Property Function Field
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

    private int GetMinDistancePath()
    {
        if (CurrentPathIndex + 1 >= MovePath.Length)
            return MovePath.Length - 1;

        int _closestIndex = -1;
        float _closestDistanceSqr = Mathf.Infinity;

        for (int index = CurrentPathIndex + 1; index < MovePath.Length; index++)
        {
            float distanceSqr = (MovePath[index].position - transform.position).sqrMagnitude;

            if (distanceSqr < _closestDistanceSqr)
            {
                _closestIndex = index;
                _closestDistanceSqr = distanceSqr;
            }
        }

        return _closestIndex;
    }
}


public partial class MonsterAI : NetworkBehaviour  //Unity Function Field
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