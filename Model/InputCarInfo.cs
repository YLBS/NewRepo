
namespace Model
{
    public class InputCarInfo
    {
        public string BrandName { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string RegistrationTime { get; set; } = null!;

        public decimal SellingPrice { get; set; }

        public bool Amortize { get; set; } 

        public decimal DownPayment { get; set; }

        public int VehicleAge { get; set; }

        public string TransmissionType { get; set; } = null!;

        public int Seating { get; set; }

        public decimal Mileage { get; set; }

        public string Fuel { get; set; } = null!;

        public string VehicleSource { get; set; } = null!;

        public string VehicleColor { get; set; } = null!;

        public int TransfersNumber { get; set; }

        public string Registration { get; set; } = null!;

        public string Appearance { get; set; } = null!;

        public string VehiTrim { get; set; } = null!;

        public string VehicleCondition { get; set; } = null!;


    }
}
