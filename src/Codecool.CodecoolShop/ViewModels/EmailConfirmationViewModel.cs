namespace Codecool.CodecoolShop.ViewModels
{
    public class EmailConfirmationViewModel
    {
        public string FullName { get; private set; }

        public string Link { get; private set; }

        public EmailConfirmationViewModel(string fullName, string link)
        {
            FullName = fullName;
            Link = link;
        }
    }
}
