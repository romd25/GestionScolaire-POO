using GestionScolaireFINAL;
using GestionScolaireFINAL.UI;

var gestionnaire = new GestionnaireEcole();
var menu = new MenuPrincipal(gestionnaire);
menu.Lancer();
