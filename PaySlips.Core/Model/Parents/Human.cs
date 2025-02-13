namespace PaySlips.Core.Model.Parents
{
    public class Human
    {
        public string Firstname { get; set; }
        public string Secondname { get; set; }
        public string Surname { get; set; }
        public string Fullname { get => $"{Secondname} {Firstname} {Surname}"; }
    }
}
