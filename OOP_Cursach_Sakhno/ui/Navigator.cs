using OOP_Cursach_Sakhno.ui.createHouse;
using OOP_Cursach_Sakhno.ui.search;

namespace OOP_Cursach_Sakhno.ui
{
    public class Navigator
    {
        private Dictionary<NavScreen,NavigatableForm> stack = new Dictionary<NavScreen, NavigatableForm>();
        public void navigate(NavScreen screen)
        {
            if (stack.Count == 0)
            {
                var form = toForm(screen);
                stack.Add(screen,form);
                Application.Run(form);
            }
           else if (stack.Keys.Contains(screen)) 
           {
                var form = stack[screen];
                closeLastForm();
                stack.Remove(screen);
                stack.Add(screen,form);
                form.Show();
           }
            else
            {
                var form = toForm(screen);
                form?.Show();
                closeLastForm();
                stack.Add(screen,form);
            }
        }
        private void closeLastForm()
        {
            if (stack.Count == 1)
            {
                stack.Last().Value.Hide();
            }
            else stack.Last().Value.Hide();
        }
        public void pop()
        {
            stack.Last().Value.Hide();
            stack.Remove(stack.Last().Key);
            if(stack.Count == 0) Application.Exit();
            else stack.Last().Value.Show();
        }

        public void sendEvent(NavScreen scr, object ev)
        {
            if (!stack.ContainsKey(scr))
            {
                stack.Add(scr, toForm(scr));
            }
            stack[scr].sendEvent(ev);
        }
        private NavigatableForm? toForm(NavScreen scr) { 
            switch(scr)
            {
                case NavScreen.Main:
                    return new Main(this);
                case NavScreen.Info:
                    return new Info(this);
                case NavScreen.CreateHouse:
                    return new CreateHouse(this);
                case NavScreen.AddHabitant:
                    return new AddHabitant(this);
                case NavScreen.SerachRez:
                    return new Search(this);
                default:
                    return null; 
            }
        }
    };

   public class NavigatableForm : Form {
        protected delegate void Event(object ev);
        protected Navigator navigator;
        protected event Event? updateView;
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
           // navigator.pop();
           Application.Exit();
        }
        public void sendEvent(object ev)
        {
            updateView?.Invoke(ev);
        }
        public NavigatableForm() { }
        protected NavigatableForm(Navigator _navigator) => navigator = _navigator;
    }
    public enum NavScreen { Main, Info, AddHabitant, CreateHouse, SerachRez }
}
