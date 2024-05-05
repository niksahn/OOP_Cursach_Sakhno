using OOP_Cursach_Sakhno.data.models;
using OOP_Cursach_Sakhno.ui.mainScreen;
using OOP_Cursach_Sakhno.utils;
using System.Windows.Forms;

namespace OOP_Cursach_Sakhno.ui
{
    public partial class Main : NavigatableForm 
    {
        private ViewModel viewModel;

        public Main(Navigator navigator) : base(navigator)
        {
            InitializeComponent();
            viewModel = new ViewModel();
            viewModel.stateChanged += showState;
            viewModel.getFlats();
            updateView += (ob) => { viewModel.getFlats(); };
        }

        private void button3_Click(object sender, EventArgs e)
        {
            viewModel.changePaid((double) numericUpDown1.Value);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (viewModel?.state?.idSelectedFlat != null)
            {
                navigator.sendEvent(NavScreen.AddHabitant, viewModel.state.idSelectedFlat);
                navigator.navigate(NavScreen.AddHabitant);
            }
            else
            {
                MessageBox.Show(
                   "Выберите кватриру",
                   "Выберите кватриру"
               );
            }
        }

        private void showState(ScreenState state)
        {
            var selectedFlat = state.flats.FirstOrDefault((it) => { return it?.id == state.idSelectedFlat; }, null);
            var selectedhab = selectedFlat?.habitants.FirstOrDefault((it) => { return it?.Id == state.idSelectedHabitant; }, null);
            textBox4.Text = state.numberOfHabitants.ToString();
            textBox5.Text = state.flats?.Count.ToString();
            comboBox1.Items.Clear();
            foreach(var i in state.flats)
            {
                comboBox1.Items.Add(i.number);
            }
            listBox1.Items.Clear();
            textBox7.Text = selectedFlat?.habitants.Count.ToString();
            if(selectedFlat?.commPaid!=null) numericUpDown1.Value = (decimal)selectedFlat.commPaid;
            textBox8.Text = selectedFlat?.commNeedToPay.ToString();

            var habs = selectedFlat?.habitants;
            if(habs != null)
            {
                foreach (var i in habs)
                {
                    listBox1.Items.Add(i.Id+" "+i.Name + " " + i.SurName);
                }
            }
            comboBox1.Text = selectedFlat?.number.ToString();
            if (selectedhab != null)
            {
                textBox1.Text = selectedhab.Name;
                textBox2.Text = selectedhab.SurName;
                textBox3.Text = selectedhab.PhoneNumber;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ind = comboBox1.SelectedItem;
            if (ind != null)
            {
                var selectedFlat = (int)ind;
                if (selectedFlat != viewModel?.state?.idSelectedFlat)
                {
                    viewModel?.selectFlat(selectedFlat);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1?.SelectedItem != null)
            {
                var ind = Int32.Parse(listBox1?.SelectedItem?.ToString().Split()[0]);
                var selectedHab = ind;
                if (selectedHab != viewModel?.state?.idSelectedHabitant)
                {
                    viewModel?.selectHab(selectedHab);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Patterns.isString(textBox1.Text)&& Patterns.isString(textBox2.Text ) && Patterns.isTelNumber(textBox3.Text) )
            {
                viewModel.changeHab(textBox1.Text, textBox2.Text, textBox3.Text);
            }
            else
            {
                MessageBox.Show(
                    "Заполните все поля корректно",
                    "Поля некорректно заполнены"
                );
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            viewModel.delFromFlat();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            viewModel.delFromHouse();
        }
    }
}