using System;
using System.Collections.Generic;

namespace SPG_Fachtheorie.Aufgabe2.Model
{
    public enum Gender { Male = 1 , Female = 2 }

    public class Student
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Student() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Student(string firstname, string lastname, string email, Schoolclass schoolclass, Gender gender, DateTime dateOfBirth)
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Schoolclass = schoolclass;
        }
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public Schoolclass Schoolclass { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Registration> Registrations { get; } = new();
    }
    public class Teacher
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Teacher() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Teacher(string shortname, string firstname, string lastname, string email, Gender gender)
        {
            Shortname = shortname;
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Gender = gender;
        }

        public int Id { get; set; }
        public string Shortname { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public List<Languageweek> LanguageweekTeachers { get; } = new();
        public List<Languageweek> LanguageweekSupportTeachers { get; } = new();

    }
    public class Schoolclass
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Schoolclass() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Schoolclass(string shortname, string department)
        {
            Shortname = shortname;
            Department = department;
        }
        public int Id { get; set; }
        public string Shortname { get; set; }
        public string Department { get; set; }
        public List<Languageweek> Languageweeks { get; } = new();
        public List<Student> Students { get; } = new();
    }
    public class Languageweek
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Languageweek() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Languageweek(Schoolclass schoolclass, Destination destination, DateTime from, DateTime to, Teacher teacher, decimal totalPrice)
        {
            Schoolclass = schoolclass;
            Destination = destination;
            From = from;
            To = to;
            Teacher = teacher;
            TotalPrice = totalPrice;
        }
        public int Id { get; set; }
        public Schoolclass Schoolclass { get; set; }
        public Destination Destination { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Teacher Teacher { get; set; }
        public decimal TotalPrice { get; set; }
        public Teacher? SupportTeacher { get; set; }
        public List<Registration> Registrations { get; } = new();
    }

    public class Registration
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Registration() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Registration(Languageweek languageweek, Student student, DateTime registerDate)
        {
            Languageweek = languageweek;
            Student = student;
            RegisterDate = registerDate;
        }
        public int Id { get; set; }
        public Languageweek Languageweek { get; set; }
        public Student Student { get; set; }
        public DateTime RegisterDate { get; set; }
    }

    public class Destination
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Destination() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Destination(string city, string country)
        {
            City = city;
            Country = country;
        }
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<Languageweek> Languageweeks { get; } = new();
    }
}