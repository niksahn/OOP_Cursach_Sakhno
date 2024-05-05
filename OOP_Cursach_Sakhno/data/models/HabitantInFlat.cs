namespace OOP_Cursach_Sakhno.data.models
{
    public class HabitantInFlat
    {
        public int Id { get; set; }
        public int HabitantId { get; set; }
        public int FlatId { get; set; }

        public HabitantInFlat(int habitantId, int flatId)
        {
            HabitantId = habitantId;
            FlatId = flatId;
        }
    }
}
