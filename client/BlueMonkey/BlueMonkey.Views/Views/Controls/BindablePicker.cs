using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BlueMonkey.Views.Controls
{
    public class BindablePicker : Picker
    {
        public static readonly BindableProperty ItemsSourceProperty = 
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(BindablePicker), null, propertyChanged: OnItemsSourceChanged);

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var picker = bindable as BindablePicker;
            picker.Items.Clear();
            var items = newValue as IEnumerable;
            if (items != null)
            {
                foreach (var item in items)
                {
                    picker.Items.Add(item.ToString());
                }
            }
        }
    }
}
