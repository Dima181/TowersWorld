using UI.Core;

namespace Gameplay.Acceleration
{
    public abstract class BaseProgressTaskView : UIScreenView
    {
        public abstract void SetProgress(string remainedTimeText, float progress);

        public abstract void Show();
        public abstract void Hide();
    }
}
