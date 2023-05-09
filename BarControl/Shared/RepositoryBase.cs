
namespace BarControl.Shared
{
    public abstract class RepositoryBase<EntityType> 
                          where EntityType : EntityBase<EntityType>
    {
        protected List<EntityType> records;

        public List<EntityType> GetRecords()
        {
            return records;
        }

        public int idCounter = 1;

        public void IncreaseId()
        {
            idCounter++;
        }

        public Notifier notifier = new Notifier();

        // CRUD --------------------------------------------------------------------

        public void Add(EntityType entity)
        {
            records.Add(entity);
            entity.id = idCounter;
            IncreaseId();
        }

        public void UpdateData(int id, EntityType entityEdit)
        {
            EntityType entityUpdate = GetSelectedId(id);
            entityUpdate.UpdateData(entityEdit);
        }

        public void Remove(int selectedId)
        {
            EntityType entity = GetSelectedId(selectedId);
            records.Remove(entity);
        }

        // Repository facilities ---------------------------------------------------

        public virtual EntityType GetSelectedId(int selectedId) => records.Find(records => records.id == selectedId); //Lambda

        public virtual int isValidId(int selectedId)
        {
            do
            {
                if (selectedId <= 0 || selectedId > idCounter - 1)
                {
                    notifier.Error("\nThis ID doesn't exist. Try again:" + "\n→ ");
                    selectedId = Convert.ToInt32(Console.ReadLine());
                }

                else { break; }

            } while (true);

            return selectedId;
        }

        public bool NoRecords()
        {
            if (records.Count == 0)
            {
                notifier.Error("\nNo records found.");
                return true;
            }
            else { return false; }
        }
    }
}
