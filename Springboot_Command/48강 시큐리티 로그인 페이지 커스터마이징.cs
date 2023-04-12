
# 시큐리티를 사용하여 로그인 페이지 커스터마이징 하기

Spring Security를 사용하게 된다면
pom.xml에서 아래 dependency를 추가하면 
localhost:8080/ -> index.jsp가 잠긴다.
<!-- 시큐리티  ( 이 dependency 때문에 화면이 잠김 )-->
<dependency>
    <groupId>org.springframework.boot</groupId>
    <artifactId>spring-boot-starter-security</artifactId>
</dependency>

로그인을 해야 접속을 할 수 있는데
ID : user
PW : 콘솔 log에서 ctl + F 해서 password를 입력하면 password가 출력된걸 복사해서 붙여넣기 하면 된다.

┌─────────────────────────────────────────────────────────────────────────────────
│ Spring Security를 사용하면서 변경된 페이지들 
└─────────────────────────────────────────────────────────────────────────────────

┌─────────────────────────────────────────────────────────────────────────────────
│ 1번째 : application.yml에서 context-path '/' 변경 
└─────────────────────────────────────────────────────────────────────────────────
server:
  port: 8000
  servlet:
    context-path: /                            # context-path : 내 프로젝트에 들어가기 위한 진입장벽   => http://localhost:8000/blog/~
    encoding:
      charset: UTF-8
      enabled: true
      force: true

┌─────────────────────────────────────────────────────────────────────────────────
│ 2번째 : loginForm.jsp ( 다시 action으로 form 데이터 전송 및 name 추가 )
└─────────────────────────────────────────────────────────────────────────────────
<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<!-- 현재위치에서 (joinForm.jsp) layout폴더의 header.jsp를 찾을려면 한칸 위로 올라가서 찾아야 한다. -->
<%@ include file="../layout/header.jsp"%>
<!--  div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간 -->
<div class="container">
	<form action="#" method="post">
	<!-- User Name -->
		<div class="form-group">
			<label for="username">User Name :</label>
			 <input type="text" name="username" placeholder="Enter username" class="form-control"  id="username">
		</div>
	<!-- Password  -->
		<div class="form-group">
			<label for="password">User Password :</label> 
			<input type="password"  name="password" class="form-control" placeholder="Enter password" id="password">
		</div>
		<div class="form-group form-check">
			<label class="form-check-label"> 
			<input name="remember" class="form-check-input" type="checkbox"> 기억하기
			</label>
		</div>
	<button id="btn-login" class="btn btn-primary">로그인</button>
	</form>
</div>
<!--  div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간 -->
<%@ include file="../layout/footer.jsp"%>

# login은 이제 js로 구현하지 않음 주석처리
<!-- <script src="/js/user.js"></script>  -->


┌─────────────────────────────────────────────────────────────────────────────────
│ 3번째 : UserApiController ( 경로 변경 ) : auth이하는 누구나 접근이 가능 하다.
└─────────────────────────────────────────────────────────────────────────────────
// 얘는 나중에 웹에도 쓸수 있다.
//데이터만 리턴을 해주는 컨트롤러 이기 때문에 @RestController
@RestController						
public class UserApiController {

    // DI ( Spring이 컴포넌트 스캔할 때 스프링 빈을 통해서 IoC에 띄어준다 )
    @Autowired
    private UserService userService;
    
    // JSON이니까 @RequestBody로 파라미터 받음
    // 통신상태를 확인하기 위해 HttpStatus.OK 
    @PostMapping("/auth/joinProc")
    public ResponseDto<Integer> save(@RequestBody User user) {		

        System.out.println("UserApiController : save 호출됨!");
        
        // username, password, email 입력값 form태그에서 받음
        // role은 back단에서 직접 넣어줘야함
        user.setRole(RoleType.USER);
        
        // 실제로 여기서 DB에 insert를 하고 아래에서 return이 되면 되요.
        userService.회원가입(user);
        
        // 회원가입이 성공 시 [ 상태값 : 200, 1 ] 호출
        // 자바 오브젝트를 JSON으로 변환해서 리턴 (Jackson)
        return new ResponseDto<Integer>(HttpStatus.OK.value(), 1);	
    }
}
┌─────────────────────────────────────────────────────────────────────────────────
│ 4번째 : user.js 파일 url 경로 변경  
└─────────────────────────────────────────────────────────────────────────────────
$.ajax({
    type: "post",									// POST방식으로 전송
    url: "/auth/joinProc",					// UserApiController의 save() 함수 호출					
    data: JSON.stringify(data),		// JSON 문자열로 data ( http body 데이터 ) 변경 (MIME 타입이 필요)
    contentType: "application/json; charset=utf-8",
    dataType: "json"							// 요청을 서버로해서 응답이 왔을 때 기본적으로 모든 것이 버퍼로 오기 때문에 응답값은 String 입니다.
                                                            // dataType : JSON => 생긴게 JSON이라면 => javascript로 오브젝트 변경 
}).done(function(resp){					    
    // 응답의 결과가 정상이면 done을 실행되는 영역
    console.log(resp);
    alert("회원가입이 완료 되었습니다.");
    location.href = "/";				// 정상적으로 회원가입 후 다시 /blog url로 이동 
    
}).fail(function(error){
    // 응답의 결과가 실패 하면 fail을 실행
    alert(JSON.spstringify(error));
});

┌─────────────────────────────────────────────────────────────────────────────────
│ 5번째 : SecurityConfig.java ( 스프링 시큐리티 설정파일 추가 )  
└─────────────────────────────────────────────────────────────────────────────────
package com.cos.blog.config;

import org.springframework.context.annotation.Configuration;
import org.springframework.security.config.annotation.method.configuration.EnableGlobalMethodSecurity;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter;

// 빈 등록 : 스프링 컨테이너에서 객체를 관리할 수 있게 하는 것
@Configuration																			 // 스프링 빈에 등록 ( IoC 관리 )
@EnableWebSecurity																	 // 시큐리티 필터가 등록이 된다. ( 설정 : SecurityConfig )
@EnableGlobalMethodSecurity (prePostEnabled = true) // 특정 주소로 접근을 하면 권한 및 인증을 미리 체크하겠다는 뜻
public class SecurityConfig extends WebSecurityConfigurerAdapter{
	
    @Override
    protected void configure(HttpSecurity http) throws Exception {
            
        // request가 들어오면 
        //.antMatchers("/auth/loginForm", "/auth/joinForm")
        http
            .authorizeRequests()
                .antMatchers("/auth/**")			// 경로가 /auth/이하는 누구나 들어올 수 있어요!.		
                .permitAll()						// 누구나 다 
                
                .anyRequest()					    // 경로가 /auth/가 아닌 모든 요청은
                .authenticated()  					// 인증이 되어있어야 해요
        
            .and()
                .formLogin()											
                .loginPage("/auth/loginForm");		// 처음 경로가 login 경로로 맞춘다.
    }
}