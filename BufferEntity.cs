using System;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
namespace EntityWindowsTool
{
    public class BufferEntity
    {
        private static string Text1 = "Словесное описание алгоритма\r\n1.\tПользователь заходит в систему\r\n2.\tАвторизация\r\n2.1.\tАвторизация в роли менеджера\r\n2.2.\tАвторизация в роли покупателя\r\n2.3.\tАвторизация в роли руководства\r\n2.4.\tАвторизация в роли физ. лица\r\n2.5.\tАвторизация в роли кладовщика\r\n3.\tОкно менеджера\r\n3.1.\tМенеджер просматривает информацию о менеджерах\r\n3.2.\tМенеджер просматривает информацию о пользователях\r\n3.3.\tПросматривать и изменять информацию своего профиля\r\n3.4.\tАдминистрировать заказы\r\n4.\tОкно покупателя\r\n4.1.\tПросмотр заказов\r\n4.2.\tПросматривать и изменять информацию своего профиля\r\n4.3.\tОставлять заказы на оптовую или розничную покупку\r\n5.\tВыход из системы\r\n\r\n\r\n";

        public void GetText1()
        {
            string textToCopy = Text1;

            if (!string.IsNullOrWhiteSpace(textToCopy))
            {
                try
                {
                    System.Windows.Forms.Clipboard.SetDataObject(textToCopy, true);
                    Console.WriteLine("Текст скопирован в буфер обмена!", "Успех");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Не удалось скопировать текст. Ошибка: {ex.Message}", "Ошибка");
                }
            }
            else
            {
                Console.WriteLine("Пустой текст нельзя скопировать.", "Ошибка");
            }
        }
    }
}
