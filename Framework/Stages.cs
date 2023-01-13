using System.Collections.Generic;
using Foster.Framework;

namespace Foster
{
    public class Stages : Module
    {
        /// <summary>
        /// Reference to the stages
        /// </summary>
        public Stack<Stage> stages = new();
        /// <summary>
        /// Reference to the active stage
        /// </summary>
        public Stage? activeStage;

        /// <summary>
        /// Called every fixed step
        /// </summary>
        protected internal override void FixedUpdate()
        {
            UpdateActiveStage();
            activeStage?.FixedUpdate();
        }

        /// <summary>
        /// Called every variable step
        /// </summary>
        protected internal override void Update()
        {
            UpdateActiveStage();
            activeStage?.Update();
        }

        private void UpdateActiveStage()
        {
            Stage? nextStage = null;
            if (stages.Count != 0)
            {
                nextStage = stages.Peek();
                if (activeStage != nextStage)
                {
                    if (activeStage != null)
                    {
                        activeStage.End();
                    }
                    if (nextStage != null)
                    {
                        nextStage.Start();
                    }
                }
            }
            activeStage = nextStage;
        }

        public void PushStage(Stage stage)
        {
            stages.Push(stage);
        }

        public void PopStage()
        {
            stages.Pop();
        }

        public Stage? GetActiveStage()
        {
            return activeStage;
        }
    }
}
