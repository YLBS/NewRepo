using DataEntity;
using IServer;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Server
{
    public class SmsService : ISmsService
    {
        private DbContext _dbContext;
        public SmsService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool Add(string phone, string code)
        {
            DateTime currentTime = DateTime.Now;
            AuthCode authCode = new AuthCode
            {
                Phone = phone,
                Code = code,
                ExpirationTime = currentTime.AddMinutes(5)

            };
            _dbContext.Add(authCode);
            return _dbContext.SaveChanges() > 0;
        }

       

        public bool VerifyExpiration(string phone)
        {

            DateTime currentTime = DateTime.Now;
            var authCode = _dbContext.Set<AuthCode>().Where(a => a.Phone == phone && a.ExpirationTime > currentTime).OrderBy(a => a.ExpirationTime).FirstOrDefault();
            if (authCode != null) //未过期，不得再获取验证码
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool VerifyCode(string phone, string code)
        {
            DateTime currentTime = DateTime.Now;
            var authCode = _dbContext.Set<AuthCode>().Where(a => a.Phone == phone && a.Code == code && a.ExpirationTime > currentTime).FirstOrDefault();
            if (authCode != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public MsgResult CheckLock(string phone)
        {
            MsgResult result = new MsgResult();
            var userInfo = _dbContext.Set<UserInfo>().Where(u => u.Phone == phone).FirstOrDefault();
            if (userInfo != null)
            {
                if (userInfo.Lock)
                {
                    result.msg = "此账号已锁定";
                    result.result = false;
                    return result;
                }
                else
                {
                    result.msg = "登录";
                    result.result = true;
                    return result;
                }
            }
            else
            {
                result.msg = "注册";
                result.result = true;
                return result;
            }
        }

        public JwtMdel VerifyLogin(string phone, string code, out bool tf)
        {

            throw new NotImplementedException();
        }
    }
}
