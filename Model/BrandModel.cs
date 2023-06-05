namespace ProductManagement.Model
{
    public class BrandModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Alias { get; set; }

        #region Constructor

        public BrandModel()
        {
        }

        public BrandModel(string name)
        {
            Name = name;
        }

        public BrandModel(int id, string name) : this(name)
        {
            Id = id;
        }

        public BrandModel(int id, string name, string alias) : this(id, name)
        {
            Alias = alias;
        }

        #endregion Constructor
    }
}