using Model;
namespace IServer
{
    public interface IJwtService
    {
        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string GetToken(JwtMdel jwtMdel);
        /// <summary>
        /// 解析Token
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        string[] ValidateToken(string Token);
    }
}
