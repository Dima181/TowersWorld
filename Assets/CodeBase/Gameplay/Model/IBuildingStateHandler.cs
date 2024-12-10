namespace Gameplay.Model
{
    public interface IBuildingStateHandler
    {
        void OnEnterState();
        void OnExitState();
    }

    public interface ISiteStateHandler : IBuildingStateHandler { }
    public interface IBuildStateHandler : IBuildingStateHandler { }
    public interface IStayStateHandler : IBuildingStateHandler { }
}
