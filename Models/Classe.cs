namespace GestionScolaireFINAL.Models
{
    public class Classe
    {
        private List<Etudiant> _etudiants = new List<Etudiant>();

        public string Nom { get; private set; }
        public int Annee { get; private set; }

        public Classe(string nom, int annee)
        {
            Nom = nom;
            Annee = annee;
        }

        public void AjouterEtudiant(Etudiant etudiant)
        {
          if (!_etudiants.Contains(etudiant))
              _etudiants.Add(etudiant);
        }

        public double MoyenneClasse()
        {
            if (_etudiants.Count == 0) return 0;

            double total = 0;
            foreach (var etudiant in _etudiants)
                total += etudiant.CalculerMoyenne();

            return total / _etudiants.Count;
        }

        public override string ToString()
        {
            return Nom + " — Année " + Annee + " (" + _etudiants.Count + " étudiants)";
        }
    }
}
