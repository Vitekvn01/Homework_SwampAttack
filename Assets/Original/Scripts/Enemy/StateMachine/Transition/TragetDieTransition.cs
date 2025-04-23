public class TragetDieTransition : Transition
{
    private void Update()
    {
        if (Target == null)
        {
            NeedTranzit = true;  
        }
    }
}
