using OOP_Cursach_Sakhno.data.database;
using OOP_Cursach_Sakhno.data.repository;
using OOP_Cursach_Sakhno.domain.repository;
using System.ComponentModel;

namespace OOP_Cursach_Sakhno.ui
{
    [Designer(typeof(System.Windows.Forms.Design.WindowsFormsComponentEditor))]
    public partial class Info : NavigatableForm<NavScreen>
    {
        public Info(Navigator<NavScreen> navigator):base(navigator) 
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataBaseRepository db = new EntityCoreRepository(DatabaseContext.Current);
            if (db.getFlats().Count != 0 ) { navigator.navigate(NavScreen.Main); }
            else { navigator.navigate(NavScreen.CreateHouse); }
        }
    }
}
