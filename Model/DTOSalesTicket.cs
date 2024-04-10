
namespace Model
{
    public class DTOSalesTicket
    {
        public int Id { get; set; }

        public DateOnly? CreateTime { get; set; }

        public bool Submit { get; set; }

        public string? BuyerName {  get; set; }

        public string? SellerName {  get; set; }

        public int CarId { get; set; }

        public string? State { get; set; }
    }
}
