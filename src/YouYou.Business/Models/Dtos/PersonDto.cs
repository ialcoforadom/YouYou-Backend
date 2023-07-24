namespace YouYou.Business.Models.Dtos
{
    public class PersonDto
    {
        public PersonDto(Person person, string password)
        {
            Person = person;
            Password = password;
        }
        public PersonDto(Person person)
        {
            Person = person;
        }
        public PersonDto(Person person, string password, Guid roleId)
        {
            Person = person;
            Password = password;
            RoleId = roleId;
        }

        public Person Person { get; set; }

        public string Password { get; set; }
        public Guid RoleId { get; set; }
    }
}
