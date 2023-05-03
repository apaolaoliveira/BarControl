using System.Collections;

namespace BarControl.Shared
{
    public abstract class RepositoryBase
    {
        protected ArrayList records;

        public ArrayList GetRecords()
        {
            return records;
        }

        public int idCounter = 1;

        public void IncreaseId()
        {
            idCounter++;
        }

        // CRUD --------------------------------------------------------------------

        public void Add(EntityBase entity)
        {
            records.Add(entity);
            entity.id = idCounter;
            IncreaseId();
        }

        public void UpdateData(int id, EntityBase entityEdit)
        {
            EntityBase entityUpdate = GetSelectedId(id);
            entityUpdate.UpdateData(entityEdit);
        }

        public void Remove(int selectedId)
        {
            EntityBase entity = GetSelectedId(selectedId);
            records.Remove(entity);
        }

        // Repository facilities ---------------------------------------------------

        public virtual EntityBase GetSelectedId(int selectedId)
        {
            EntityBase entity = null;

            foreach (EntityBase entityAdded in records)
            {
                if (entityAdded.id == selectedId)
                {
                    entity = entityAdded;
                    break;
                }
            }
            return entity;
        }

        public virtual int isValidId(int selectedId)
        {
            do
            {
                if (selectedId > 0 && selectedId < idCounter - 1)
                {
                    break;
                }

                else if (selectedId <= 0 || selectedId > idCounter - 1)
                {
                    PresentationBase.ColorfulMessage("\nThis ID doesn't exist. Try again:" + "\n→ ", ConsoleColor.Red);
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
                PresentationBase.ColorfulMessage("\nNo records found.", ConsoleColor.Red);
                PresentationBase.SetFooter();
                return true;
            }
            else { return false; }
        }
    }
}
