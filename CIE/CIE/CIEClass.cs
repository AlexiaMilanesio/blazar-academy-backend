using System;

namespace CIE
{
	public class CIEClass
	{
        public string? Nome { get; set; }
        public string? Cognome { get; set; }
        public string? CittaDiResidenza { get; set; }
        public DateTime? DataDiNascita { get; set; }

        public CIEClass(string? nome, string? cognome, string? cittaDiResidenza, DateTime? dataDiNascita)
        {
            Nome = nome;
            Cognome = cognome;
            CittaDiResidenza = cittaDiResidenza;
            DataDiNascita = dataDiNascita;
        }

        private CIEClass() { }
    }
}

