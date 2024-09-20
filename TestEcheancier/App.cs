using System;
using System.Globalization;  // Ajoute ceci
using TestEcheancier.Models;

namespace TestEcheancier
{
    public class App
    {
        public void Run(Arguments arguments)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            if (arguments.DateFin.HasValue && arguments.Periodicite > 0)
            {
                CalculerEcheancierAvecPeriode(arguments);
            }
            else if (arguments.NBEcheance > 0 && arguments.Periodicite > 0)
            {
                CalculerEcheancierAvecNombreEcheances(arguments);
            }
            else if (arguments.NBEcheance > 0 && arguments.DateFin.HasValue)
            {
                CalculerEcheancierAvecFin(arguments);
            }
            else
            {
                Console.WriteLine("Les paramètres fournis sont incomplets ou incorrects.");
            }
        }

        private void CalculerEcheancierAvecPeriode(Arguments arguments)
        {
            DateTime startDate = arguments.DateDebut;
            DateTime endDate = arguments.DateFin.Value;
            int periodicite = arguments.Periodicite;
            decimal montantParEcheance = arguments.MontantAnnuel / (12 / periodicite);
            int echeancesCount = 0;
            var culture = new CultureInfo("fr-FR");  // Ajoute ceci

            while (startDate < endDate)
            {
                echeancesCount++;
                DateTime echeanceFin = startDate.AddMonths(periodicite).AddDays(-1);

                if (echeanceFin > endDate)
                {
                    echeanceFin = endDate;
                }

                Console.WriteLine($"Échéance {echeancesCount}: du {startDate:dd/MM/yyyy} au {echeanceFin:dd/MM/yyyy} pour un Montant de : {montantParEcheance.ToString("C2", culture)}");

                startDate = echeanceFin.AddDays(1);
            }
        }

        private void CalculerEcheancierAvecNombreEcheances(Arguments arguments)
        {
            DateTime startDate = arguments.DateDebut;
            int periodicite = arguments.Periodicite;
            decimal montantParEcheance = arguments.MontantAnnuel / arguments.NBEcheance;
            var culture = new CultureInfo("fr-FR");  // Ajoute ceci

            for (int i = 0; i < arguments.NBEcheance; i++)
            {
                DateTime echeanceFin = startDate.AddMonths(periodicite).AddDays(-1);
                Console.WriteLine($"Échéance {i + 1}: du {startDate:dd/MM/yyyy} au {echeanceFin:dd/MM/yyyy} pour un Montant de : {montantParEcheance.ToString("C2", culture)}");
                startDate = echeanceFin.AddDays(1);
            }
        }

        private void CalculerEcheancierAvecFin(Arguments arguments)
        {
            DateTime startDate = arguments.DateDebut;
            DateTime endDate = arguments.DateFin.Value;
            int echeances = arguments.NBEcheance;
            decimal montantParEcheance = arguments.MontantAnnuel / echeances;
            int monthsPerEcheance = (int)((endDate - startDate).TotalDays / 30) / echeances;
            var culture = new CultureInfo("fr-FR");  // Ajoute ceci

            for (int i = 0; i < echeances; i++)
            {
                DateTime echeanceFin = startDate.AddMonths(monthsPerEcheance).AddDays(-1);
                if (echeanceFin > endDate) echeanceFin = endDate;

                Console.WriteLine($"Échéance {i + 1}: du {startDate:dd/MM/yyyy} au {echeanceFin:dd/MM/yyyy} pour un Montant de : {montantParEcheance.ToString("C2", culture)}");
                startDate = echeanceFin.AddDays(1);
            }
        }
    }
}
