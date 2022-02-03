using System;
using api.Data.Enums;
using meerkat;
using meerkat.Attributes;
using moment.net;

namespace api.Data.Entities;

[Collection(Name = "attendance", TrackTimestamps = true)]
public class Attendee : Schema
{
    public DateTime Date { get; private set; }

    public string EmailAddress { get; private set; }

    public string FullName { get; private set; }

    public int? Age { get; private set; }

    public Gender? Gender { get; private set; }

    public string Phone { get; private set; }

    public string ResidentialAddress { get; private set; }

    public bool ReturnedInLastTenDays { get; private set; }

    public bool LiveWithCovidCaregivers { get; private set; }

    public bool CaredForSickPerson { get; private set; }

    public MultiChoice? HaveCovidSymptoms { get; private set; }

    [Obsolete("Replaced by 'SeatAssigned'")]
    public int? SeatNumber { get; private set; }

    public string SeatAssigned { get; private set; }

    public string SeatType { get; private set; }

    [Ignore] 
    public int SerialNo { get; set; }

    private Attendee()
    {
    }

    public Attendee(string firstName, string lastName, string phone, string seatAssigned, string seatType)
    {
        Date = DateTime.UtcNow.Date;
        FullName = $"{firstName} {lastName}";
        Phone = phone;
        SeatAssigned = seatAssigned;
        SeatType = seatType;
    }

    public Attendee(string emailAddress, string fullName, int? age, string phone, string residentialAddress,
        Gender? gender, bool returnedInLastTenDays, bool liveWithCovidCaregivers, bool caredForSickPerson,
        MultiChoice? haveCovidSymptoms)
    {
        // they should be registering against the next sunday
        Date = DateTime.UtcNow.DayOfWeek == DayOfWeek.Sunday
            ? DateTime.UtcNow.Date
            : DateTime.UtcNow.Date.Next(DayOfWeek.Sunday);
        EmailAddress = emailAddress;
        FullName = fullName;
        Age = age;
        Phone = phone;
        ResidentialAddress = residentialAddress;
        Gender = gender;
        ReturnedInLastTenDays = returnedInLastTenDays;
        LiveWithCovidCaregivers = liveWithCovidCaregivers;
        CaredForSickPerson = caredForSickPerson;
        HaveCovidSymptoms = haveCovidSymptoms;
    }

    public bool CanRegister()
    {
        return CaredForSickPerson == false && ReturnedInLastTenDays == false && LiveWithCovidCaregivers == false &&
               HaveCovidSymptoms == MultiChoice.No;
    }

    public void UpdateDate(DateTime? date)
    {
        if (date.HasValue) Date = date.Value.Date;
    }

    public void UpdateFullName(string fullName)
    {
        if (!string.IsNullOrWhiteSpace(fullName)) FullName = fullName;
    }

    public void UpdateResidentialAddress(string residentialAddress)
    {
        if (!string.IsNullOrWhiteSpace(residentialAddress)) ResidentialAddress = residentialAddress;
    }

    public void UpdatePhone(string phone)
    {
        if (!string.IsNullOrWhiteSpace(phone)) Phone = phone;
    }

    public void UpdateEmail(string email)
    {
        if (!string.IsNullOrWhiteSpace(email)) EmailAddress = email;
    }

    public void UpdateSeatAssigned(string seatAssigned)
    {
        if (!string.IsNullOrWhiteSpace(seatAssigned)) SeatAssigned = seatAssigned;
    }

    public void UpdateSeatType(string seatType)
    {
        if (!string.IsNullOrWhiteSpace(seatType)) SeatType = seatType;
    }

    public void UpdateGender(Gender? gender)
    {
        if (gender.HasValue) Gender = gender.Value;
    }

    public void UpdateHaveCovidSymptoms(MultiChoice? haveCovidSymptoms)
    {
        if (haveCovidSymptoms.HasValue) HaveCovidSymptoms = haveCovidSymptoms.Value;
    }

    public void UpdateAge(int? age)
    {
        if (age.HasValue) Age = age.Value;
    }

    public void UpdateSeatNumber(int? seatNumber)
    {
        if (seatNumber.HasValue) SeatNumber = seatNumber.Value;
    }

    public void UpdateReturnedInLastTenDays(bool? returnedInLastTen)
    {
        if (returnedInLastTen.HasValue) ReturnedInLastTenDays = returnedInLastTen.Value;
    }

    public void UpdateLiveWithCovidCaregivers(bool? liveWithCaregivers)
    {
        if (liveWithCaregivers.HasValue) LiveWithCovidCaregivers = liveWithCaregivers.Value;
    }

    public void UpdateCaredForSickPerson(bool? caredForSickPerson)
    {
        if (caredForSickPerson.HasValue) CaredForSickPerson = caredForSickPerson.Value;
    }
}