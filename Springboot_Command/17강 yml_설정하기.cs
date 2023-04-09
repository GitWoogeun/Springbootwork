
# yaml 설정이란?

1. 인코딩
2. 데이터베이스 연결
3. 서버 포트 
등등


# XML, JSON, YAML의 차이 
[ 경로 ] : https://www.inflearn.com/questions/16184


# YMAL 설정 :
server:
  port: 8000
  servlet:
    context-path: /blog                       # context-path : 내 프로젝트에 들어가기 위한 진입장벽   => http://localhost:8000/blog/~
    encoding:
      charset: UTF-8
      enabled: true
      force: true
    
spring:
  mvc:
    view:
      prefix: /WEB-INF/views/                 # prefix : Controller가 리턴을할 때 앞에 붙여주는 경로 명
      suffix: .jsp                            # suffix : Controller가 리턴을할 때 뒤에 붙여주는 경로 명 
      
  datasource:                                 # database 설정
    driver-class-name: com.mysql.cj.jdbc.Driver
    url: jdbc:mysql://localhost:3306/blog?serverTimezone=Asia/Seoul
    username: cos
    password: cos1234
    
  jpa:
    open-in-view: true
    hibernate:
      ddl-auto: create
      naming:
        physical-strategy: org.hibernate.boot.model.naming.PhysicalNamingStrategyStandardImpl
      use-new-id-generator-mappings: false
    show-sql: true
    properties:
      hibernate.format_sql: true

  jackson:
    serialization:
      fail-on-empty-beans: false


# pom.xml파일 설정
# JSP 템플릿 엔진  jasper가 없으면 main폴더에서 JSP파일을 인식하지 못함
# static 폴더는 정적파일만 놓을 수 있다. 
<dependency>
	<groupId>org.apache.tomcat</groupId>
	<artifactId>tomcat-jasper</artifactId>
	<version>9.0.22</version>
</dependency>


