using LoginAndRegistration.Modals;

namespace LoginAndRegistration.Dto
{
    public class LoginResponseDto
    {
        public LocalUser user { get; set; }
        public string  Token { get; set; }
    }
}
