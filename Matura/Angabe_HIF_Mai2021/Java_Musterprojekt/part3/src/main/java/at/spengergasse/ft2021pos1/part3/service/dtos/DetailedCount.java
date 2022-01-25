package at.spengergasse.ft2021pos1.part3.service.dtos;

import lombok.Builder;
import lombok.Getter;
import lombok.RequiredArgsConstructor;

@RequiredArgsConstructor
@Builder
@Getter
public class DetailedCount {
    private final Long positive;
    private final Long negative;
    private final Long invalid;
}
