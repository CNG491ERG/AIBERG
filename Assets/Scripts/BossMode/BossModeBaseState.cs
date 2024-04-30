using AIBERG.Core;

namespace AIBERG.BossMode{
    public abstract class BossModeBaseState{
        protected GameEnvironment gameEnvironment;
        public abstract void EnterState(BossModeStateManager stateManager);
        public abstract void UpdateState(BossModeStateManager stateManager);

    }
}
