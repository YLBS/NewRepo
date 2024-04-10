using DataEntity;
using IServer;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Server
{
    public class CarInfoServer : ICarInfoSerivce
    {
        #region 依赖注入

        private DbContext _dbContext;
        public CarInfoServer(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        #endregion

        bool ICarInfoSerivce.Add(InputCarInfo carInfo, int Id)
        {
            CarInfo car = new CarInfo
            {
                UserId = Id,
                BrandName = carInfo.BrandName,
                Name = carInfo.Name,
                RegistrationTime = DateOnly.Parse(carInfo.RegistrationTime),
                SellingPrice = carInfo.SellingPrice,
                Amortize = carInfo.Amortize,
                DownPayment = carInfo.DownPayment,
                VehicleAge = carInfo.VehicleAge,
                TransmissionType = carInfo.TransmissionType,
                Seating = carInfo.Seating,
                Mileage = carInfo.Mileage,
                Fuel = carInfo.Fuel,
                VehicleSource = carInfo.VehicleSource,
                VehicleColor = carInfo.VehicleColor,
                TransfersNumber = carInfo.TransfersNumber,
                Registration = carInfo.Registration,
                Appearance = carInfo.Appearance,
                VehiTrim = carInfo.VehiTrim,
                VehicleCondition = carInfo.VehicleCondition,
                VehicleLevelName = "",
                VehicleState = "审核中"
            };

            using (var dbContextTransaction = _dbContext.Database.BeginTransaction()) { //使用事务
                try
                {
                    _dbContext.Set<CarInfo>().Add(car);
                    
                    WorkFlow workFlow = _dbContext.Set<WorkFlow>().Where(w =>w.WorkFlowName=="流程一").First();
                    if (workFlow!=null)
                    {
                        int id = _dbContext.Set<CarInfo>().OrderByDescending(c=>c.Id).Select(c=>c.Id).First()+1;
                        ConsignmentOrder consignmentOrder = new ConsignmentOrder();
                        consignmentOrder.State = "待审核";
                        consignmentOrder.CarInfoId = id;
                        consignmentOrder.CurrentProcessor = workFlow.CurrentProcessor;
                        _dbContext.Set<ConsignmentOrder>().Add(consignmentOrder);
                        _dbContext.Set<CarInfo>().Add(car);
                    }
                    dbContextTransaction.Commit();
                } catch {
                    dbContextTransaction.Rollback();
                } 

            }
            return _dbContext.SaveChanges() > 0;
        }

        public List<OutCar> GetCarList(int page, int limit, out int count, string searchCriteria,string cName)
        {
            List<OutCar> carList = new List<OutCar>();
            List<CarInfo> carInfos = null!;
            if (searchCriteria != "") {

                carInfos = _dbContext.Set<CarInfo>().Where(c => c.VehicleState == "热售中" && c.VehicleSource == cName && c.Name.Contains(searchCriteria)).ToList();

            }
            else
            {
                carInfos = _dbContext.Set<CarInfo>().Where(c => c.VehicleState == "热售中" && c.VehicleSource == cName).ToList();
            }
            
            int y=DateTime.Now.Year;
            foreach (CarInfo carInfo in carInfos.Skip((page - 1) * limit).Take(limit))
            {
                OutCar outCar = new OutCar();
                outCar.Id = carInfo.Id;
                outCar.Name = carInfo.Name;
                outCar.SellingPrice = carInfo.SellingPrice;
                outCar.DownPayment=carInfo.DownPayment;
                outCar.Appearance = carInfo.Appearance.Split(';')[0];
                outCar.VehicleAge = y- carInfo.VehicleAge;
                outCar.Mileage = carInfo.Mileage;
                carList.Add(outCar);
            }
            count = carInfos.Count();
            return carList;
        }

        public List<ConsignmentOrderDTO> GetConsignmentOrder(int page, int limit, out int count)
        {
            List<ConsignmentOrder> consignmentOrders = _dbContext.Set<ConsignmentOrder>().Skip((page - 1) * limit).Take(limit).ToList();
            List<ConsignmentOrderDTO> consignments = new List<ConsignmentOrderDTO>();
            foreach (ConsignmentOrder consignment in consignmentOrders)
            {
                ConsignmentOrderDTO coDTO = new ConsignmentOrderDTO();
                coDTO.Id = consignment.Id;
                coDTO.State = consignment.State;
                coDTO.CreateTime=consignment.CreateTime;
                coDTO.Condition=consignment.Condition;
                coDTO.CarInfoId = consignment.CarInfoId;
                consignments.Add(coDTO);
            }
            count = _dbContext.Set<ConsignmentOrder>().Count();
            return consignments;
        }

        public OutCarInfoDTO GetCarInfo(int Id, bool tf)
        {
           // CarInfo? carInfos1 = _dbContext.Set<CarInfo>().Where(c=>c.Id==Id).FirstOrDefault();
            var carInfos = from carInfo in _dbContext.Set<CarInfo>()
                          where carInfo.Id == Id
                          select carInfo;
            if (tf)
            {
                carInfos =( from carInfo in carInfos
                        where carInfo.VehicleState == "热售中"
                           select carInfo);
            }

            CarInfo[] carInfos1 = carInfos.ToArray();
            OutCarInfoDTO outCarInfo = new OutCarInfoDTO();
            if (carInfos1.Length!=0)
            {

                outCarInfo.Id = carInfos1[0].Id;
                outCarInfo.Name = carInfos1[0].Name;
                outCarInfo.TransfersNumber = carInfos1[0].TransfersNumber;
                outCarInfo.BrandName = carInfos1[0].BrandName;
                outCarInfo.VehicleLevelName = carInfos1[0].VehicleLevelName;
                outCarInfo.Amortize = carInfos1[0].Amortize;
                outCarInfo.Appearance = carInfos1[0].Appearance;
                outCarInfo.DownPayment = carInfos1[0].DownPayment;
                outCarInfo.Fuel = carInfos1[0].Fuel;
                outCarInfo.SellingPrice = carInfos1[0].SellingPrice;
                outCarInfo.VehicleSource = carInfos1[0].VehicleSource;
                outCarInfo.Mileage = carInfos1[0].Mileage;
                outCarInfo.Registration = carInfos1[0].Registration;
                outCarInfo.RegistrationTime = carInfos1[0].RegistrationTime;
                outCarInfo.VehicleAge = carInfos1[0].VehicleAge;
                outCarInfo.VehicleColor = carInfos1[0].VehicleColor;
                outCarInfo.Seating = carInfos1[0].Seating;
                outCarInfo.VehiTrim = carInfos1[0].VehiTrim;
                outCarInfo.VehicleCondition = carInfos1[0].VehicleCondition;
                outCarInfo.VehicleState = carInfos1[0].VehicleState;
                outCarInfo.TransmissionType = carInfos1[0].TransmissionType;
                string? proposer = _dbContext.Set<UserInfo>().Where(u => u.Id == carInfos1[0].UserId).Select(u => u.Name).FirstOrDefault();
                outCarInfo.Proposer = "";
                if (proposer != null)
                {
                    outCarInfo.Proposer = proposer;
                }
            }
            return outCarInfo;
        }

        public List<VehicleLevel> GetCarType()
        {

            List<VehicleLevel> outCarTypes = new List<VehicleLevel>();
            return(_dbContext.Set<VehicleLevel>().ToList());

        }

        public bool UpCarState(int Id, string Idea, string Options)
        {

            using (var dbContextTransaction = _dbContext.Database.BeginTransaction()) {
                try
                {
                    ConsignmentOrder? consignmentOrder = _dbContext.Set<ConsignmentOrder>().Where(c=>c.State=="待审核" && c.Id==Id).FirstOrDefault();
                   
                    if (consignmentOrder != null)
                    {
                        CarInfo? carInfo = _dbContext.Set<CarInfo>().Find(consignmentOrder.CarInfoId);

                        consignmentOrder.State = Idea;
                        if(carInfo != null)
                        {
                            if (Idea == "不同意")
                            {
                                carInfo.VehicleState = "未通过审核";
                                consignmentOrder.Condition = Options; //添加不同意的原因
                            }
                            else
                            {
                                carInfo.VehicleState = "热售中";
                                carInfo.VehicleLevelName = Options;//添加汽车类型
                            }
                            _dbContext.Update(consignmentOrder);
                            _dbContext.Update(carInfo);

                            dbContextTransaction.Commit();
                        }
                        
                    }
                    
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
            return _dbContext.SaveChanges() > 0;
        }

        public List<OutCar> GetCarListByUserId(int page, int limit, out int count, int UserId)
        {
            List<OutCar> carList = new List<OutCar>();
            List<CarInfo> carInfos = _dbContext.Set<CarInfo>().Where(c => c.UserId == UserId).ToList();

            int y = DateTime.Now.Year;
            foreach (CarInfo carInfo in carInfos.Skip((page - 1) * limit).Take(limit))
            {
                OutCar outCar = new OutCar();
                outCar.Id = carInfo.Id;
                outCar.BrandName = carInfo.BrandName;
                outCar.Name = carInfo.Name;
                outCar.SellingPrice = carInfo.SellingPrice;
                outCar.DownPayment = carInfo.DownPayment;
                outCar.Appearance = carInfo.Appearance.Split(';')[0];
                outCar.VehicleAge = y - carInfo.VehicleAge;
                outCar.Mileage = carInfo.Mileage;
                outCar.State = carInfo.VehicleState;
                carList.Add(outCar);
            }
            count = carInfos.Count();
            return carList;
        }

        public bool SoldOutById(int Id)
        {
            CarInfo? carInfos = _dbContext.Set<CarInfo>().Where(c => c.Id == Id&& c.VehicleState== "热售中").FirstOrDefault();
            if (carInfos != null) {
                carInfos.VehicleState = "已下架";
                _dbContext.Update(carInfos);
            }
            return _dbContext.SaveChanges() > 0;
        }

        public List<OutCar> GetCarListBySearch(int page, int limit, out int count, SearchKey searchKey)
        {
            List<OutCar> carList = new List<OutCar>();
            var dd = (from c in _dbContext.Set<CarInfo>().Where(c => c.VehicleState == "热售中")
                                     select new OutCar
                                     {
                                         Id = c.Id,
                                         BrandName=c.BrandName,
                                         TransmissionType=c.TransmissionType,
                                         Name = c.Name,
                                         VehicleAge=c.VehicleAge,
                                         Seating=c.Seating,
                                         SellingPrice = c.SellingPrice,
                                         DownPayment = c.DownPayment,
                                         Fuel=c.Fuel,
                                         VehicleSource=c.VehicleSource,
                                         VehicleLevelName=c.VehicleLevelName,
                                         Appearance = c.Appearance,
                                         Mileage = c.Mileage,
                                     });
            if (searchKey.BrandName != null)
            {
                dd = dd.Where(c => c.BrandName == searchKey.BrandName);
            }
            if (searchKey.Name != null)
            {
                dd = dd.Where(c => c.Name.Contains(searchKey.Name));
            }
            if (searchKey.VehicleAge != 0)
            {
                if (searchKey.VehicleAge == 9)
                {
                    dd = dd.Where(c => c.VehicleAge >= searchKey.VehicleAge);
                }
                else {
                    dd = dd.Where(c => c.VehicleAge <= searchKey.VehicleAge);
                }
                
            }
            if (searchKey.TransmissionType != null)
            {
                dd = dd.Where(c => c.TransmissionType == searchKey.TransmissionType);
            }
            if (searchKey.Seating != 0)
            {
                if (searchKey.Seating == 7)
                {
                    dd = dd.Where(c => c.Seating >= searchKey.Seating );
                }
                else {
                    dd = dd.Where(c => c.Seating == searchKey.Seating);
                }
            }
            if (searchKey.SellingPrice != null)
            {
                if (searchKey.SellingPrice == "两万以下") {
                    dd = dd.Where(c => c.SellingPrice < 2);
                }
                else
                {
                    string[] strings = searchKey.SellingPrice.Split('万');
                    try
                    {
                        int price1 = Convert.ToInt32(strings[0]);
                        int price2 = Convert.ToInt32(strings[1].Substring(1));

                        dd = dd.Where(c => c.SellingPrice < price2 && c.SellingPrice > price1);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            if (searchKey.Mileage != 0)
            {
                if (searchKey.Mileage == 11)
                {
                    dd = dd.Where(c => c.Mileage >= searchKey.Mileage);
                }
                else {
                    dd = dd.Where(c => c.Mileage <= searchKey.Mileage);
                }
                   
            }
            if (searchKey.Fuel != null)
            {
                dd = dd.Where(c => c.Fuel == searchKey.Fuel);
            }
            if (searchKey.VehicleSource != null)
            {
                dd = dd.Where(c => c.VehicleSource.Contains(searchKey.VehicleSource));
            }

            try {
                count = dd.ToList().Count;
                carList = dd.Skip((page - 1) * limit).Take(limit).ToList();
            }catch (Exception ex)
            {
                count = 0;
            }
            
            return carList;
            //throw new NotImplementedException();
        }

        public bool UpNewSalesTickets(UpSales upSales,int button, bool tf)
        {
            var carInfo=_dbContext.Set<CarInfo>().Where(c=>c.Id.Equals(upSales.CarId)).First();
            var sales = _dbContext.Set<NewSalesTicket>().Where(n => n.Id.Equals(upSales.Id)).First();
            if (sales != null && carInfo!=null)
            {
                DateTime date = DateTime.Now.Date;
                int y = date.Year;
                int m = date.Month;
                int d=date.Day;
                using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        if (button == 1)
                        {
                            sales.Idea1 = upSales.Idea1;
                            sales.Reason1 = upSales.Reason1;
                            sales.Time1 = DateOnly.Parse(y + "-" + m + "-" + d);
                            if (upSales.Idea1 == "不同意")
                            {
                                sales.EndTime = DateOnly.Parse(y + "-" + m + "-" + d);
                                sales.State = "结束";
                                carInfo.VehicleState = "热售中";
                            }
                            else { 
                                if (!tf) {
                                    sales.EndTime = DateOnly.Parse(y + "-" + m + "-" + d);
                                    sales.State = "结束";
                                    carInfo.VehicleState = "已售出";
                                } 
                            }
                        }
                        else if(button==2) {

                            sales.Idea2 = upSales.Idea2;
                            sales.Reason2 = upSales.Reason2;
                            sales.Time2 = DateOnly.Parse(y + "-" + m + "-" + d);
                            sales.EndTime = DateOnly.Parse(y + "-" + m + "-" + d);
                            sales.State = "结束";
                            if (upSales.Idea2 == "不同意") {
                                carInfo.VehicleState = "热售中";
                            }
                            else
                            {
                                carInfo.VehicleState = "已售出";
                            }
                        }
                        dbContextTransaction.Commit();
                    }
                    catch
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }
            
            return _dbContext.SaveChanges() > 0;
        }
    }
}
