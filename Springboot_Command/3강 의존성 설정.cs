
# 의존성 설정 

# Spring Start project에서 아래 항목들 의존성 추가
1. Spring Boot DevTools
2. Lombok
3. Spring Data JPA
4. MySQL Driver
5. Spring security
6. Spring Web

# 설명
1. Spring Boot DevTools :
=> Spring boot에서 추가해주면 좋은 개발 경험을 갖게 될것이다. 
=> 라이브리로드 기능을 준다.
=> 자동 재시작을 해준다 (class path에서 Change될 때 자동으로 재시작 개발할 때 편리함 );


2. Lombok :
=> Getter와 Setter, Constructor를 어노테이션을 사용하여 자동으로 생성;


3. Spring Data JPA :
=> Database를 만들 때 JPA를 통해서 만들것
=> ORM 이용 가능;


4. MySQL Driver;


5. Spring security :
=> Spring을 개발을할 때 보안적인 기능을 제공해주는 라이브러리;


6. Spring Web :
=> Spring에서 개발을 할 때 어노테이션을 사용하려면 사용해야한다.
=> 내장형 Tomcat을 가지고 있기 때문에 Tomcat을 따로 설치할 필요가 없다.


7. 추가설정 :
=> <!-- 추가 라이브러리 시작 -->
		<!-- 시큐리티 태그 라이브러리 -->
		<!-- security는 잠시 주석 처리
		<dependency>
  			<groupId>org.springframework.security</groupId>
  			<artifactId>spring-security-taglibs</artifactId>
		</dependency>
		-->
		<!-- JSP 템플릿 엔진  jasper가 없으면 main폴더에서 JSP파일을 인식하지 못함 -->
		<dependency>
  			<groupId>org.apache.tomcat</groupId>
  			<artifactId>tomcat-jasper</artifactId>
  			<version>9.0.73</version>
		<!-- JSTL --> 
		</dependency>
		<dependency>
  			<groupId>javax.servlet</groupId>
  			<artifactId>jstl</artifactId>
  			<version>1.2</version>		
  		</dependency>
		<!-- 추가 라이브러리 종료-->
		<dependency>
			<groupId>org.springframework.boot</groupId>
			<artifactId>spring-boot-starter-data-jpa</artifactId>
		</dependency>
		<!-- 시큐리티는 잠시 주석 처리
		<dependency>
			<groupId>org.springframework.boot</groupId>
			<artifactId>spring-boot-starter-security</artifactId>
		</dependency>
		-->
		<dependency>
			<groupId>org.springframework.boot</groupId>
			<artifactId>spring-boot-starter-web</artifactId>
		</dependency>

		<dependency>
			<groupId>org.springframework.boot</groupId>
			<artifactId>spring-boot-devtools</artifactId>
			<scope>runtime</scope>
			<optional>true</optional>
		</dependency>
		<!--
		<dependency>
			<groupId>com.mysql</groupId>
			<artifactId>mysql-connector-j</artifactId>
			<scope>runtime</scope>
		</dependency>
		-->
