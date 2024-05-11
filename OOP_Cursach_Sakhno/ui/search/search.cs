using System.Windows.Forms;

namespace OOP_Cursach_Sakhno.ui.search
{
    public partial class Search : NavigatableForm
    {
        private SearchViewModel viewModel;
        public Search(Navigator navigator) : base(navigator)
        {
            InitializeComponent();
            viewModel = new SearchViewModel();
            updateView += (ob) => {
                 viewModel.find(ob.ToString());
            };
            viewModel.stateChanged += (state) =>
            {
                showState(state);
            };
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            navigator.pop();
        }

        private void showState(ScreenState s)
        {
            if (s.rezults != null)
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                var selectedhab = s.rezults.FirstOrDefault((it) => { return it.Id == s.selected_id; }, null);
                foreach (var i in s.rezults)
                {
                    listBox1.Items.Add(i.Id + " " + i.Name + " " + i.SurName);
                }
                if (selectedhab != null)
                {
                    textBox1.Text = selectedhab.Name;
                    textBox2.Text = selectedhab.SurName;
                    textBox3.Text = selectedhab.PhoneNumber;
                }
                if (s.flatsOfSelected != null)
                {
                    foreach (var i in s.flatsOfSelected)
                    {
                        listBox2.Items.Add(i);
                    }
                }
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1?.SelectedItem!=null)viewModel.select(Int32.Parse(listBox1?.SelectedItem?.ToString().Split()[0]));
        }
    }
}
