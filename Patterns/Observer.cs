using GestionScolaireFINAL.Models;

namespace GestionScolaireFINAL.Patterns
{
    public interface IObservateurEtudiant
    {
        void SurNouvelleNote(Etudiant etudiant, Note note);
    }

    public class AlerteNoteBasse : IObservateurEtudiant
    {
        private double _seuil;
        private List<string> _alertes = new List<string>();

        public List<string> Alertes { get { return _alertes; } }

        public AlerteNoteBasse(double seuil)
        {
          _seuil = seuil;
        }

        public void SurNouvelleNote(Etudiant etudiant, Note note)
        {
            if (note.Valeur >= _seuil)
                return;

            string msg = DateTime.Now.ToString("HH:mm") + " — " + etudiant.NomComplet() + " : " + note.Valeur + "/20 en " + note.Cours.Intitule;
            _alertes.Add(msg);
        }
    }
}
