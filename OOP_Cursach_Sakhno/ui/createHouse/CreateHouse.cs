using OOP_Cursach_Sakhno.data.database;
using OOP_Cursach_Sakhno.data.models;
using OOP_Cursach_Sakhno.data.repository;
using OOP_Cursach_Sakhno.domain.repository;
using System.ComponentModel;

namespace OOP_Cursach_Sakhno.ui.createHouse
{
    [Designer(typeof(System.Windows.Forms.Design.WindowsFormsComponentEditor))]
    public partial class CreateHouse : NavigatableForm
    {
        public CreateHouse(Navigator navigator)  : base(navigator)
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            DataBaseRepository db = new EntityCoreRepository(DatabaseContext.Current);
            for(int i=1;i<numericUpDown1.Value+1; i++)
            {
                await Task.Run(() => { db.addFlat(new Flat(0.0, i)); });
            }
            navigator.navigate(NavScreen.Main);
        }
    }
}
