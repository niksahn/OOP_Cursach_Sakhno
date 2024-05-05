using OOP_Cursach_Sakhno.ui.mainScreen;
using System.ComponentModel;

namespace OOP_Cursach_Sakhno.ui
{
    [Designer(typeof(System.Windows.Forms.Design.WindowsFormsComponentEditor))]
    public partial class Main : NavigatableForm<NavScreen>
    {
        private ViewModel viewModel;

        public Main(Navigator<NavScreen> navigator) : base(navigator)
        {
            InitializeComponent();
            viewModel = new ViewModel();
            viewModel.stateChanged += showState;
            updateView+= ()=>{ viewModel.getFlats(); };
            viewModel.getFlats();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void showState(ScreenState state)
        {
            var selectedFlat = state.flats.FirstOrDefault((it) => { return it?.id == state.idSelectedFlat; }, null);
            textBox4.Text = state.numberOfHabitants.ToString();
            textBox5.Text = state.flats?.Count.ToString();
            comboBox1.Items.Clear();
            foreach(var i in state.flats)
            {
                comboBox1.Items.Add(i.number.ToString());
            }
            listBox1.Items.Clear();
            textBox7.Text = selectedFlat?.habitants.Count.ToString();
            textBox6.Text = selectedFlat?.commPaid.ToString();
            textBox8.Text = selectedFlat?.commNeedToPay.ToString();

            var habs = selectedFlat?.habitants;
            if(habs != null)
            {
                foreach (var i in habs)
                {
                    comboBox1.Items.Add(i.Name + " " + i.SurName);
                }
            }
        }
    }
}