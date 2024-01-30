public class PlayerAssembler : MechaAssembler
{
    protected override void Awake()
    {
        var playerProfile = PlayerProfile.Instance;

        torso = playerProfile.torso;
        leftArm = playerProfile.leftArm;
        rightArm = playerProfile.rightArm;
        leg = playerProfile.leg;

        base.Awake();
    }
}
