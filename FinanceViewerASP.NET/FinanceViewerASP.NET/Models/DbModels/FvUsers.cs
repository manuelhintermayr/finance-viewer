namespace FinanceViewerASP.NET.Models.DbModels
{
    public partial class FvUsers
    {
        public string UName { get; set; }
        public string UPassword { get; set; }
        public int UIsLocked { get; set; }
        public string UFirstName { get; set; }
        public string ULastName { get; set; }
    }
}
