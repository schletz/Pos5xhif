package at.spengergasse.ft2021pos1.part3.service;

import at.spengergasse.ft2021pos1.part3.service.commands.CreateTestAppointmentCommand;
import at.spengergasse.ft2021pos1.part3.service.dtos.*;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.UUID;

@Service
public class PersonService {

    public List<PersonStatistic> getPersonStatistic() {
        // in reality to be replaces with some brave and high performant business logic implementation
        return generateFakePersonStatistics();
    }

    public List<PersonStatistic> getDetailedPersonStatistic() {
        // in reality to be replaces with some brave and high performant business logic implementation
        return generateFakeDetailedPersonStatistics();
    }

    public TestDto createTestAppointment(Long personId, CreateTestAppointmentCommand createTestAppointmentCommand) {
        // in reality to be stored in the database and of course linked to the person etc.
        // for the sake of brevity, error handling omitted
        return createFakeTestDto(createTestAppointmentCommand);
    }

    public static List<PersonStatistic> generateFakePersonStatistics() {
        return List.of(
                SimplePersonStatistic.builder().yearOfBirth(1969).male(127l).female(148l).build(),
                SimplePersonStatistic.builder().yearOfBirth(1970).male(132l).female(119l).build(),
                SimplePersonStatistic.builder().yearOfBirth(1971).male(114l).female(122l).build(),
                SimplePersonStatistic.builder().yearOfBirth(1972).male(205l).female(199l).build(),
                SimplePersonStatistic.builder().yearOfBirth(1973).male(137l).female(110l).build()
        );
    }

    public static List<PersonStatistic> generateFakeDetailedPersonStatistics() {
        return List.of(
                DetailedPersonStatistic.builder()
                                       .yearOfBirth(1969)
                                       .male(DetailedCount.builder().positive(7l).negative(115l).invalid(5l).build()) // 127
                                       .female(DetailedCount.builder().positive(8l).negative(130l).invalid(10l).build()) // 148
                                       .build(),
                DetailedPersonStatistic.builder()
                                       .yearOfBirth(1970)
                                       .male(DetailedCount.builder().positive(2l).negative(125l).invalid(5l).build()) // 132
                                       .female(DetailedCount.builder().positive(9l).negative(100l).invalid(10l).build()) // 119
                                       .build(),
                DetailedPersonStatistic.builder()
                                       .yearOfBirth(1971)
                                       .male(DetailedCount.builder().positive(4l).negative(105l).invalid(5l).build()) // 114
                                       .female(DetailedCount.builder().positive(2l).negative(110l).invalid(10l).build()) // 122
                                       .build(),
                DetailedPersonStatistic.builder()
                                       .yearOfBirth(1972)
                                       .male(DetailedCount.builder().positive(5l).negative(199l).invalid(5l).build()) // 205
                                       .female(DetailedCount.builder().positive(9l).negative(180l).invalid(10l).build()) // 199
                                       .build(),
                DetailedPersonStatistic.builder()
                                       .yearOfBirth(1973)
                                       .male(DetailedCount.builder().positive(7l).negative(125l).invalid(5l).build()) // 137
                                       .female(DetailedCount.builder().positive(10l).negative(90l).invalid(10l).build()) // 110
                                       .build()
        );
    }

    public static TestDto createFakeTestDto(CreateTestAppointmentCommand createTestAppointmentCommand) {
        return TestDto.builder()
                      .id(UUID.randomUUID().toString()) // fake it till you make it
//                      .testTimeStamp()
//                      .station()
                      .build();
    }
}
