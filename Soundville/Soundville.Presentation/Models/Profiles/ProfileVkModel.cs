namespace Soundville.Presentation.Models.Profiles
{
    public class ProfileVkModel
    {
        public string Token { get; set; }

        public ProfileVkModel(string token)
        {
            Token = token;
        }
    }

}
