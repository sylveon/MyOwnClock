﻿using System.Windows;
using System.Windows.Media;

namespace MyOwnClock.Extensions
{
    public class CustomProperties
    {
        public static readonly DependencyProperty MouseOverColorProperty = DependencyProperty.RegisterAttached("MouseOverColor", typeof(Brush), typeof(CustomProperties), new PropertyMetadata(default(Brush)));
        public static void SetMouseOverColor(UIElement element, Brush value) => element.SetValue(MouseOverColorProperty, value);
        public static Brush GetMouseOverColor(UIElement element) => (Brush)element.GetValue(MouseOverColorProperty);

        public static readonly DependencyProperty PressedColorProperty = DependencyProperty.RegisterAttached("PressedColor", typeof(Brush), typeof(CustomProperties), new PropertyMetadata(default(Brush)));
        public static void SetPressedColor(UIElement element, Brush value) => element.SetValue(PressedColorProperty, value);
        public static Brush GetPressedColor(UIElement element) => (Brush)element.GetValue(PressedColorProperty);
    }
}