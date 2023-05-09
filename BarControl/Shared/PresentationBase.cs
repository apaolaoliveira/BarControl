
namespace BarControl.Shared
{
    public abstract class PresentationBase<RepositoryType, EntityType> : ICRUD
                          where RepositoryType : RepositoryBase<EntityType>
                          where EntityType : EntityBase<EntityType>
    {
        protected RepositoryType repository = null;
        protected EntityType entity = null;
        
        public Notifier notifier = new Notifier();

        public string entityName;

        protected abstract void DisplayTable();
        protected abstract EntityType GetRecordProperties();
        protected int GetInputId()
        {
            int id = 0;
            try
            {
                notifier.Text("\nEnter the ID: \n\n-> ");
                id = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                notifier.Error("\nThis is not a validy ID!\n");
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

            EntityType entity = GetRecordProperties();

            ArrayList errorsList = entity.Errors();

            if (errorsList.Count > 0)
            {
                foreach (string error in errorsList)
                {
                    notifier.Error(error);
                    SetFooter();
                }

                Create();
                return;
            }

            repository.Add(entity);
            notifier.Success($"\n{entityName} suscessfully created!");

            SetFooter();
        }

        public virtual void Read()
        {
            Console.Clear();

            SetHeader($"view {entityName}'s table");

            if (repository.NoRecords())
            {
                SetFooter();
                return;
            }

            DisplayTable();
        }

        public virtual void Update()
        {
            Console.Clear();

            SetHeader($"update a {entityName}");

            if (repository.NoRecords())
            {
                SetFooter();
                return;
            }

            DisplayTable();

            int getId = GetInputId();
            int validId = repository.isValidId(getId);

            EntityType newRecord = GetRecordProperties();
            repository.UpdateData(validId, newRecord);

            notifier.Success($"\n{entityName} suscessfully updated!");
            SetFooter();
        }

        public virtual void Delete()
        {
            Console.Clear();

            SetHeader($"Delete a {entityName}");

            if (repository.NoRecords())
            {
                SetFooter();
                return;
            }

            DisplayTable();

            int getId = GetInputId();
            int validId = repository.isValidId(getId);

            repository.Remove(validId);
            notifier.Success($"\n{entityName} suscessfully deleted!");
            SetFooter();
        }

        // Presentation facilities ---------------------------------------------------------

        public void SetHeader(string header)
        {
            Console.Clear();

            notifier.Text(
              $"\n\n{header.ToUpper()}"
            + "\n------------------------------\n");
        }

        public void SetFooter()
        {
            notifier.Text("\n\n<-'");
            Console.ReadLine();
        }

        public int SetMenu(string entity)
        {
            Console.Clear();

            notifier.Menu(
              $"\n\n{entity.ToUpper()}"
            + $"\n-------------------"
            + $"\n[1] Create {entity}."
            + $"\n[2] View {entity}'s table."
            + $"\n[3] Edit a {entity}."
            + $"\n[4] Delete a {entity}."
            + $"\n[5] Go back."
            + "\n\n→ ");

            int selectedOption = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            return selectedOption;
        }

        public void SetTable(string[] columnNames, int[] columnWidths, List<object> data)
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

        public Type SetField<Type>(string message, ConsoleColor color)
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
                this.notifier.Error($"\nInvalid input. Please enter a valid {typeof(Type).Name}.\n");
                return SetField<Type>(message, color);
            }
        }
    }
}

