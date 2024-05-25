using OOP_Cursach_Sakhno.data.database;
using OOP_Cursach_Sakhno.data.repository;
using OOP_Cursach_Sakhno.domain.repository;
using System.ComponentModel;

namespace OOP_Cursach_Sakhno.ui
{
    [Designer(typeof(System.Windows.Forms.Design.WindowsFormsComponentEditor))]
    public partial class Info : NavigatableForm
    {
        DataBaseRepository db = new EntityCoreRepository(DatabaseContext.Current);

        public Info(Navigator navigator):base(navigator) 
        {
            InitializeComponent();
            Wait();
        }

        public async void Wait()
        {
            await Task.Delay(1000);
            if (db.getFlats().Count != 0) { navigator.navigate(NavScreen.Main); }
            else { navigator.navigate(NavScreen.CreateHouse); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (db.getFlats().Count != 0 ) { navigator.navigate(NavScreen.Main); }
            else { navigator.navigate(NavScreen.CreateHouse); }
        }
    }
}
