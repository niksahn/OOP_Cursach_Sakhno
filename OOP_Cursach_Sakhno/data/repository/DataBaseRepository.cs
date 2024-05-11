using OOP_Cursach_Sakhno.data.models;

namespace OOP_Cursach_Sakhno.data.repository
{
    public interface DataBaseRepository
    {
            public List<Habitant> getHabitants();
            public List<Flat> getFlats();
            public List<Flat> getFlatsByHab(int habId);
            public List<Habitant> getHabitantsInFlat(int flatId);
            public List<HabitantInFlat> getHabitantsList();
            public void addFlat(Flat flat);
            public int addHabitant(Habitant hab);
            public void addHabitantToFlat(int habId, int flatId);
            public void editHabitant(string name, string surname, string phoneNumber, int id);
            public void deleteHabitantFromFlat(int habId, int flatId);
            public void deleteHabitant(int habId);
            public void editFlat(Flat flat);
            public List<Habitant> findHabitants(String name);
    }
}
