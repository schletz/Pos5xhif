package at.spengergasse.ft2021pos1.part3.service.dtos;

import lombok.Builder;
import lombok.Getter;

@Getter
public class DetailedPersonStatistic extends PersonStatistic {

    private DetailedCount male;
    private DetailedCount female;

    @Builder
    public DetailedPersonStatistic(Integer yearOfBirth, DetailedCount male, DetailedCount female) {
        super(yearOfBirth);
        this.male = male;
        this.female = female;
    }
}
