using System.Windows.Input;
using Xamarin.Forms;

namespace BlueMonkey.Views.Behaviors
{
    /// <summary>
    /// Behavior that executes Command when Item is selected in ListView.
    /// </summary>
    public class ItemSelectedToCommandBehavior : BehaviorBase<ListView>
    {
        /// <summary>
        /// BindableProperty for Command.
        /// </summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(EventToCommandBehavior), null);

        /// <summary>
        /// Command to be executed
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Attached to ListView.
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.ItemSelected += Bindable_ItemSelected;
        }

        /// <summary>
        /// Detaching from ListView.
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.ItemSelected -= Bindable_ItemSelected;
        }

        /// <summary>
        /// ItemSelected event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bindable_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                if (Command.CanExecute(e.SelectedItem))
                {
                    Command.Execute(e.SelectedItem);
                }
                AssociatedObject.SelectedItem = null;
            }
        }
    }
}
