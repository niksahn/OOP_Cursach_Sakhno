namespace OOP_Cursach_Sakhno.data.models
{
    public class Habitant
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public string PhoneNumber { get; set; }

        public Habitant(string name, string surName, string phoneNumber) { 
            Name = name;
            SurName= surName;   
            PhoneNumber= phoneNumber;
        }
    }
}
