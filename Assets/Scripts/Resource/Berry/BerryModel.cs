namespace BTF.Resource
{
    public class BerryModel
    {
        public bool IsAvailable { get; private set; }

        public BerryModel()
        {
            IsAvailable = true; 
        }

        public void Consume() => IsAvailable = false;
        
        public void Regrow() => IsAvailable = true; 
    }
}
