namespace Foster
{
    public class Stages
    {
        /// <summary>
        /// Reference to the active stage
        /// </summary>
        public Stage? activeStage;
        /// <summary>
        /// Reference to the next stage
        /// </summary>
        public Stage? nextStage;

        /// <summary>
        /// Called every fixed step
        /// </summary>
        public void FixedUpdate()
        {
            UpdateActiveStage();
            activeStage?.FixedUpdate();
        }

        /// <summary>
        /// Called every variable step
        /// </summary>
        public void Update()
        {
            UpdateActiveStage();
            activeStage?.Update();
        }

        private void UpdateActiveStage()
        {
            if (activeStage == nextStage) return;

            if (activeStage is not null)
            {
                activeStage.End();
            }

            activeStage = nextStage;
            if (activeStage is not null)
            {
                activeStage.stages = this;
                activeStage.Start();
            }
        }

        public void SetNextStage(Stage stage)
        {
            nextStage = stage;
        }
    }
}
