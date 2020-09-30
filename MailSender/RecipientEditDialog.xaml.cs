using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfMailSender
{
    /// <summary>
    /// Логика взаимодействия для RecipientEditDialog.xaml
    /// </summary>
    public partial class RecipientEditDialog : Window
    {
        public RecipientEditDialog()
        {
            InitializeComponent();
        }

        private void OnPortTextInput(object Recipient, TextCompositionEventArgs E)
        {
            // Если источник события - не текстовое поле ввода
            // или текст в поле ввода отсутствует, то...
            // ничего не делаем
            if (!(Recipient is TextBox text_box) || text_box.Text == "") return;
            // иначе если не удалось превратить текст в число, то
            // отмечаем событие как обработанное - текст не введётся
            E.Handled = !int.TryParse(text_box.Text, out _);//тест
        }
        /// <summary>
        /// Обработчик события кнопки
        /// Если кнопка IsCancel == true, то результатом диалога будет false
        /// </summary>
        private void OnButtonClick(object Recipient, RoutedEventArgs E)
        {
            DialogResult = !((Button)E.OriginalSource).IsCancel;
            Close();
        }
        // Добавляем статические методы для удобства работы с диалогом
        /// <summary>
        /// Метод, позволяющий отобразить диалог для редактирования данных
        /// Редактируемые параметры передаются по ссылке
        /// Если пользователь выбрал Ok, то метод возвращает true
        /// Если пользователь выбрал Cancel, то параметры не меняются.
        /// </summary>
        public static bool ShowDialog(
            string Title, ref string Name,
            ref string Address, ref int Id,
            ref string Description)
        {
            // Создаём окно и инициализируем его свойства
            var window = new RecipientEditDialog
            {
                Title = Title,
                // Так можно инициализировать свойства вложенных объектов
                RecipientName = { Text = Name },
                RecipientAddress = { Text = Address },
                RecipientId = { Text = Id.ToString() },
                RecipientDescription = { Text = Description },
                // Берём класс "Приложение"
                Owner = Application
                    // получаем экземпляр текущего приложения
                    .Current
                    // берём все окна приложения
                    .Windows
                    // пеерводим их из интерфейса IEnumerable в IEnumerable<Window>
                    .Cast<Window>()
                    // находим первое окно, у которого свойство IsActive == true
                    .FirstOrDefault(window => window.IsActive)
            };
            if (window.ShowDialog() != true) return false;
            Name = window.RecipientName.Text;
            Address = window.RecipientAddress.Text;
            Id = int.Parse(window.RecipientId.Text);
            Description = window.RecipientDescription.Text;
            return true;
        }
        /// <summary>
        /// Метод, позволяющий отобразить диалог создания нового сервера
        /// Редактируемые параметры передаются по ссылке
        /// Если пользователь выбрал Ok, то метод возвращает true
        /// Если пользователь выбрал Cancel, то возвращаются дефолтные значения
        /// </summary>
        public static bool Create(
            out string Name,
            out string Address,
            out int Id,
            out string Description)
        {
            // Инициализируем переменные значениями на случай отмены операции
            Name = null;
            Address = null;
            Id = 0;
            Description = null;
            return ShowDialog("Создать сервер",
                ref Name,
                ref Address,
                ref Id,
                ref Description);
        }
    }
}
