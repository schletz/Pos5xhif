package at.spengergasse.ft2021pos1.part3.service.dtos;

import lombok.Builder;
import lombok.Getter;

@Getter
public class SimplePersonStatistic extends PersonStatistic {

    private final Long male;
    private final Long female;

    @Builder
    public SimplePersonStatistic(Integer yearOfBirth, Long male, Long female) {
        super(yearOfBirth);
        this.male = male;
        this.female = female;
    }
}
