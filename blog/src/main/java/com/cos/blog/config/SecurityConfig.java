package com.cos.blog.config;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.config.annotation.authentication.builders.AuthenticationManagerBuilder;
import org.springframework.security.config.annotation.method.configuration.EnableGlobalMethodSecurity;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;

import com.cos.blog.config.auth.PrincipalDetailService;

// 빈 등록 : 스프링 컨테이너에서 객체를 관리할 수 있게 하는 것
@Configuration																			 // 스프링 빈에 등록 ( IoC 관리 )
@EnableWebSecurity																	 // 시큐리티 필터가 등록이 된다. ( 설정 : SecurityConfig )
@EnableGlobalMethodSecurity (prePostEnabled = true) // 특정 주소로 접근을 하면 권한 및 인증을 미리 체크하겠다는 뜻
public class SecurityConfig extends WebSecurityConfigurerAdapter{
	
		// DI해준다.
		@Autowired
		private PrincipalDetailService principalDetailService;
	
		@Bean	// IoC가 된다.
		public BCryptPasswordEncoder encodePWD(){
			return new BCryptPasswordEncoder();
		}
		
		// 시큐리티가 대신 로그인을 해주는데 password가 가로채기를 하는데
		// 해당 password가 뭘로 해쉬가 되어서 회원가입이 되었는지 알아야
		// 같은 해쉬로 암호화해서 DB에 있는 해쉬랑 비교할 수 있다.
		@Override
		protected void configure(AuthenticationManagerBuilder auth) throws Exception {
				// principalDetailService를 통해서 우리가 로그인을할 때 패스워드 처리를 encodePWD()를 비교를 해준다.
				auth.userDetailsService(principalDetailService).passwordEncoder(encodePWD());
		}
		
		@Override
		protected void configure(HttpSecurity http) throws Exception {
				
				// request(요청)가 들어오면 
			    //.antMatchers("/auth/loginForm", "/auth/joinForm", "/dummy/**")
				http
					.csrf().disable()    																												// csrf 토큰 비활성화 ( 테스트 시 걸어두는 게 좋음 )
					.authorizeRequests()																											// request가 들어오면 
						.antMatchers("/", "/auth/**", "/js/**", "/css/**", "/image/**", "/dummy/**")	// 경로가 /auth/이하는 누구나 들어올 수 있어요!.		
						.permitAll()																														// 누구나 다 
						
						.anyRequest()																													// 경로가 /auth/가 아닌 모든 요청은
						.authenticated()  																											// 인증이 되어있어야 해요
				
					.and()
						.formLogin()											
						.loginPage("/auth/loginForm")																					// 처음 경로가 login 경로로 맞춘다.
						.loginProcessingUrl("/auth/loginProc")																	// 스프링 시큐리티가 해당 주소로 로그인을 가로채서 대신 로그인 해준다
						.defaultSuccessUrl("/");																									// 정상적으로 로그인 성공 시 이동하는 기본 URL
		}
}