// using System.Windows;
// using System.Windows.Controls;
//
// namespace Annuaire.Helpers{
//     public class PasswordBoxAssistant
//     {
//         public static readonly DependencyProperty BoundPasswordProperty =
//             DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(),
//                 new PropertyMetadata(string.Empty, OnBoundPasswordChanged));
//
//         public static string GetBoundPassword(DependencyObject obj)
//         {
//             return (string)obj.GetValue(BoundPasswordProperty);
//         }
//
//         public static void SetBoundPassword(DependencyObject obj, string value)
//         {
//             obj.SetValue(BoundPasswordProperty, value);
//         }
//
//         private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
//         {
//             if (d is PasswordBox passwordBox)
//             {
//                 passwordBox.PasswordChanged -= PasswordChanged;
//                 passwordBox.Password = e.NewValue?.ToString() ?? string.Empty;
//                 passwordBox.PasswordChanged += PasswordChanged;
//             }
//         }
//
//         private static void PasswordChanged(object sender, RoutedEventArgs e)
//         {
//             if (sender is PasswordBox passwordBox)
//             {
//                 SetBoundPassword(passwordBox, passwordBox.Password);
//             }
//         }
//     }
// }