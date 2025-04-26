namespace Features.Conveyor
{
    public class TomatoFirtsCrossView : CrossShapeConveyorView
    {
        protected override void SetOpenView()
        {
            base.SetOpenView();
            spriteRenderer.flipY = true;
        }

        protected override void SetCloseView()
        {
            base.SetCloseView();
            spriteRenderer.flipY = false;
        }
    }
}
