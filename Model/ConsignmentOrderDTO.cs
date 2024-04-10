

namespace Model
{
    public class ConsignmentOrderDTO
    {
        public int Id { get; set; }
        public DateOnly? CreateTime { get; set; }

        public int CarInfoId { get; set; }

        public string? Condition { get; set; }

        public string State { get; set; } = null!;
    }
}
