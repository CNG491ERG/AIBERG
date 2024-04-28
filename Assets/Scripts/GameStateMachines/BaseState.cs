using UnityEngine;
using AIBERG.Core;


namespace AIBERG.GameStateMachines
{
    public abstract class BaseState
    {
        [SerializeField] protected GameEnvironment environment = GameObject.Find("Environment").GetComponent<GameEnvironment>();
        protected static int parkourCounter = 0;
        protected static int bossCounter = 0;
        protected float stateStartTime;

        public int GetParkourCounter()
        {
            return parkourCounter;
        }
        public int GetBossCounter()
        {
            return bossCounter;
        }
        public abstract void EnterState(GameStateMachineScript stateMachine);
        public abstract void UpdateState(GameStateMachineScript stateMachine);
    }
}
