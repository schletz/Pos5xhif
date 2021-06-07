package at.spengergasse.ft2021pos1.part2.domain;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;

import java.time.LocalDate;

@NoArgsConstructor
@AllArgsConstructor
@Builder
@Getter
public class Person {
    private Gender gender;
    private LocalDate birthDate;
}