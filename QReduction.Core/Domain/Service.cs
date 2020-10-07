using System;

namespace QReduction.Core.Domain
{
    public class Service
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Note { get; set; }

        public string NoteEN { get; set; }
        public bool IsDeleted { get; set; }
        public int? CreateBy { get; set; }
        public DateTime CreateAt { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }
        public int OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
