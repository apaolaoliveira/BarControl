using System.Collections;

namespace BarControl.Shared
{
    public abstract class PresentationBase
    {
        protected RepositoryBase repository = null;
        protected EntityBase entity = null;

        public string entityName;
        protected abstract void DisplayTable();
        protected abstract EntityBase GetRecordProperties();

        // Get user input -----------------------------------------------------------------

        protected int GetInputId()
        {
            int id = 0;
            try
            {
                ColorfulMessage("\nEnter the ID: \n\n-> ", ConsoleColor.Cyan);
                id = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                ColorfulMessage("\nThis is not a validy ID!\n", ConsoleColor.Red);
                SetFooter();
                GetInputId();
                return 0;
            }
            return id;
        }

        // CRUD ---------------------------------------------------------------------------

        public virtual void Create()
        {
            Console.Clear();

            SetHeader($"create new {entityName}");

            EntityBase entity = GetRecordProperties();

            ArrayList errorsList = entity.Errors();

            if (errorsList.Count > 0)
            {
                foreach (string error in errorsList)
                {
                    ColorfulMessage(error, ConsoleColor.Red);
                    SetFooter();
                }

                Create();
                return;
            }

            repository.Add(entity);
            ColorfulMessage($"\n{entityName} suscessfully created!", ConsoleColor.Green);

            SetFooter();
        }

        public virtual void Read()
        {
            Console.Clear();

            SetHeader($"view {entityName}'s table");

            if (repository.NoRecords())
                return;

            DisplayTable();
        }

        public virtual void Update()
        {
            Console.Clear();

            SetHeader($"update a {entityName}");

            if (repository.NoRecords())
                return;

            DisplayTable();

            int getId = GetInputId();
            int validId = repository.isValidId(getId);

            EntityBase newRecord = GetRecordProperties();
            repository.UpdateData(validId, newRecord);
            ColorfulMessage($"\n{entityName} suscessfully updated!", ConsoleColor.Green);
            SetFooter();
        }

        public virtual void Delete()
        {
            Console.Clear();

            SetHeader($"Delete a {entityName}");

            if (repository.NoRecords())
                return;

            DisplayTable();

            int getId = GetInputId();
            int validId = repository.isValidId(getId);

            repository.Remove(validId);
            ColorfulMessage($"\n{entityName} suscessfully deleted!", ConsoleColor.Green);
            SetFooter();
        }

        // Presentation facilities --------------------------------------------------------

        public static void ColorfulMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }

        public static void SetHeader(string header)
        {
            Console.Clear();

            ColorfulMessage(
              $"\n\n{header.ToUpper()}"
            + "\n------------------------------\n"
            , ConsoleColor.Cyan);
        }

        public static void SetFooter()
        {
            ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
            Console.ReadLine();
        }

        public static int SetMenu(string entity)
        {
            Console.Clear();

            ColorfulMessage(
              $"\n\n{entity.ToUpper()}"
            + $"\n-------------------"
            + $"\n[1] Create {entity}."
            + $"\n[2] View {entity}'s table."
            + $"\n[3] Edit a {entity}."
            + $"\n[4] Delete a {entity}."
            + $"\n[5] Go back."
            + "\n\n→ "
            , ConsoleColor.DarkYellow);

            int selectedOption = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            return selectedOption;
        }

        public static void SetTable(string[] columnNames, int[] columnWidths, List<object> data)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            // Print - separator
            const int pipeCount = 2;
            const char SeparatorChar = '-';
            int totalWidth = columnWidths.Sum() + columnNames.Length * pipeCount - 1;
            string separator = new string(SeparatorChar, totalWidth);

            Console.WriteLine($"\n {separator}");

            // Print header
            string header = "";
            for (int i = 0; i < columnNames.Length; i++)
            {
                header += String.Format("| {0,-" + columnWidths[i] + "}", columnNames[i].ToUpper());
            }
            Console.WriteLine($"{header}|\n {separator}");

            Console.ResetColor();

            // Print the each row from 'data'
            foreach (object[] row in data)
            {
                string rowString = "";
                for (int i = 0; i < row.Length; i++)
                {
                    rowString += String.Format("| {0,-" + columnWidths[i] + "}", row[i]);
                }
                Console.WriteLine(rowString + "|");
            }
            Console.WriteLine($" {separator}");

            /*
                Exemple of use:
                string[] columnNames = { "id", "name", "phone", "address" };
                int[] columnWidths = { 4, 15, 15, 30 };
                List<object> data = new List<object>();

                SetTable(columnNames, columnWidths, data);
             */
        }

        public static Type SetField<Type>(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write($"\n{message}\n→ ");
            Console.ResetColor();

            string input = Console.ReadLine();

            try
            {
                Type result = (Type)Convert.ChangeType(input, typeof(Type));
                return result;
            }
            catch (FormatException)
            {
                ColorfulMessage($"\nInvalid input. Please enter a valid {typeof(Type).Name}.\n", ConsoleColor.Red);
                return SetField<Type>(message, color);
            }
        }
    }
}

