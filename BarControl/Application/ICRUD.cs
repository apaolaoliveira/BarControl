
namespace BarControl.Application
{
    public interface ICRUD
    {
        public int SetMenu(string entity);
        public void Create();
        public void Read();
        public void Update();
        public void Delete();
        public void SetFooter();
    }
}
