namespace YouYou.Api.ViewModels
{
    public class UserTokenViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public bool ReadTermsOfUse { get; set; }

        public IEnumerable<ClaimViewModel> Claims { get; set; }
    }
}
