using OOP_Cursach_Sakhno.data.database;
using OOP_Cursach_Sakhno.data.models;
using OOP_Cursach_Sakhno.data.repository;
using OOP_Cursach_Sakhno.domain.repository;

namespace OOP_Cursach_Sakhno.ui.search
{
    public delegate void State(ScreenState state);
    internal class SearchViewModel
    {
        public ScreenState state;
        public event State stateChanged;
        private DataBaseRepository dbRepo;
        private static readonly object _locker = new object();

        public SearchViewModel() {

            dbRepo = new EntityCoreRepository(DatabaseContext.Current);
            state = new ScreenState();
        }
        public void find(String s)
        {
            var hab = dbRepo.findHabitants(s);
            updateState(state.copy(rezult: hab));
        }

        public async void select(int id)
        {
            await Task.Run(() =>
            {
               var flats = dbRepo.getFlatsByHab(id).Select(f=> f.number.ToString()).ToList();
               updateState(state.copy(selected_id: id, flatsOfSelected: flats));
            });
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
        public List<Habitant> rezults;
        public List<String> flatsOfSelected;
        public int? selected_id;
        public ScreenState()
        {
            rezults = new List<Habitant>();
            selected_id = null;
            flatsOfSelected= new List<String>();
        }
        public ScreenState(List<Habitant> rezult, int? selected_id, List<string> flatsOfSelected)
        {
            this.rezults = rezult;
            this.selected_id = selected_id;
            this.flatsOfSelected = flatsOfSelected;
        }
        public ScreenState copy(List<Habitant>? rezult = null, int? selected_id = null, List<string>? flatsOfSelected = null)
        {
            return new ScreenState(
                rezult: rezult == null ? this.rezults : rezult,
                selected_id: selected_id == null ? this.selected_id : selected_id,
                flatsOfSelected: flatsOfSelected == null ? this.flatsOfSelected : flatsOfSelected
                );
        }
    }
}
