using OOP_Cursach_Sakhno.data.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Cursach_Sakhno.ui.mainScreen
{
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
