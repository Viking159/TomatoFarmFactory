namespace Features.ConveyorDataCondition
{
    using Features.SaveSystem;
    using Features.Scenes;

    /// <summary>
    /// Conveyor data load scene condition
    /// </summary>
    public class ConveyorDataCondition : AbstractLoadSceneCondition
    {
        public override void InitCondition()
        {
            isValidCondition = false;
            ConveyorDataFileController.Instance.onDataInited += OnDataInited;
            ConveyorDataFileController.Instance.LoadData();
        }

        protected virtual void OnDataInited() => isValidCondition = true;

        protected virtual void OnDisable() => ConveyorDataFileController.Instance.onDataInited -= OnDataInited;
    }
}