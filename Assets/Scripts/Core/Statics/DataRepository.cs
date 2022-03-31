namespace Core.Statics
{
    public static class DataRepository
    {
        public static int SelectedPack { get; set; }
        
        public static int SelectedLevel { get; set; }
        
        public static int CurrentEnergy { get; set; }
        
        public static int CurrentTime { get; set; }
        
        public static bool IsStarted { get; set; }
    }
}