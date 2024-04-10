using DataEntity;
using Model;

namespace IServer
{
    public interface ICarInfoSerivce
    {
        /// <summary>
        /// 添加车辆信息
        /// </summary>
        /// <param name="carInfo"></param>
        /// <returns></returns>
        bool Add(InputCarInfo carInfo,int Id);
        /// <summary>
        /// 返回车辆信息列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="limit">每页数目</param>
        /// <param name="count">总数目</param>
        /// <param name="searchCriteria">搜索条件</param>
        /// <returns></returns>
        List<OutCar> GetCarList(int page, int limit, out int count,string searchCriteria, string cName);
        /// <summary>
        /// 搜索获取
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>

        List<OutCar> GetCarListBySearch(int page, int limit, out int count, SearchKey searchKey);
        /// <summary>
        /// 根据ID返回车辆信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        OutCarInfoDTO GetCarInfo(int Id,bool tf);
        /// <summary>
        /// 分页获取寄售单信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<ConsignmentOrderDTO> GetConsignmentOrder(int page, int limit, out int count);
        /// <summary>
        /// 获取自己的寄售信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        List<OutCar> GetCarListByUserId(int page, int limit, out int count,int UserId);
        /// <summary>
        /// 获取汽车类型，在审核寄售单是管理员添加
        /// </summary>
        /// <returns></returns>
        List<VehicleLevel> GetCarType();
        /// <summary>
        /// 修改寄售审核单
        /// </summary>
        /// <param name="Id">寄售单ID</param>
        /// <param name="Idea">意见，同意或不同意</param>
        /// <param name="Options">根据意见修改汽车状态，同意就是汽车类型，不同意就是不同意的理由</param>
        /// <returns></returns>
        bool UpCarState(int Id,string Idea,string Options);
        /// <summary>
        /// 下架
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool SoldOutById(int Id);
        /// <summary>
        /// 修改合同状态
        /// </summary>
        /// <returns></returns>
        bool UpNewSalesTickets(UpSales upSales,int button,bool tf);


    }
}
