namespace OamCake.Entity
{
    public class Provider : BaseEntity
    {
        public short Id { get; set; }
        public string Nombre { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
