using AIBERG.Core;

namespace AIBERG.BossMode{
    public abstract class BossModeBaseState{
        public abstract void EnterState(BossModeStateManager stateManager);
        public abstract void UpdateState(BossModeStateManager stateManager);
    }
}
