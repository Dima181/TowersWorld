namespace Gameplay.Buildings
{
    public enum EBuildingUpgradeResult
    {
        Success,
        MissingResources,
        MissingRequires,
        CanBuySlot,
        ReachedSlotLimit,
        Error
    }
}
