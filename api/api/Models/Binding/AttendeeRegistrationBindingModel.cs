using api.Data.Enums;

namespace api.Models.Binding;

public class AttendeeRegistrationBindingModel
{
    public string EmailAddress { get;  set; }

    public string FullName { get;  set; }

    public int? Age { get;  set; }

    public Gender? Gender { get;  set; }

    public string Phone { get;  set; }

    public string ResidentialAddress { get;  set; }

    public bool ReturnedInLastTenDays { get;  set; }

    public bool LiveWithCovidCaregivers { get;  set; }

    public bool CaredForSickPerson { get;  set; }

    public MultiChoice? HaveCovidSymptoms { get;  set; }

    public int? SeatNumber { get; set; }
}