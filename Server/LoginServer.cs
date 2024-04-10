using DataEntity;
using IServer;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Data;

namespace Server
{
    public class LoginServer : ILoginService
    {

        #region 依赖注入

        private DbContext _dbContext;
        public LoginServer(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion


        public bool AddUserInfo(string phone)
        {
            UserInfo userInfo = new UserInfo
            {
                PassWord = "123456",
                Phone = phone,
                Lock = false,
                RoleId = _dbContext.Set<Role>().Where(r => r.RoleName == "普通用户").First().Id,
                HeadPortrait = "\\car\\202402291740231r.jpg;"
            };
            _dbContext.Add(userInfo);
            return _dbContext.SaveChanges() > 0;
        }

        public bool AddUserInfo(string phone, string name, int roleId)
        {
            UserInfo userInfo = new UserInfo
            {
                PassWord = "123456",
                Phone = phone,
                Name = name,
                RoleId = roleId,
                Lock = false,
                HeadPortrait = "\\car\\202402291740231r.jpg;"
            };
            _dbContext.Add(userInfo);
            return _dbContext.SaveChanges() > 0;
        }

        public MsgResult CheckRepeatLock(string phone)
        {
            MsgResult result = new MsgResult();
            var userInfo = _dbContext.Set<UserInfo>().Where(u => u.Phone == phone).FirstOrDefault();
            if (userInfo != null)
            {
                if (userInfo.Lock)
                {
                    result.msg = "此账号已锁定";
                    result.result = true;
                    return result;
                }
                else
                {
                    result.msg = "此账号已存在";
                    result.result = true;
                    return result;
                }
            }
            else
            {
                result.msg = "";
                result.result = false;
                return result;
            }
        }

        public MsgResult CheckLock(string phone)
        {
            MsgResult result = new MsgResult();
            var userInfo=_dbContext.Set<UserInfo>().Where(u=>u.Phone==phone).FirstOrDefault();
            if (userInfo != null)
            {
                if (userInfo.Lock)
                {
                    result.msg = "此账号已锁定";
                    result.result = true;
                    return result;
                }
                else
                {
                    result.msg = "";
                    result.result = false;
                    return result;
                }
            }
            else
            {
                result.msg = "账号错误";
                result.result = true;
                return result;
            }
        }

        public OutSalesContract GetSalesContract1(int SalesId,int LoginId)
        {
            OutSalesContract outSalesContract = new OutSalesContract();
            NewSalesTicket? newSales=_dbContext.Set<NewSalesTicket>().Where(n=>n.Id==SalesId).FirstOrDefault();
            if (newSales != null) {
                var SalesInfo = _dbContext.Set<UserInfo>().Where(s => s.Id == newSales.SellerId).First();
                var BuyerInfo = _dbContext.Set<UserInfo>().Where(b => b.Id == newSales.BuyerId).First();
                var Info = _dbContext.Set<UserInfo>().Where(b => b.Id == newSales.UserId).First();
                var CarInfo =_dbContext.Set<CarInfo>().Where(c=>c.Id==newSales.CarId).First();
                if (SalesInfo != null && BuyerInfo != null)
                {

                    outSalesContract.SellerName = SalesInfo.Name;
                    outSalesContract.SellerPhone = SalesInfo.Phone;
                    outSalesContract.SellerCardNumber = SalesInfo.CardNumber;
                    outSalesContract.BuyerName = BuyerInfo.Name;
                    outSalesContract.BuyerPhone = BuyerInfo.Phone;
                    outSalesContract.BuyerCardNumber = BuyerInfo.CardNumber;

                    outSalesContract.CarName = CarInfo.Name;
                    outSalesContract.VehicleSource = CarInfo.VehicleSource;
                    outSalesContract.TotalPrices = CarInfo.SellingPrice;
                    outSalesContract.CarId = CarInfo.Id;


                    outSalesContract.sellerId = newSales.Id;
                    outSalesContract.CreateTime=newSales.CreateTime;
                    outSalesContract.Submit = newSales.Submit;


                    outSalesContract.Time1 = newSales.Time1;
                    outSalesContract.Reason1=newSales.Reason1;
                    outSalesContract.Idea1 = newSales.Idea1;


                    outSalesContract.Time2 = newSales.Time2;
                    outSalesContract.Name=Info.Name;
                    outSalesContract.Reason2 = newSales.Reason2;
                    outSalesContract.Idea2 = newSales.Idea2;

                    outSalesContract.EndTime=newSales.EndTime;
                    outSalesContract.State=newSales.State;

                    outSalesContract.Edit1= (newSales.SellerId==LoginId && outSalesContract.Idea1==null) ?false:true;//第一个审核人可不可以编辑
                    outSalesContract.Edit2 = (newSales.UserId == LoginId &&outSalesContract.Idea2 == null && outSalesContract.Idea1 != null) ? false : true;//第二个审核人可不可以编辑
                }
            }
            return outSalesContract;

        }

        public OutSalesContract GetSalesContract(int CarId, int BuyerId)
        {
            OutSalesContract outSalesContract = new OutSalesContract();
            //根据CarId,获取车主信息以及汽车信息
            var CarInfo=_dbContext.Set<CarInfo>().Where(c=>c.Id==CarId).First();
           
            if (CarInfo!=null) 
            {
                var SalesInfo = _dbContext.Set<UserInfo>().Where(u => u.Id == CarInfo.UserId).First();
                var BuyerInfo = _dbContext.Set<UserInfo>().Where(u => u.Id == BuyerId).First();
                if (SalesInfo != null && BuyerInfo != null) {
                    
                    outSalesContract.SellerName = SalesInfo.Name;
                    outSalesContract.SellerPhone = SalesInfo.Phone;
                    outSalesContract.SellerCardNumber = SalesInfo.CardNumber;
                    outSalesContract.BuyerName= BuyerInfo.Name;
                    outSalesContract.BuyerPhone= BuyerInfo.Phone;
                    outSalesContract.BuyerCardNumber= BuyerInfo.CardNumber;
                    outSalesContract.CarName= CarInfo.Name;
                    outSalesContract.VehicleSource= CarInfo.VehicleSource;
                    outSalesContract.TotalPrices = CarInfo.SellingPrice;
                    outSalesContract.CarId= CarInfo.Id;
                }

            }
            return outSalesContract;
        }

        public List<OutUserInfo> GetUserInfo(int page, int limit, out int count, string searchName)
        {
            //var Info1 = _dbContext.Set<UserInfo>().Where(u =>  u.Lock == false).ToList();
            
            //List < OutUserInfo > outUserInfo1 = new List<OutUserInfo>();
            //foreach (var Info in Info1)
            //{
            //    OutUserInfo outUserInfo = new OutUserInfo();
            //    outUserInfo.Name = Info.Name;
            //    outUserInfo.Phone = Info.Phone;
            //    outUserInfo.CardNumber = Info.CardNumber;
            //    outUserInfo.IdNumber = Info.IdNumber;
            //    outUserInfo.HeadPortrait = Info.HeadPortrait;
            //    outUserInfo.Address = Info.Address;
            //    outUserInfo1.Add(outUserInfo);

            //}

            List<OutUserInfo> outUserInfo2 = new List<OutUserInfo>();
            var ab = (from u in  _dbContext.Set<UserInfo>()
                      join rr in _dbContext.Set<Role>()
                      on u.RoleId equals rr.Id into ewe
                      orderby u.Id descending
                      select new OutUserInfo
                      {
                          Id=u.Id,
                          Address = u.Address,
                          Name = u.Name,
                          Phone = u.Phone,
                          Lock = u.Lock,
                          RoleName = ewe.ToList().First().RoleName,
                      });
            if (searchName != "")
            {
                ab = ab.Where(u=>u.Name.Contains(searchName));
            }
            outUserInfo2 = ab.Skip((page - 1) * limit).Take(limit).ToList();
            count = ab.ToList().Count;
            return outUserInfo2;
        }

        public OutUserInfo GetUserInfo(int userId)
        {
            var Info = _dbContext.Set<UserInfo>().Where(u => u.Id == userId && u.Lock==false).First();
            OutUserInfo outUserInfo = new OutUserInfo();
            if (Info != null)
            {
                outUserInfo.Id = Info.Id;
                outUserInfo.Name = Info.Name;
                outUserInfo.Phone = Info.Phone;
                outUserInfo.CardNumber = Info.CardNumber;
                outUserInfo.IdNumber = Info.IdNumber;
                outUserInfo.HeadPortrait = Info.HeadPortrait;
                outUserInfo.Address = Info.Address;
                outUserInfo.OpeningBank = Info.OpeningBank;
                outUserInfo.Mailbox = Info.Mailbox;
                outUserInfo.Lock = true;
            }
            return outUserInfo;
        }

        public bool NegationLock(int Id)
        {
            var userInfo = _dbContext.Set<UserInfo>().Where(u => u.Id == Id).First();
            if (userInfo != null)
            {
                userInfo.Lock = !userInfo.Lock;
            }
            return _dbContext.SaveChanges() > 0;
        }

        public bool ResetPasswords(int Id)
        {
            var userInfo = _dbContext.Set<UserInfo>().Where(u => u.Id == Id).First();
            if (userInfo != null)
            {
                userInfo.PassWord = "123456123";
            }
            return _dbContext.SaveChanges()>0;
        }

        public bool UpdatePassword(string password, int UserId)
        {
            UserInfo? userInfo = _dbContext.Set<UserInfo>().Where(u=> u.Id == UserId).FirstOrDefault();
            if (userInfo != null)
            {
                userInfo.PassWord = password;
                _dbContext.Update(userInfo);
                
            }
            return _dbContext.SaveChanges() > 0;
        }

        public bool UpdateUserInfo(OutUserInfo outUserInfo)
        {
            UserInfo? userInfo = _dbContext.Set<UserInfo>().Where(u => u.Id == outUserInfo.Id).FirstOrDefault();

            if (userInfo != null)
            {
                
                userInfo.Name = outUserInfo.Name;
                userInfo.Phone = outUserInfo.Phone;
                userInfo.HeadPortrait = outUserInfo.HeadPortrait;
                userInfo.Address= outUserInfo.Address;
                userInfo.IdNumber= outUserInfo.IdNumber;
                userInfo.CardNumber= outUserInfo.CardNumber;
                userInfo.OpeningBank= outUserInfo.OpeningBank;
                userInfo.Mailbox= outUserInfo.Mailbox;
                _dbContext.Update(userInfo);

                //PropertyInfo[] properties = typeof(UserInfo).GetProperties();

                //PropertyInfo[] properties1 = typeof(OutUserInfo).GetProperties();
                //foreach (var item in properties)
                //{
                //    foreach (var item1 in properties1) {
                //        if (item.Name == item1.Name) {

                //        }
                //    }
                //}
                //Console.WriteLine(userInfo);
            }
            return _dbContext.SaveChanges() > 0;

        }

        public JwtMdel VerificationLogin(string phone, string password,out bool tf)
        {
            JwtMdel jwtMdel = new JwtMdel();
            UserInfo? userInfo= _dbContext.Set<UserInfo>().Where(u => u.PassWord == password && u.Phone == phone).FirstOrDefault();
            if (userInfo != null)
            {

                Role role = _dbContext.Set<Role>().Where(r => r.Id == userInfo.RoleId).First();
                if (role != null)
                {
                    jwtMdel.id = userInfo.Id;
                    jwtMdel.Phone = userInfo.Phone;
                    jwtMdel.Role = role.RoleName;
                    tf= true;
                    return jwtMdel;
                }

            }
            tf = false;
            return jwtMdel;
        }

        public OutNamePhone GetOutNamePhone(int CarId)
        {
            var ab = (from u in _dbContext.Set<UserInfo>()
                      join car in _dbContext.Set<CarInfo>().Where(c => c.Id == CarId)
                      on u.Id equals car.UserId 
                      select new OutNamePhone
                      {
                          Name = u.Name,
                          PhoneNumber = u.Phone,
                      }).FirstOrDefault() ;
            
            return ab;
        }

        public bool AddSalesTicket(int CarId, int BuyerId)
        {
            using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
            { //使用事务
                try {
                    NewSalesTicket newSalesTicket = new NewSalesTicket();
                    ////根据CarId,获取车主信息以及汽车信息
                    CarInfo? CarInfo = _dbContext.Set<CarInfo>().Where(c => c.Id == CarId).First();
                    if (CarInfo != null)
                    {
                        var SalesInfo = _dbContext.Set<UserInfo>().Where(u => u.Id == CarInfo.UserId).First();
                        if (SalesInfo != null)
                        {
                            newSalesTicket.SellerId = SalesInfo.Id;
                        }
                        CarInfo.VehicleState = "售出中";
                        newSalesTicket.CreateTime = DateOnly.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                        newSalesTicket.Submit = true;
                        newSalesTicket.BuyerId = BuyerId;
                        newSalesTicket.CarId = CarId;
                        newSalesTicket.UserId = 4;
                        newSalesTicket.State = "待审核";
                        _dbContext.Add(newSalesTicket);
                        _dbContext.Update(CarInfo);
                        dbContextTransaction.Commit();
                    }                    
                }
                catch
                {
                    dbContextTransaction.Rollback();
                }
            }
            return _dbContext.SaveChanges()>0;
        }

        public List<DTOSalesTicket> GetSalesList(int page, int limit, out int count, int UserId)
        {
            List<DTOSalesTicket> outSalesList = new List<DTOSalesTicket>();
            List<NewSalesTicket> newSalesTicket = _dbContext.Set<NewSalesTicket>().Where(n => n.BuyerId == UserId).Skip((page - 1) * limit).Take(limit).ToList();
            foreach (NewSalesTicket newSales in newSalesTicket) {
                var Userinfo = _dbContext.Set<UserInfo>().Where(u => u.Id == newSales.BuyerId || u.Id == newSales.SellerId);
                DTOSalesTicket salesContract = new DTOSalesTicket();
                var na= (from a in Userinfo
                                            where a.Id == newSales.SellerId
                                            select a.Name);
                salesContract.SellerName = string.Concat(na);
                salesContract.BuyerName = string.Concat(from a in Userinfo
                                           where a.Id == newSales.BuyerId
                                           select a.Name);
                salesContract.State = newSales.State;
                salesContract.CreateTime = newSales.CreateTime;
                salesContract.Submit = newSales.Submit;
                salesContract.CarId= newSales.CarId;
                salesContract.Id = newSales.Id;
                outSalesList.Add(salesContract);
            }
            count = newSalesTicket.Count;
            return outSalesList;
        }

        public List<DTOSalesTicket> GetSalesList(int page, int limit, out int count)
        {
            List<DTOSalesTicket> outSalesList = new List<DTOSalesTicket>();
            List<NewSalesTicket> newSalesTicket = _dbContext.Set<NewSalesTicket>().Skip((page - 1) * limit).Take(limit).ToList();
            foreach (NewSalesTicket newSales in newSalesTicket)
            {
                var Userinfo = _dbContext.Set<UserInfo>().Where(u => u.Id == newSales.BuyerId || u.Id == newSales.SellerId);
                DTOSalesTicket salesContract = new DTOSalesTicket();
                var na = (from a in Userinfo
                          where a.Id == newSales.SellerId
                          select a.Name);
                salesContract.SellerName = string.Concat(na);
                salesContract.BuyerName = string.Concat(from a in Userinfo
                                                        where a.Id == newSales.BuyerId
                                                        select a.Name);
                salesContract.State = newSales.State;
                salesContract.CreateTime = newSales.CreateTime;
                salesContract.Submit = newSales.Submit;
                salesContract.CarId = newSales.CarId;
                salesContract.Id = newSales.Id;
                outSalesList.Add(salesContract);
            }
            count = newSalesTicket.Count;
            return outSalesList;
        }

        public List<DTOSalesTicket> GetSellOrdersListByUserId(int page, int limit, out int count, int UserId, bool t)
        {
            List<DTOSalesTicket> outSalesList = new List<DTOSalesTicket>();
            List<NewSalesTicket> newSalesTicket = _dbContext.Set<NewSalesTicket>().Where(n => n.SellerId == UserId).Skip((page - 1) * limit).Take(limit).ToList();
            if (t) {
                newSalesTicket= _dbContext.Set<NewSalesTicket>().Skip((page - 1) * limit).Take(limit).ToList();
            }
            foreach (NewSalesTicket newSales in newSalesTicket)
            {
                var Userinfo = _dbContext.Set<UserInfo>().Where(u => u.Id == newSales.BuyerId || u.Id == newSales.SellerId);
                DTOSalesTicket salesContract = new DTOSalesTicket();
                var na = (from a in Userinfo
                          where a.Id == newSales.SellerId
                          select a.Name);
                salesContract.SellerName = string.Concat(na);
                salesContract.BuyerName = string.Concat(from a in Userinfo
                                                        where a.Id == newSales.BuyerId
                                                        select a.Name);
                salesContract.State = newSales.State;
                salesContract.CreateTime = newSales.CreateTime;
                salesContract.Submit = newSales.Submit;
                salesContract.CarId = newSales.CarId;
                salesContract.Id = newSales.Id;
                outSalesList.Add(salesContract);
            }
            count = newSalesTicket.Count;
            return outSalesList;
        }

        public List<OutMenu> outMenus(int UserId)
        {
            int RoleId = _dbContext.Set<UserInfo>().Where(u=>u.Id==UserId).Select(u=>u.RoleId).First();
            var MenuId = _dbContext.Set<Power>().Where(p=>p.RoleId== RoleId).Select(p=>p.MenuId).ToList();
            List<OutMenu> outMenus = [];

            var menu = _dbContext.Set<Menu>().Where(m=> MenuId.Contains(m.Id)).ToList();
            foreach (var item in menu)
            {   
                    OutMenu outMenu1 = new OutMenu();
                    outMenu1.Name = item.Name;
                    outMenu1.Id = item.Id;
                    outMenu1.Icon = item.Icon;
                    outMenu1.ParentId = item.ParentId;
                    outMenu1.Click = item.Click;
                    outMenus.Add(outMenu1);
            }
            return outMenus;
        }

        public JwtMdel VerificationLogin(string phone)
        {
            JwtMdel jwtMdel = new JwtMdel();
            UserInfo? userInfo = _dbContext.Set<UserInfo>().Where(u => u.Phone == phone).FirstOrDefault();
            if (userInfo != null)
            {

                Role role = _dbContext.Set<Role>().Where(r => r.Id == userInfo.RoleId).First();
                if (role != null)
                {
                    jwtMdel.id = userInfo.Id;
                    jwtMdel.Phone = userInfo.Phone;
                    jwtMdel.Role = role.RoleName;
                    return jwtMdel;
                }

            }
            return jwtMdel;
        }
    }
}
