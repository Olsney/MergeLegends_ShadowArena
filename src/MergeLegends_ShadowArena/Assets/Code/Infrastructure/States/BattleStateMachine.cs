using System;
using UnityEngine;

namespace Code.Infrastructure.States
{
    public class BattleStateMachine : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private GameObject _playZone;

        public BattleStateMachine(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        public event Action FightStarted;

        public void Enter() 
            => EnterPlanning();

        public void Exit()
        {
        }

        public void OnFightStarted() 
            => FightStarted?.Invoke();
        
        public void InitPlayZone(GameObject playZone) 
            => _playZone = playZone;

        public void RemovePlayZone() 
            => GameObject.Destroy(_playZone);

        public void EnterFight() 
            => _gameStateMachine.Enter<BattleFightState>();
        
        private void EnterPlanning() 
            => _gameStateMachine.Enter<BattlePlanningState>();
    }
}