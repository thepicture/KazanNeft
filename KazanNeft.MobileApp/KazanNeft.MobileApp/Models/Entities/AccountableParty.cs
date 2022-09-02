namespace KazanNeft.MobileApp.Models.Entities
{
    public class AccountableParty
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Phone { get; set; }
        public override string ToString()
        {
            return LastName + " " + FirstName;
        }
    }
}
