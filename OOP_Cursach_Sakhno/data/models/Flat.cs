namespace OOP_Cursach_Sakhno.data.models
{
    public class Flat
    {
        public int id { get; set; }
        public double commPaid { get; set; }
        public int number { get; set; }

        public Flat(double commPaid, int number)
        {
            this.commPaid = commPaid;
            this.number = number;
        }
    }
}
