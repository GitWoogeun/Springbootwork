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
						.antMatchers("/auth/**")					// 경로가 /auth/이하는 누구나 들어올 수 있어요!.		
						.permitAll()											// 누구나 다 
						
						.anyRequest()										// 경로가 /auth/가 아닌 모든 요청은
						.authenticated()  								// 인증이 되어있어야 해요
				
					.and()
						.formLogin()											
						.loginPage("/auth/loginForm");		// 처음 경로가 login 경로로 맞춘다.
		}
}
