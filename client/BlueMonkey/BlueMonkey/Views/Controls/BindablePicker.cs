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

        public static readonly BindableProperty SelectedItemsProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(string), typeof(BindablePicker), null, propertyChanged: OnSelectedItemsChanged);

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public string SelectedItem
        {
            get { return (string)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        public BindablePicker()
        {
            SelectedIndexChanged += (sender, args) =>
            {
                SelectedItem = Items[SelectedIndex];
            };
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

        private static void OnSelectedItemsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var picker = bindable as BindablePicker;
            picker.SelectedIndex = picker.Items.IndexOf((string)newValue);
        }
    }
}
