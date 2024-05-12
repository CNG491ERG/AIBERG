using AIBERG.Core.InputHandlers;
using UnityEngine;

namespace AIBERG.Core
{
    public class InputReplayer : InputHandler
    {
        private GameEnvironment environment;
        private ReplayManager replayManager;
        private Playthrough currentPlaythrough;
        private PlayerAgent playerAgent;
        private BossAgent bossAgent;
        int currentStep;
        void Start()
        {
            environment = Utilities.ComponentFinder.FindComponentInParents<GameEnvironment>(this.transform);
            replayManager = GetComponent<ReplayManager>();
        }


        void FixedUpdate()
        {
            if (replayManager.queueReady)
            {
                Debug.Log("Queue Length: " + replayManager.playthroughs.Count);
                if (currentPlaythrough == null && replayManager.playthroughs.Count != 0)
                {
                    currentPlaythrough = replayManager.playthroughs.Dequeue();
                    currentStep = 0;
                    playerAgent = environment.Player.GetComponent<PlayerAgent>();
                    bossAgent = environment.Boss.GetComponent<BossAgent>();
                    environment.ResetEnvironment();
                    Debug.Log("Current playthrough: " + currentPlaythrough.playthroughID);
                }
                else
                {
                    if (currentPlaythrough != null && currentPlaythrough.stepInputs.TryGetValue(currentStep, out string input))
                    {
                        Debug.Log("Playthrough #" + currentPlaythrough.playthroughID + ", Key: " + currentStep + ", Value: " + input);
                        currentStep++;
                        ApplyInput(input);
                    }
                    else
                    {
                        currentPlaythrough = null;
                        ApplyInput("00000000");
                    }
                }
            }
        }

        private void ApplyInput(string input)
        {
            if (input != null)
            {
                playerAgent.jumpInput = input[0] == '1';
                playerAgent.basicAttackInput = input[1] == '1';
                playerAgent.activeAbility1Input = input[2] == '1';
                playerAgent.activeAbility2Input = input[3] == '1';
                bossAgent.moveUpInput = input[4] == '1';
                bossAgent.moveDownInput = input[5] == '1';
                bossAgent.basicAttackInput = input[6] == '1';
                bossAgent.attackDroneInput = input[7] == '1';
            }

        }

    }

}
