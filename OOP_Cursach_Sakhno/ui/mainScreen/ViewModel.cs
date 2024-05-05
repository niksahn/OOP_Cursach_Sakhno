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

        static private ScreenState? state;
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
            updateState(new ScreenState(flatView, habitants.Count, state?.idSelectedFlat, state?.idSelectedHabitant));
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
        public ScreenState(List<FlatsView> flat, int numberOfHabitants, int? idSelectedFlat, int? idSelectedHabitant)
        {
            flats = flat;
            this.numberOfHabitants = numberOfHabitants;
            this.idSelectedFlat = idSelectedFlat;
            this.idSelectedHabitant = idSelectedHabitant;
        }
        public ScreenState()
        {
            flats = new List<FlatsView>();
            idSelectedFlat = 0;
            idSelectedHabitant = 0;
        }
        public List<FlatsView> flats { get; set; }

        public int? idSelectedFlat { get; set; }
        public int numberOfHabitants { get; set; }
        public int? idSelectedHabitant { get; set; }

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
