namespace Findmaster.DataAccessLayer.Entity
{
    public class Vacancy
    {
        public int VacancyId { get; set; }
        public string VacancyName { get; set; }

        public int VacancySalary { get; set; }

        public string VacancyEmployerName { get; set; }

        public string VacancyAddress { get; set; }

        public string VacancyRequirements { get; set; }

        public string VacancyExp { get; set; }

        public string VacancyEmploymentType { get; set; }

        public string VacancyDescription { get; set; }

        public byte[] VacancyDatePosted { get; set; }
    }
}
