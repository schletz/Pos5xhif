package at.spengergasse.ft2021pos1.part3.service;

import at.spengergasse.ft2021pos1.part3.service.commands.UpdateTestResultCommand;
import at.spengergasse.ft2021pos1.part3.service.dtos.TestDto;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;

@Service
public class TestService {

    public TestDto updateTestResult(String testId, UpdateTestResultCommand updateTestResultCommand) {
        // in reality to be stored in the database and of course linked to the person etc.
        // for the sake of brevity, error handling omitted
        return generateFakeTestDto(testId, updateTestResultCommand);
    }

    public static TestDto generateFakeTestDto(String testId, UpdateTestResultCommand updateTestResultCommand) {
        return TestDto.builder()
                      .id(testId)
                      .testTimeStamp(LocalDateTime.now()) // fake it till you make it
                      .station("Stadthalle") // fake it till you make it
//                      .testBay()
//                      .testKitType()
//                      .result()
                      .build();
    }
}