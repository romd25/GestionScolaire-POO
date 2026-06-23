namespace GestionScolaireFINAL.Models
{
    public class Cours
    {
        private static int _compteur = 1;
        private List<Etudiant> _inscrits = new List<Etudiant>();

        public string Code { get; }
        public string Intitule { get; set; }
        public double Coefficient { get; set; }
        public Professeur? Titulaire { get; private set; }
        public List<Etudiant> Inscrits { get { return _inscrits; } }

        public Cours(string intitule, double coefficient)
        {
          if (coefficient <= 0)
              throw new ArgumentException("Le coefficient doit être supérieur à 0.");

            Intitule = intitule;
            Coefficient = coefficient;
            Code = "CRS" + _compteur++.ToString("D3");
        }

        public void AssignerProfesseur(Professeur prof)
        {
            Titulaire = prof;
            prof.AjouterCours(this);
        }

        public void InscrireEtudiant(Etudiant etudiant)
        {
            if (!_inscrits.Contains(etudiant))
                _inscrits.Add(etudiant);
        }

        public override string ToString()
        {
            return "[" + Code + "] " + Intitule + " (coeff. " + Coefficient + ")";
        }
    }
}
