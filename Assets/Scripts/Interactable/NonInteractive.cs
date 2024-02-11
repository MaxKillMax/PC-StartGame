namespace Interactable
{
    public class NonInteractive: Interactive
    {
        protected override void DoAction()
        {
            print("Non Action");
        }
    }
}