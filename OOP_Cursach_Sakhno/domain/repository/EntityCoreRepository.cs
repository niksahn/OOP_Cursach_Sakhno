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
        public Task addFlat(Flat flat)
        {
            return Task.Run(() =>
            {
                db.Flats.AddAsync(flat);
                db.SaveChangesAsync();
            });
        }

        public void addHabitant(Habitant hab)
        {
               db.Habitant.Add(hab);
               db.SaveChanges();
        }

        public void addHabitantToFlat(int habId, int flatId)
        {
            db.HabitantList.Add(new HabitantInFlat(habId, flatId));
            db.SaveChanges();
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

        public void editHabitant(Habitant hab)
        {
            db.Habitant.Update(hab);
            db.SaveChanges();
        }

        public void deleteHabitantFromFlat(int habId, int flatId)
        {
            db.HabitantList
                .Where(h => h.FlatId == flatId && h.HabitantId == habId)
                .ExecuteDelete();
            db.SaveChanges();
        }

        public void deleteHabitant(int habId)
        {
            db.Habitant.Where(h=> h.Id == habId).ExecuteDelete();
            db.SaveChanges();
        }
        public void editFlat(Flat flat) { 
            db.Update(flat);
            db.SaveChanges();
        }

        List<HabitantInFlat> DataBaseRepository.getHabitantsList()
        {
            return db.HabitantList.ToList();
        }
    }
}
