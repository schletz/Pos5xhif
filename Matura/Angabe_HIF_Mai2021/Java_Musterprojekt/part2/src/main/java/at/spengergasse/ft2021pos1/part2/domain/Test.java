package at.spengergasse.ft2021pos1.part2.domain;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;

import java.time.LocalDateTime;

@NoArgsConstructor
@AllArgsConstructor
@Builder
@Getter
public class Test {
    private LocalDateTime testTimeStamp;
    private Person person;
    private TestResult testResult;
}