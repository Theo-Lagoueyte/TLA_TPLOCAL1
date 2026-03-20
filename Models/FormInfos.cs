namespace TPLOCAL1.Models
{
    public class FormInfos
    {
        private DateTime dateDebut;

        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Sexe { get; set; }
        public string Adresse { get; set; }
        public int CodePostal { get; set; }
        public string Ville { get; set; }
        public string AdresseMail { get; set; }
        public DateTime DateDebut { get => dateDebut; set => dateDebut = value; }
        public string FormationSuivie { get; set; }
        public string AvisCobol { get; set; }
        public string AvisCSharp { get; set; }

        public FormInfos() { }
    }
}
