
SpringBoot 프로젝트에서 
application.propertise와 application.yml 설정파일이 있는데
yml파일이 좀 더 사용하기 편한다.


# MySQL Spring 프로젝트 연결 application.yml로 설정:
spring:
  datasource:
    driver-class-name: com.mysql.cj.jdbc.Driver
    url: jdbc:mysql://localhost:3306/blog?serverTimezone=Asia/Seoul
    username: cos
    password: cos1234

# MySQL Spring 프로젝트 연결 application.propertise로 설정 :
spring.datasource.driver-class-name = com.mysql.cj.jdbc.Driver
spring.datasource.url = jdbc:mysql://localhost:3306/blog?serverTimezone=Asia/Seoul
spring.datasourec.usename = cos
spring.datasource.password = cos1234
