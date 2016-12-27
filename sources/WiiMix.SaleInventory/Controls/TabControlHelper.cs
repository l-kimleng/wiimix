using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MahApps.Metro.Controls;

namespace WiiMix.SaleInventory.Controls
{
    public static class TabControlHelper
    {
        public static readonly DependencyProperty HeaderPanelBackgroundBrushProperty = DependencyProperty.RegisterAttached("HeaderPanelBackgroundBrush", typeof(Brush), typeof(TabControlHelper), new FrameworkPropertyMetadata(Brushes.Transparent, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
        /// <summary>
        /// Sets the brush used to draw the mouse over brush.
        /// </summary>
        public static void SetHeaderPanelBackgroundBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(HeaderPanelBackgroundBrushProperty, value);
        }

        /// <summary>
        /// Gets the brush used to draw the mouse over brush.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof(TabItem))]
        public static Brush GetHeaderPanelBackgroundBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(HeaderPanelBackgroundBrushProperty);
        }
    }
}
