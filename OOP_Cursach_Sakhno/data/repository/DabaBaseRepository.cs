using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_Cursach_Sakhno.data.models;

namespace OOP_Cursach_Sakhno.data.repository
{
    public interface DabaBaseRepository
        {
            public List<Habitant> getHabitants();
            public List<Flat> getFlats();
            public List<Habitant> getHabitantsInFlat(int flatId); 
            public void addFlat(Flat flat);
            public void addHabitant(Flat flat);
            public void addHabitantToFlat(int habId, int flatId);
        }
}
