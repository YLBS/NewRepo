

namespace Model
{
    public class OutCar
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public decimal SellingPrice { get; set; }

        public string BrandName { get; set; } = null!;


        public decimal DownPayment { get; set; }

        /// <summary>
        /// 车龄
        /// </summary>
        public int VehicleAge { get; set; }

        public decimal Mileage { get; set; }
        /// <summary>
        /// 外观
        /// </summary>
        public string Appearance { get; set; } = null!;
        public string State { get; set; } = null!;
        public string TransmissionType { get; set; } = null!;
        public int Seating { get; set; }
        public string Fuel { get; set; } = null!;
        public string VehicleSource { get; set; } = null!;
        public string? VehicleLevelName { get; set; }


    }
}
