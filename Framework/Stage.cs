namespace Foster
{
    public class Stage
    {
        protected internal Stages? stages;
        public virtual void Start()
        {

        }

        public virtual void End()
        {

        }

        public virtual void FixedUpdate()
        {

        }

        public virtual void Update()
        {

        }

        protected virtual void SetNextStage(Stage stage)
        {
            stages?.SetNextStage(stage);
        }
    }
}
