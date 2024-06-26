﻿using Microsoft.EntityFrameworkCore;
using OOP_Cursach_Sakhno.data.database;
using OOP_Cursach_Sakhno.data.models;
using OOP_Cursach_Sakhno.data.repository;

namespace OOP_Cursach_Sakhno.domain.repository
{
    class EntityCoreRepository : DataBaseRepository
    {
        private DatabaseContext db;
        public EntityCoreRepository(DatabaseContext datab) { 
            db=datab;
        }
        public List<Flat> getFlatsByHab(int habId) { 
            List<int> flats = db.HabitantList.Where(h => h.HabitantId==habId).Select(h=> h.FlatId).ToList();
            return db.Flats.Where(f => flats.Contains(f.id)).ToList();
        }
        public void addFlat(Flat flat)
        {            
          db.Flats.AddAsync(flat);
          db.SaveChangesAsync();
        }

        public int addHabitant(Habitant hab)
        {
             var h = db.Habitant.AddAsync(hab);
             db.SaveChangesAsync();
            return h.Result.Entity.Id;
        }

        public void addHabitantToFlat(int habId, int flatId)
        {
            db.HabitantList.AddAsync(new HabitantInFlat(habId, flatId));
            db.SaveChangesAsync();
        }

        public List<Flat> getFlats()
        {
            return db.Flats.ToList();     
        }

        public List<Habitant> getHabitants()
        {
            return db.Habitant.ToList();
        }

        public List<Habitant> getHabitantsInFlat(int flatId)
        {
            var habIds = db.HabitantList.Where(h=> h.FlatId == flatId);
            return db.Habitant.Where(h=> habIds.Any(habId => habId.HabitantId == h.Id)).ToList();
        }

        public void editHabitant(string name, string surname, string phoneNumber, int id)
        {
            var hab = db.Habitant.Find(id);
            hab.Name = name;
            hab.SurName = surname;
            hab.PhoneNumber = phoneNumber;
            db.Habitant.Update(hab);
            db.SaveChanges();
        }

        public void deleteHabitantFromFlat(int habId, int flatId)
        {
            db.HabitantList
                .Where(h => h.FlatId == flatId && h.HabitantId == habId)
                .ExecuteDelete();
            if(db.HabitantList.Where(h => h.HabitantId == habId).Count() == 0) db.Habitant.Where(h => h.Id == habId).ExecuteDelete();
            db.SaveChanges();
        }

        public void deleteHabitant(int habId)
        {
            db.Habitant.Where(h=> h.Id == habId).ExecuteDelete();
            db.HabitantList
              .Where(h => h.HabitantId == habId)
              .ExecuteDelete();
            db.SaveChanges();
        }
        public void editFlat(Flat flat) { 
            db.Update(flat);
            db.SaveChanges();
        }

        public List<Habitant> findHabitants(String name) {
           return db.Habitant.Where(h => (h.Name + " " + h.SurName).Contains(name)).ToList();
        }
        List<HabitantInFlat> DataBaseRepository.getHabitantsList()
        {
            return db.HabitantList.ToList();
        }
    }
}
