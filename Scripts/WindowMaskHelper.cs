using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Financeiro.Scripts {

    public class WindowMaskHelper {

        public WindowMaskHelper () {

        }

        ~WindowMaskHelper () {

        }

        MainWindow MainWindow { get => (MainWindow)App.Current.MainWindow; }
        Grid MyGrid { get => MainWindow.MyGrid; }
        UIElementCollection Children { get => MyGrid.Children; }
        List<FrameworkElement> AddedChildren = new List<FrameworkElement>();
        List<FrameworkElement> PreviousUserControls = new List<FrameworkElement>();
        Rectangle Mask;

        public void CreateMask () {
            if (Mask != null) {
                Remove(Mask);
            }
            Mask = new Rectangle() {
                //Width = MainWindow.ActualWidth,
                //Height = MainWindow.ActualHeight,
                Fill = Brushes.Black,
                Opacity = 0.2
            };
            Add(Mask);
        }

        internal void Previous() {
            var last = AddedChildren.Last();
            Remove(last);
            var current = PreviousUserControls.Last();
            //Remove(current);
            SetView(current);
        }

        public void ClearMask () {
            while (AddedChildren.Count > 0) {
                Remove(AddedChildren[0]);
            }
            PreviousUserControls.Clear();
            Mask = null;
        }

        public void SetView (FrameworkElement userControl) {
            userControl.HorizontalAlignment = HorizontalAlignment.Center;
            userControl.VerticalAlignment = VerticalAlignment.Center;
            userControl.Margin = new Thickness(10);
            var t = userControl.GetType();
            var wmh = t.GetProperties().FirstOrDefault(x => x.PropertyType == typeof(WindowMaskHelper));
            if (wmh != default) {
                wmh.SetValue(userControl, this);
            }
            Add(userControl);
        }

        internal void Next(FrameworkElement userControl) {
            PreviousUserControls.Add(AddedChildren.Last());
            Remove(AddedChildren.Last());
            SetView(userControl);
        }

        void Add(FrameworkElement element) {
            AddedChildren.Add(element);
            Children.Add(element);
        }

        void Remove(FrameworkElement element) {
            AddedChildren.Remove(element);
            Children.Remove(element);
        }

    }
}
