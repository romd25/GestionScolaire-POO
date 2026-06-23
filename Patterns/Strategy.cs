using GestionScolaireFINAL.Models;

namespace GestionScolaireFINAL.Patterns
{
    public interface IStrategieTri
    {
        string Description { get; }
        List<Etudiant> Trier(List<Etudiant> etudiants);
    }

    public class TriParMoyenne : IStrategieTri
    {
        public string Description { get { return "Par moyenne décroissante"; } }

        public List<Etudiant> Trier(List<Etudiant> etudiants)
        {
            List<Etudiant> copie = new List<Etudiant>(etudiants);
            copie.Sort((a, b) => b.CalculerMoyenne().CompareTo(a.CalculerMoyenne()));
            return copie;
        }
    }

    public class TriAlphabetique : IStrategieTri
    {
      public string Description { get { return "Alphabétique"; } }

        public List<Etudiant> Trier(List<Etudiant> etudiants)
        {
            List<Etudiant> copie = new List<Etudiant>(etudiants);
            copie.Sort((a, b) => a.Nom.CompareTo(b.Nom));
            return copie;
        }
    }

    public class TriParAnnee : IStrategieTri
    {
        public string Description { get { return "Par année"; } }

        public List<Etudiant> Trier(List<Etudiant> etudiants)
        {
          List<Etudiant> copie = new List<Etudiant>(etudiants);
          copie.Sort((a, b) => a.Annee.CompareTo(b.Annee));
          return copie;
        }
    }

    public class ClassementEtudiants
    {
        private IStrategieTri _strategie;

        public string StrategieActive { get { return _strategie.Description; } }

        public ClassementEtudiants(IStrategieTri strategie)
        {
            _strategie = strategie;
        }

        public void ChangerStrategie(IStrategieTri nouvelleStrategie)
        {
            _strategie = nouvelleStrategie;
        }

        public List<Etudiant> Classer(List<Etudiant> etudiants)
        {
            return _strategie.Trier(etudiants);
        }
    }
}
