package at.spengergasse.ft2021pos1.part3.service.dtos;


import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;

import java.time.LocalDateTime;

@AllArgsConstructor
@Builder
@Getter
public class TestDto {
    private String id;
    private LocalDateTime testTimeStamp;
    private String station;
    private Integer testBay;
    private String testKitType;
    private String result;
}
