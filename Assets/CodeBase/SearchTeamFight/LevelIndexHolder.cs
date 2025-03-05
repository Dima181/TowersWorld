namespace SearchTeamFight
{
    public class LevelIndexHolder
    {
        private int _index;
        private int _indexBackup;

        public int Index
        {
            get => _index >= MaxIndex ? 0 : _index;
            set => _index = value;
        }

        public int IndexBackup
        {
            get => _indexBackup >= MaxIndex ? 0 : _indexBackup;
            set => _indexBackup = value;
        }

        private int MaxIndex { get; set; }

        public LevelIndexHolder(int maxIndex)
        {
            MaxIndex = maxIndex;
        }

        public int LevelView => Index + 1;
    }
}
