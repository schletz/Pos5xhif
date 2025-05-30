using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe2.Services
{
    public record ExamResultDto(
        int ExamId, string ExamName, int ExamFailThreshold, int StudentId,
        string StudentName, DateTime StudentBirthDate, int Points);

    public class ExamService
    {
        private readonly ExamsContext _db;

        public ExamService(ExamsContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Diese Methode soll eine neue Prüfungsfrage zu einer bestehenden Prüfung (Exam) hinzufügen.
        /// Regeln siehe Angabe.
        /// </summary>
        public Question AddQuestion(string questionText, int examId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Diese Methode soll eine Statistik mit der erreichten Punkteanzahl zurückgeben.
        /// Nutze den Test CalculateExamStatisitcsSuccessTest, um die Korrektheit deiner Methode zu prüfen.
        /// </summary>
        /// <returns></returns>
        public List<ExamResultDto> CalculateExamResults()
        {
            throw new NotImplementedException();
        }
    }
}