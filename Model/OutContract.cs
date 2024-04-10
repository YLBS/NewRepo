

namespace Model
{
    public class OutSalesContract
    {
        public int sellerId {  get; set; }
        public string? SellerName { get; set; }
        public string? SellerPhone {  get; set; }  
        public string? SellerCardNumber {  get; set; }
        public DateOnly? CreateTime { get; set; }
        public bool Submit { get; set; }
        public string? BuyerName { get;  set; }

        public string? BuyerPhone { get;  set; }
        public string? BuyerCardNumber { get;  set; }
        public int CarId { get; set; }  
        public string? CarName { get; set;}
        public decimal TotalPrices { get; set;}
        public string? VehicleSource { get; set;}
        public DateOnly? Time1 { get; set; }
        public string? Reason1 { get; set; }

        public string? Idea1 { get; set; }

        public int UserId { get; set; }

        public DateOnly? Time2 { get; set; }

        public string? Idea2 { get; set; }

        public string? Reason2 { get; set; }

        public string? State { get; set; }
        public string? Name { get; set; }

        public DateOnly? EndTime { get; set; }
        public bool Edit1 {  get; set; }=false;
        public bool Edit2 { get; set; }=false;

    }
}
