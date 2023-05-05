
using System.Collections;

namespace BarControl.Shared
{
    public abstract class EntityBase<EntityType>
    {
        public int id;
        public abstract void UpdateData(EntityType entity);
        public abstract ArrayList Errors();
    }
}
