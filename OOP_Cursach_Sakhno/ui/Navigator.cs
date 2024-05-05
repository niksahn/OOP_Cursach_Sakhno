using Microsoft.Extensions.Logging;
using OOP_Cursach_Sakhno.ui.createHouse;
using System.Windows.Forms;

namespace OOP_Cursach_Sakhno.ui
{
    public class Navigator
    {
        private List<NavigatableForm> stack = new List<NavigatableForm>();
        private List<NavScreen> stackNames = new List<NavScreen>();
        public void navigate(NavScreen screen)
        {
            if (stack.Count == 0)
            {
                var form = toForm(screen);
                stack.Add(form);
                stackNames.Add(screen);
                Application.Run(form);
            }
           else if (stackNames.Contains(screen)) 
           {
                var formClass = toFormType(screen);
                var form = stack.First((it) => { return it.GetType() == formClass; });
                closeLastForm();
                stack.Remove(form);
                stack.Add(form);
                stackNames.Remove(screen);
                stackNames.Add(screen);
                form.Show();
           }
            else
            {
                var form = toForm(screen);
                form.Show();
                closeLastForm();
                stack.Add(form);
                stackNames.Add(screen);
            }
        }
        private void closeLastForm()
        {
            if (stack.Count == 1)
            {
                stack.Last().Hide();
            }
            else stack.Last().Close();
        }
        public void pop()
        {
            stack.RemoveAt(stack.Count - 1);
            stackNames.RemoveAt(stack.Count - 1);
            if(stack.Count == 0) Application.Exit();
            else stack.Last().Show();
        }

        public void sendEvent(NavScreen scr)
        {
            var formClass = toFormType(scr);
            stack
                .First((it) => { return it.GetType() == formClass;})
                .sendEvent();
        }
        private NavigatableForm toForm(NavScreen scr) { 
            switch(scr)
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
        private Type toFormType(NavScreen scr)
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
    };

   public abstract class NavigatableForm : Form {
        protected delegate void Event();
        protected Navigator navigator;
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
        protected NavigatableForm(Navigator _navigator) => navigator = _navigator;
    }
    public enum NavScreen { Main, Info, AddHabitant, CreateHouse }
}
