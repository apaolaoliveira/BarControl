
using System.Collections;

namespace BarControl.Shared
{
    public abstract class EntityBase
    {
        public int id;
        public abstract void UpdateData(EntityBase entity);
        public abstract ArrayList Errors();
    }
}
