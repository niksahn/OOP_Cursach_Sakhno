using OOP_Cursach_Sakhno.data.database;
using OOP_Cursach_Sakhno.data.models;
using OOP_Cursach_Sakhno.data.repository;
using OOP_Cursach_Sakhno.domain.repository;
using OOP_Cursach_Sakhno.utils;

namespace OOP_Cursach_Sakhno.ui
{
    public partial class AddHabitant : NavigatableForm
    {
        DataBaseRepository db ;
        int selected = -1;
        public AddHabitant(Navigator navigator) : base(navigator)
        {
            InitializeComponent();
            db = new EntityCoreRepository(DatabaseContext.Current);
            addFlats();
            updateView += (ob) => {
                selected = (int)ob;
            };
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            navigator.pop();
        }

        private async void addFlats()
        {
            var flats = await Task.Run(() => { return db.getFlats(); });
            flats.ForEach(flat =>
            {
                checkedListBox1.Items.Add(flat.number);
            });
            if(selected!=-1) checkedListBox1.SetItemChecked(selected - 1, true);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (Patterns.isString(textBox1.Text) && Patterns.isString(textBox2.Text) && Patterns.isTelNumber(textBox3.Text) && checkedListBox1.CheckedItems.Count != 0 ) {
                var habitant = new Habitant(textBox1.Text, textBox2.Text, textBox3.Text);
                var id = await Task.Run(() => { return db.addHabitant(habitant); });
                foreach(var i in checkedListBox1.CheckedItems)
                {
                    await Task.Run(() => { db.addHabitantToFlat(id, (int)i); });
                }
                navigator.pop();
                navigator.sendEvent(NavScreen.Main,null);
            } else {
                MessageBox.Show(
                    "Заполните все поля корректно",
                    "Поля некорректно заполнены"
                );
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            navigator.pop();
        }
    }
}
