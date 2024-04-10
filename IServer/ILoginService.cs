using Model;

namespace IServer
{
    public interface ILoginService
    {
        /// <summary>
        /// 检查用户是否禁用
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        MsgResult CheckLock(string phone);
        /// <summary>
        /// 验证手机号和密码是否匹配
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <returns></returns>

        JwtMdel VerificationLogin(string phone, string password,out bool tf);

        JwtMdel VerificationLogin(string phone);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="password"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        bool UpdatePassword(string password,int UserId);
        /// <summary>
        /// 返回购车合同
        /// </summary>
        /// <param name="SalesId">合同ID</param>
        /// <returns></returns>
        OutSalesContract GetSalesContract1(int SalesId,int LoginId);
        /// <summary>
        /// 买方获取自己的购车列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>

        List<DTOSalesTicket> GetSalesList(int page, int limit, out int count,int UserId);
        /// <summary>
        /// 卖方获取自己的售车记录
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>

        List<DTOSalesTicket> GetSellOrdersListByUserId(int page, int limit, out int count, int UserId,bool tf);
        /// <summary>
        /// 获取所有销售单信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<DTOSalesTicket> GetSalesList(int page, int limit, out int count);
        /// <summary>
        /// 用户点击购车，返回数据，购车前返回的
        /// </summary>
        /// <param name="CarId"></param>
        /// <param name="BuyerId"></param>
        /// <returns></returns>

        OutSalesContract GetSalesContract(int CarId,int BuyerId);
        bool AddSalesTicket(int CarId, int BuyerId);
        List<OutUserInfo> GetUserInfo(int page, int limit, out int count,string searchName);

        OutUserInfo GetUserInfo(int userId);
        
        bool AddUserInfo(string phone);
        bool UpdateUserInfo(OutUserInfo outUserInfo);

        bool ResetPasswords(int Id);
        /// <summary>
        /// Lock取反
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool NegationLock(int Id);

        bool AddUserInfo(string phone,string name,int roleId);
        /// <summary>
        /// 添加时，校验手机号是否重复以及是否被锁定状态
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        MsgResult CheckRepeatLock(string phone);
        /// <summary>
        /// 输出车主的姓名和电话
        /// </summary>
        /// <param name="CarId"></param>
        /// <returns></returns>
        OutNamePhone GetOutNamePhone(int CarId);
        List<OutMenu> outMenus(int UserId);

    }
}
