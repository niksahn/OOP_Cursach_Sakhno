using OOP_Cursach_Sakhno.data.database;
using OOP_Cursach_Sakhno.data.models;
using OOP_Cursach_Sakhno.data.repository;
using OOP_Cursach_Sakhno.domain.repository;
using OOP_Cursach_Sakhno.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Cursach_Sakhno.ui
{
    public partial class AddHabitant : NavigatableForm
    {
        DataBaseRepository db ;
        public AddHabitant(Navigator navigator) : base(navigator)
        {
            InitializeComponent();
            db = new EntityCoreRepository(DatabaseContext.Current);
            addFlats();
        }

        private async void addFlats()
        {
            var flats = await Task.Run(() => { return db.getFlats(); });
            flats.ForEach(flat =>
            {
                checkedListBox1.Items.Add(flat.number);
            });
            updateView += (ob) => {
                checkedListBox1.SetItemChecked((int)ob, true);
            };
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (Patterns.isString(textBox1.Text) && Patterns.isString(textBox2.Text) && Patterns.isTelNumber(textBox3.Text) && checkedListBox1.CheckedItems.Count != 0 ) {
                var habitant = new Habitant(textBox1.Text, textBox2.Text, textBox3.Text);
                var id = await Task.Run(() => { return db.addHabitant(habitant); });
                foreach(var i in checkedListBox1.CheckedItems)
                {
                  await  db.addHabitantToFlat(id,(int) i);
                }
                navigator.pop();
                navigator.sendEvent(NavScreen.Main,null);
            } else {
                MessageBox.Show(
                    "Заполните все поля",
                    "Поля не заполнены"
                );
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            navigator.pop();
        }
    }
}
