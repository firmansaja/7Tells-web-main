namespace DataObjects
{
    public class DaoFactories
    {
        public static IDaoFactory GetFactory()
        {
            return new DaoFactory();
        }
    }
}