using Microsoft.Extensions.Logging;
using OOP_Cursach_Sakhno.ui.createHouse;
using System.Windows.Forms;

namespace OOP_Cursach_Sakhno.ui
{
    public abstract class Navigator<NavScreen>
    {
        private Dictionary<NavScreen, NavigatableForm<NavScreen>> stack = new Dictionary<NavScreen,NavigatableForm<NavScreen>>();
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
                var formClass = toFormType(screen);
                var form = stack.First((it) => { return it.GetType() == formClass; }).Value;
                closeLastForm();
                stack.Remove(screen);
                stack.Add(screen,form);
                form.Show();
           }
            else
            {
                var form = toForm(screen);
                form.Show();
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
            else stack.Last().Value.Close();
        }
        public void pop()
        {
            stack.Remove(stack.Last().Key);
            if(stack.Count == 0) Application.Exit();
            else stack.Last().Value.Show();
        }

        public void sendEvent(NavScreen scr)
        {
            var formClass = toFormType(scr);
            stack
                .First((it) => { return it.GetType() == formClass;})
                .Value
                .sendEvent();
        }
        protected abstract NavigatableForm<NavScreen> toForm(NavScreen scr);
        protected abstract Type toFormType(NavScreen scr);
    };

    public class MineNavigator : Navigator<NavScreen>
    {
        protected override NavigatableForm<NavScreen> toForm(NavScreen scr)
        {
            switch (scr)
            {
                case NavScreen.Main:
                    return new Main(this);
                case NavScreen.Info:
                    return new Info(this);
                case NavScreen.CreateHouse:
                    return new CreateHouse(this);
                default:
                    return new Info(this);
            }
        }

        protected override Type toFormType(NavScreen scr)
        {
            switch (scr)
            {
                case NavScreen.Main:
                    return typeof(Main);
                case NavScreen.Info:
                    return typeof(Info);
                case NavScreen.CreateHouse:
                    return typeof(CreateHouse);
                default:
                    return typeof(Info);
            }
        }
    }

    public class NavigatableForm<NavScreen> : Form {
        protected delegate void Event();
        protected Navigator<NavScreen> navigator;
        protected event Event? updateView;
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
           // navigator.pop();
           Application.Exit();
        }
        public void sendEvent()
        {
            updateView?.Invoke();
        }
        public NavigatableForm() { }
        protected NavigatableForm(Navigator<NavScreen> _navigator) => navigator = _navigator;
    }
    public enum NavScreen { Main, Info, AddHabitant, CreateHouse }
}
