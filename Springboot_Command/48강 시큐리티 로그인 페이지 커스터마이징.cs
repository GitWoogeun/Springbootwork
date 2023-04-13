
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
1. application.yml
2. loginForm.jsp
3. header.jsp
4. user.js
5. userApiController.java
6. SecurityConfig.java
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

┌─────────────────────────────────────────────────────────────────────────────────
│ 6번째 : SecurityConfig.java (추가) 설명  
└─────────────────────────────────────────────────────────────────────────────────
"@Configuration"
# =>이 클래스 파일은 설정 파일이다라고 명시 해준다. ( Spring 컨테이너에 등록 )


"@EnableWebSecurity"
# => 보안 구성을 담당하는 클래스에서 @Configuration 어노테이션과 함께 사용 됩니다.
#    @EnableWebSecurity 어노테이션을 사용하면 Spring Security의 기본 구성을 사용하거나
#    사용자 정의 보안 구성을 지정할 수 있습니다.


"@EnableWebSecurity 기능 활성화"
# 1. Spring Security 필터 체인 등록
# 2. authenticationManagerBuilder를 사용하여 인증을 설정하는 메서드를 노출하는 구성 클래스 등록
# 3. EnableGlobalMethodSecurity 및 @PreAuthorize와 같은 다른 Spring Security 어노테이션을 사용하여
#    메소드 수준의 보안 구성 활성화
# 4. WebSecurityConfigurerAdapter로 확장하여 구현할 수 있습니다. 이를 통해 웹 보안 구성에 대한 다양한
#    메서드를 재정의 할 수 있습니다.
# 5. 즉 @EnableWebSecurity 어노테이션은 Spring Security를 사용하는 웹 애플리케이션에서 
#    보안을 활성화하기 위해 필요한 기본적인 설정을 자동으로 처리 해줍니다.


"WebSecurityConfigurerAdpter 상속 받으면 구현할 수 있는 기능들"

1. configure(HttpSecurity http) 
=> 메서드는 HTTP 요청에 대한 보안 구성을 설정하는 메서드입니다. 
   예를 들어, 로그인 페이지, 로그아웃, 인증, 권한 등을 설정할 수 있습니다.;

2. configure(WebSecurity web) 
=> 메서드는 특정 요청 경로에서 보안 필터 체인을 무시하도록 설정하는 메서드입니다.

3. configure(AuthenticationManagerBuilder auth) 
=> 메서드는 인증 관련 구성을 설정하는 메서드입니다. 예를 들어, 
   사용자 인증을 위한 UserDetailsService를 구현하거나, 비밀번호 암호화를 위한 PasswordEncoder를 설정할 수 있습니다.;

4. authenticationManagerBean() 
=> 메서드는 AuthenticationManager를 빈으로 등록하여 인증 관련 작업에서 사용할 수 있도록 합니다.

5. userDetailsServiceBean() 
=> 메서드는 UserDetailsService를 빈으로 등록하여 인증 작업에서 사용할 수 있도록 합니다.

6. passwordEncoder() 
=> 메서드는 PasswordEncoder를 빈으로 등록하여 인증 작업에서 사용할 수 있도록 합니다.

7. sessionManagement() 
=> 메서드는 세션 관리에 대한 구성을 설정하는 메서드입니다. 
   예를 들어, 세션 고정 보호, 세션 유효 기간 등을 설정할 수 있습니다.;

8. csrf() 
=> 메서드는 CSRF(Cross-Site Request Forgery) 보호에 대한 구성을 설정하는 메서드입니다. 
   예를 들어, 허용되는 요청 메서드, 예외 URL, 토큰의 이름 등을 설정할 수 있습니다.;

9. cors() 메서드는 Cross-Origin Resource Sharing에 대한 구성을 설정하는 메서드입니다. 
=> 예를 들어, 허용되는 도메인, 요청 헤더 등을 설정할 수 있습니다.;

10. headers() 메서드는 HTTP 응답 헤더에 대한 구성을 설정하는 메서드입니다. 
=> 예를 들어, 캐시 제어, XSS 보호 등을 설정할 수 있습니다.

┌─────────────────────────────────────────────────────────────────────────────────
│ 7번째 : SecurityConfig.java 파일을 적용한 자바 소스 구현  
└─────────────────────────────────────────────────────────────────────────────────;
"Security Config"
// UserDetailsService를 구현한 UserService를 빈으로 등록합니다.
// @Bean: 메서드가 생성한 객체를 스프링 컨테이너에 빈으로 등록합니다.
@Bean
public UserDetailsService userDetailsService() {
    return new UserService();
}

// PasswordEncoder를 빈으로 등록합니다.
@Bean
public PasswordEncoder passwordEncoder() {
    return new BCryptPasswordEncoder();
}

// 인증 관련 구성을 설정합니다.
@Override
protected void configure(AuthenticationManagerBuilder auth) throws Exception {
    auth.userDetailsService(userDetailsService()) // UserDetailsService를 사용하여 인증을 처리합니다.
        .passwordEncoder(passwordEncoder()); // 비밀번호를 암호화합니다.
}

// HTTP 보안 구성을 설정합니다.
@Override
protected void configure(HttpSecurity http) throws Exception {
    http
        // 로그인 페이지 설정
        .formLogin() // 폼 기반 로그인을 사용합니다.
            .loginPage("/login") // 로그인 페이지의 URL을 설정합니다.
            .permitAll() // 모든 사용자가 로그인 페이지에 접근할 수 있도록 허용합니다.
        .and()
        // 로그아웃 설정
        .logout() // 로그아웃을 설정합니다.
            .logoutSuccessUrl("/login") // 로그아웃 후 리다이렉트될 URL을 설정합니다.
            .permitAll() // 모든 사용자가 로그아웃을 할 수 있도록 허용합니다.
        .and()
        // 인증 설정
        .authorizeRequests() // 요청에 대한 인가를 설정합니다.
            .antMatchers("/admin/**").hasRole("ADMIN") // "/admin"으로 시작하는 URL은 ADMIN 권한이 필요합니다.
            .antMatchers("/user/**").hasAnyRole("USER", "ADMIN") // "/user"로 시작하는 URL은 USER 또는 ADMIN 권한이 필요합니다.
            .anyRequest().authenticated(); // 모든 요청에 대해 인증된 사용자만 접근할 수 있도록 설정합니다.
}


config(HttpSecurity http)
메서드에서는 로그인 페이지, 로그아웃, 인증 등을 설정합니다.
/admin/**/ 경로는 ADMIN 권한을 가진 사용자만 접근할 수 있고,
/user/**/  경로는 USER 또는 ADMIN권한을 가진 사용자만 접근할 수 있습니다.
이외의 요청에 대해서는 인증된 사용자만 접근할 수 있도록 설정되었습니다.;

"UserController"
@Controller
@RequestMapping("/user")
public class UserController {

    @Autowired
    private UserService userService;

    // /user/** 경로는 USER 또는 ADMIN 권한을 가진 사용자만 접근할 수 있습니다.
    @GetMapping("/list")
    // PreAuthorize 애너테이션을 이용하여 권한을 설정합니다.
    // hasAnyRole() 메서드를 이용하여 USER 또는 ADMIN 권한을 가진 사용자만 접근할 수 있습니다.
    @PreAuthorize("hasAnyRole('USER', 'ADMIN')")
    public String userList(Model model) {
        // UserService를 이용하여 사용자 목록을 조회합니다.
        List<User> userList = userService.getUserList();
        // 조회된 사용자 목록을 모델에 추가합니다.
        model.addAttribute("userList", userList);
        // 사용자 목록 페이지를 반환합니다.
        return "user/list";
    }

    // /user/create 경로는 ADMIN 권한을 가진 사용자만 접근할 수 있습니다.
    @GetMapping("/create")
    // PreAuthorize 애너테이션을 이용하여 권한을 설정합니다.
    // hasRole() 메서드를 이용하여 ADMIN 권한을 가진 사용자만 접근할 수 있습니다.
    @PreAuthorize("hasRole('ADMIN')")
    public String createUserForm(Model model) {
        // 새로운 사용자를 생성하기 위한 폼을 모델에 추가합니다.
        model.addAttribute("user", new User());
        // 사용자 생성 폼 페이지를 반환합니다.
        return "user/create";
    }

    @PostMapping("/create")
    public String createUser(@ModelAttribute("user") User user, BindingResult result) {
        // BindingResult를 이용하여 폼 데이터 유효성 검사를 수행합니다.
        if (result.hasErrors()) {
            // 유효성 검사에 실패한 경우, 사용자 생성 폼 페이지로 다시 이동합니다.
            return "user/create";
        }
        // UserService를 이용하여 새로운 사용자를 생성합니다.
        userService.createUser(user);
        // 사용자 목록 페이지로 이동합니다.
        return "redirect:/user/list";
    }
}

@Service
public class UserService implements UserDetailsService {

    @Autowired
    private UserRepository userRepository;

    // UserDetailsService 인터페이스를 구현한 loadUserByUsername() 메서드를 구현합니다.
    @Override
    public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
        // UserRepository를 이용하여 사용자 정보를 조회합니다.
        User user = userRepository.findByUsername(username);
        // 사용자 정보가 존재하지 않는 경우, 예외를 던집니다.
        if (user == null) {
            throw new UsernameNotFoundException("Invalid username or password.");
        }
        // 사용자 정보를 UserDetails 객체로 변환합니다.
        return new org.springframework.security.core.userdetails.User(user.getUsername(), user.getPassword(),
                getAuthority(user));
    }

    // 사용자 권한 정보를 반환하는 getAuthority() 메서드를 구현합니다.
    private Set<SimpleGrantedAuthority> getAuthority(User user) {
        Set<SimpleGrantedAuthority> authorities = new HashSet<>();
        // 사용자가 가진 권한 정보를 SimpleGrantedAuthority 객체로 생성하여 Set 컬렉션에 추가합니다.
        user.getRoles().forEach(role -> authorities.add(new SimpleGrantedAuthority("ROLE_" + role.getName())));
        // Set 컬렉션을 반환합니다.
        return authorities;
    }

    // 모든 사용자 정보를 조회하는 getUserList() 메서드를 구현합니다.
    public List<User> getUserList() {
        return userRepository.findAll();
    }

    // 새로운 사용자를 생성하는 createUser() 메서드를 구현합니다.
    public void createUser(User user) {
        // 사용자 비밀번호를 암호화하여 저장합니다.
        user.setPassword(new BCryptPasswordEncoder().encode(user.getPassword()));
        // 사용자를 저장합니다.
        userRepository.save(user);
    }
}

