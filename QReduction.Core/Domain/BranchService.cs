namespace QReduction.Core.Domain
{
    public class BranchService
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public int ServiceId { get; set; }

        public Branch Branch { get; set; }
        public Service Service { get; set; }

    }
}
