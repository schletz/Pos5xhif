using System;
using System.Collections.Generic;

namespace SPG_Fachtheorie.Aufgabe2.Model
{

    public class Student
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        protected Student() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Student(string name, DateTime birthDate)
        {
            Name = name;
            BirthDate = birthDate;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class PossibleAnswer
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        protected PossibleAnswer() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public PossibleAnswer(Question question, string text, int points)
        {
            Question = question;
            Text = text;
            Points = points;
        }

        public int Id { get; set; }
        public Question Question { get; set; }
        public string Text { get; set; }
        public int Points { get; set; }
    }

    public class Question
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        protected Question() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Question(string text, Exam exam)
        {
            Text = text;
            Exam = exam;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public Exam Exam { get; set; }
        public List<PossibleAnswer> PossibleAnswers { get; } = new();
    }

    public class Answer
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        protected Answer() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Answer(Participation participation, PossibleAnswer givenAnswer)
        {
            Participation = participation;
            GivenAnswer = givenAnswer;
        }
        public int Id { get; set; }
        public Participation Participation { get; set; }
        public PossibleAnswer GivenAnswer { get; set; }
    }

    public class Exam
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        protected Exam() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Exam(string name, int failThreshold, bool visible)
        {
            Name = name;
            FailThreshold = failThreshold;
            Visible = visible;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int FailThreshold { get; set; }
        public bool Visible { get; set; }
        public List<Question> Questions { get; } = new();
    }
    public class Participation
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        protected Participation() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Participation(Exam exam, Student student)
        {
            Exam = exam;
            Student = student;
        }
        public int Id { get; set; }
        public Exam Exam { get; set; }
        public Student Student { get; set; }
        public List<Answer> Answers { get; } = new();
    }


}