namespace YouYou.Business.Models
{
    public class State
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UF { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
