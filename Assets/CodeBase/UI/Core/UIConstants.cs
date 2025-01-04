namespace UI.Core
{
    public readonly struct UIConstants
    {
        private const string POPUPS_PSTH = "UI/Popups/";
        private const string BUILDINGS_PSTH = "UI/Buildings/";
        private const string SCREENS_PSTH = "UI/Screens/";

        public static string Popups(string name) => POPUPS_PSTH + name;
        public static string Buildings(string name) => BUILDINGS_PSTH + name;
        public static string Screens(string name) => SCREENS_PSTH + name;
    }
}
