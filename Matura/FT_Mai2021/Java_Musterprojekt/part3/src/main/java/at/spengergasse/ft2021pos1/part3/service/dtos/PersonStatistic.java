package at.spengergasse.ft2021pos1.part3.service.dtos;

import lombok.Getter;
import lombok.RequiredArgsConstructor;

@RequiredArgsConstructor
@Getter
public abstract class PersonStatistic {
    private final Integer yearOfBirth;
}
