using OOP_Cursach_Sakhno.data.database;
using OOP_Cursach_Sakhno.data.models;
using OOP_Cursach_Sakhno.data.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Cursach_Sakhno.domain.repository
{
    class EntityCoreRepository : DabaBaseRepository
    {
        private DatabaseContext db;
        public EntityCoreRepository(DatabaseContext datab) { 
            db=datab;
        }
        public void addFlat(Flat flat)
        {
            throw new NotImplementedException();
        }

        public void addHabitant(Flat flat)
        {
            throw new NotImplementedException();
        }

        public void addHabitantToFlat(int habId, int flatId)
        {
            throw new NotImplementedException();
        }

        public List<Flat> getFlats()
        {
            throw new NotImplementedException();
        }

        public List<Habitant> getHabitants()
        {
            throw new NotImplementedException();
        }

        public List<Habitant> getHabitantsInFlat(int flatId)
        {
            throw new NotImplementedException();
        }
    }
}
