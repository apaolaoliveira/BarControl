
namespace BarControl.Application
{
    public class Notifier
    {
        private void ColorfulMessage(string message, TypeMessage typeMessage)
        {
            switch (typeMessage)
            {
                case TypeMessage.Menu:    Console.ForegroundColor = ConsoleColor.DarkYellow; break;
                case TypeMessage.Text:    Console.ForegroundColor = ConsoleColor.Cyan;       break;
                case TypeMessage.Error:   Console.ForegroundColor = ConsoleColor.Red;        break;
                case TypeMessage.Success: Console.ForegroundColor = ConsoleColor.Green;      break;
            }

            Console.Write(message);
            Console.ResetColor();
        }

        private enum TypeMessage
        {
            Menu, Text, Error, Success
        }

        public void Menu(string message)
        {
            ColorfulMessage(message, TypeMessage.Menu);
        }

        public void Text(string message)
        {
            ColorfulMessage(message, TypeMessage.Text);
        }

        public void Error(string message)
        {
            ColorfulMessage(message, TypeMessage.Error);
        }

        public void Success(string message)
        {
            ColorfulMessage(message, TypeMessage.Success);
        }
    }
}