

namespace Model
{
    /// <summary>
    /// 前端搜索汽车的条件
    /// </summary>
    public class SearchKey
    {
        public string BrandName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string SellingPrice { get; set; } = null!;
        public int VehicleAge { get; set; }
        public string TransmissionType { get; set; } = null!;
        public int Seating { get; set; }
        public decimal Mileage { get; set; }
        public string Fuel { get; set; } = null!;
        public string VehicleSource { get; set; } = null!;
        public string? VehicleLevelName { get; set; }
    }
}
