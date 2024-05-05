﻿using OOP_Cursach_Sakhno.data.models;

namespace OOP_Cursach_Sakhno.data.repository
{
    public interface DataBaseRepository
        {
            public List<Habitant> getHabitants();
            public List<Flat> getFlats();
            public List<Habitant> getHabitantsInFlat(int flatId);
            public List<HabitantInFlat> getHabitantsList();
            public Task addFlat(Flat flat);
            public void addHabitant(Habitant hab);
            public void addHabitantToFlat(int habId, int flatId);
            public void editHabitant(Habitant hab);
            public void deleteHabitantFromFlat(int habId, int flatId);
            public void deleteHabitant(int habId);
            public void editFlat(Flat flat);
        }
}