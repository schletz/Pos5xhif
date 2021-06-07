package at.spengergasse.ft2021pos1.part2.persistence;

import at.spengergasse.ft2021pos1.part2.domain.Gender;
import at.spengergasse.ft2021pos1.part2.domain.Person;
import at.spengergasse.ft2021pos1.part2.domain.Test;
import org.springframework.stereotype.Repository;

import java.time.LocalDate;
import java.time.Month;
import java.util.List;

@Repository
public class TestRepository {

    public List<Test> getTests() {
        return generateFakeTests(); // fake it till you make it
    }

    public static List<Test> generateFakeTests() {
        return List.of(
                Test.builder().person(Person.builder().gender(Gender.MALE).birthDate(LocalDate.of(1969, Month.SEPTEMBER, 1)).build()).build(),
                Test.builder().person(Person.builder().gender(Gender.MALE).birthDate(LocalDate.of(2000, Month.MAY, 1)).build()).build(),
                Test.builder().person(Person.builder().gender(Gender.FEMALE).birthDate(LocalDate.of(1974, Month.JANUARY, 4)).build()).build(),
                Test.builder().person(Person.builder().gender(Gender.MALE).birthDate(LocalDate.of(2010, Month.MAY, 22)).build()).build(),
                Test.builder().person(Person.builder().gender(Gender.DIVERS).birthDate(LocalDate.of(1999, Month.APRIL, 15)).build()).build(),
                Test.builder().person(Person.builder().gender(Gender.FEMALE).birthDate(LocalDate.of(1978, Month.JULY, 15)).build()).build(),
                Test.builder().person(Person.builder().gender(Gender.MALE).birthDate(LocalDate.of(1984, Month.DECEMBER, 15)).build()).build(),
                Test.builder().person(Person.builder().gender(Gender.FEMALE).birthDate(LocalDate.of(1985, Month.DECEMBER, 12)).build()).build(),
                Test.builder().person(Person.builder().gender(Gender.FEMALE).birthDate(LocalDate.of(1965, Month.JANUARY, 5)).build()).build(),
                Test.builder().person(Person.builder().gender(Gender.FEMALE).birthDate(LocalDate.of(1941, Month.JULY, 24)).build()).build()
        );
    }
}
