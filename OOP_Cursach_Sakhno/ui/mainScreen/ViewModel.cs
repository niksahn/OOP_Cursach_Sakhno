using OOP_Cursach_Sakhno.data.database;
using OOP_Cursach_Sakhno.data.models;
using OOP_Cursach_Sakhno.data.repository;
using OOP_Cursach_Sakhno.domain.repository;
using static OOP_Cursach_Sakhno.ui.mainScreen.ScreenState;

namespace OOP_Cursach_Sakhno.ui.mainScreen
{
    public delegate void State(ScreenState state);
    public class ViewModel
    {
        private DataBaseRepository dbRepo;
        private static readonly object _locker = new object();

        const double comByPerson = 10.23;

        public ScreenState state;
        public event State stateChanged;

        public ViewModel()
        {
            dbRepo = new EntityCoreRepository( DatabaseContext.Current);
            state = new ScreenState();
        }
        async public void getFlats()
        {
            var flats = await Task.Run(() => { return dbRepo.getFlats(); });
            var habitants = await Task.Run(() => { return dbRepo.getHabitants(); });
            var habList = await Task.Run(() => { return dbRepo.getHabitantsList(); });
            List<FlatsView> flatView = new List<FlatsView>();
            foreach (Flat flat in flats)
            {
                var habIds = habList.Where(h => h.FlatId == flat.id);
                var listOfHab = habitants.Where(h => habIds.Any(habId => habId.HabitantId == h.Id)).ToList();
                var commNeedToPay = listOfHab.Count * comByPerson;
                flatView.Add(new FlatsView(flat.id, flat.commPaid, commNeedToPay, flat.number, listOfHab));
            }
            updateState(new ScreenState(flatView,flats, habitants.Count, state?.idSelectedFlat, state?.idSelectedHabitant));
        }

        public void selectFlat(int FlatId)
        {
            updateState(new ScreenState(state.flats,state.flatDb, state.numberOfHabitants,FlatId,state.idSelectedHabitant));
        }
        public void selectHab(int HabId)
        {
            updateState(new ScreenState(state.flats, state.flatDb, state.numberOfHabitants, state.idSelectedFlat, HabId));
        }

        public void changePaid(double paid)
        {
            var selectedFlat = state.flatDb.FirstOrDefault((it) => { return it?.id == state.idSelectedFlat; }, null);
            selectedFlat.commPaid = paid;
            dbRepo.editFlat(selectedFlat);
            getFlats();
        }

        public void changeHab(string name,string surname,string phoneNumber)
        {
            if (state.idSelectedHabitant != null) {
                dbRepo.editHabitant(name,surname,phoneNumber,(int)state.idSelectedHabitant);
                getFlats();
            }
        }
        
        public void delFromFlat()
        {
            if (state?.idSelectedHabitant != null && state.idSelectedFlat != null)
            { 
                dbRepo.deleteHabitantFromFlat((int)state.idSelectedHabitant, (int)state.idSelectedFlat);
                updateState(new ScreenState(state.flats, state.flatDb, state.numberOfHabitants, state.idSelectedFlat, null));

                getFlats();
            }
        }
        public void delFromHouse()
        {
            if (state?.idSelectedHabitant != null)
            {
                dbRepo.deleteHabitant((int)state.idSelectedHabitant);
                updateState(new ScreenState(state.flats, state.flatDb, state.numberOfHabitants, state.idSelectedFlat, null));
                getFlats();
            }
        }

        private void updateState(ScreenState _state)
        {
            lock (_locker)
            {
                state = _state;
                stateChanged?.Invoke(_state);
            }
        }
    }

    public class ScreenState
    {
        public ScreenState(List<FlatsView> flatVeiw,List<Flat> flats, int numberOfHabitants, int? idSelectedFlat, int? idSelectedHabitant)
        {
            this.flats = flatVeiw;
            flatDb = flats;
            this.numberOfHabitants = numberOfHabitants;
            this.idSelectedFlat = idSelectedFlat;
            this.idSelectedHabitant = idSelectedHabitant;
        }
        public ScreenState()
        {
            flats = new List<FlatsView>();
            idSelectedFlat = 1;
            idSelectedHabitant = 0;
        }
        public List<FlatsView> flats { get; set; }
        public int? idSelectedFlat { get; set; }
        public int numberOfHabitants { get; set; }
        public int? idSelectedHabitant { get; set; }

        public List<Flat> flatDb {  get; set; }
        public class FlatsView
        {
            public int id { get; set; }
            public double commPaid { get; set; }
            public double commNeedToPay { get; set; }
            public int number { get; set; }
            public List<Habitant> habitants { get; set; }
            public FlatsView(int _id, double _commPaid, double _commNeedToPay, int _number, List<Habitant> _habitants)
            {
                id = _id;
                commNeedToPay = _commNeedToPay;
                commPaid = _commPaid;
                number = _number;
                habitants = _habitants;
            }
        }
    }
}
