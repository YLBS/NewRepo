

namespace Model
{

    public class JWTTokenOptions
    {
        public string Audience { get; set; } = null!;
        public string Isuser { get; set; } = null!;
        public string SecurityKey { get; set; } = null!;


    }
}
