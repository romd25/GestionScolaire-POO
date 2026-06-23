namespace GestionScolaireFINAL.Models
{
    public class Professeur : Personne
    {
      private static int _compteur = 1;
      private List<Cours> _cours = new List<Cours>();

        public string Code { get; }
        public string Specialite { get; private set; }

        public Professeur(string nom, string prenom, string email, string specialite) : base(nom, prenom, email)
        {
            Specialite = specialite;
            Code = "PROF" + _compteur++.ToString("D3");
        }

        public override string GetRole()
        {
            return "Professeur";
        }

        public void AjouterCours(Cours cours)
        {
            if (!_cours.Contains(cours))
                _cours.Add(cours);
        }

        public override string ToString()
        {
            return "[" + Code + "] " + NomComplet() + " — " + Specialite;
        }
    }
}
