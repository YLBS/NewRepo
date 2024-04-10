using Model;
namespace IServer
{
    public interface ISmsService
    {
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        bool Add(string phone,string code);
        /// <summary>
        /// 查询上一个验证是否已过期，未过期不得再次获取验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        bool VerifyExpiration(string phone);
        /// <summary>
        /// 校验验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        bool VerifyCode(string phone, string code);
        /// <summary>
        /// 验证是注册还是登录
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        MsgResult CheckLock(string phone);

        JwtMdel VerifyLogin(string phone, string code, out bool tf);
    }
}
