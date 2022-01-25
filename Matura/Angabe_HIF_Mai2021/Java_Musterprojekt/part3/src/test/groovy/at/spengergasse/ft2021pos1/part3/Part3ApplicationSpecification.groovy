package at.spengergasse.ft2021pos1.part3

import org.springframework.beans.factory.annotation.Autowired
import org.springframework.boot.test.context.SpringBootTest
import org.springframework.context.ApplicationContext
import spock.lang.Specification

@SpringBootTest
class Part3ApplicationSpecification extends Specification {

    @Autowired(required = true)
    ApplicationContext applicationContext

    def "load application context"() {
        expect:
        applicationContext != null
    }
}
