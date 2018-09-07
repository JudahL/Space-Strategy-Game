public class AttachmentEffectCommand : ICommand
{
    private IDamageHandlerComponent _damageHandler;
    private DamageDetails _damageDetails;

    public AttachmentEffectCommand(IDamageHandlerComponent damageHandler, DamageDetails damageDetails)
    {
        _damageHandler = damageHandler;
        _damageDetails = damageDetails;
    }

    public void Execute()
    {
        _damageHandler.ProcessDamage(_damageDetails);
    }
}
