

namespace Findmaster.DataAccessLayer.Entity
{
    public class Vacancy
    {
        public Vacancy(string vacancyName, int vacancySalary, string vacancyEmployerName, string vacancyAddress, string vacancyRequirements, string vacancyExp, string vacancyEmploymentType, string vacancyDescription)
        {
            VacancyName = vacancyName;
            VacancySalary = vacancySalary;
            VacancyEmployerName = vacancyEmployerName;
            VacancyAddress = vacancyAddress;
            VacancyRequirements = vacancyRequirements;
            VacancyExp = vacancyExp;
            VacancyEmploymentType = vacancyEmploymentType;
            VacancyDescription = vacancyDescription;
        }

        public int VacancyId { get; set; }
        public string VacancyName { get; set; }

        public int VacancySalary { get; set; }

        public string VacancyEmployerName { get; set; }

        public string VacancyAddress { get; set; }

        public string VacancyRequirements { get; set; }

        public string VacancyExp { get; set; }

        public string VacancyEmploymentType { get; set; }

        public string VacancyDescription { get; set; }
    }
}
