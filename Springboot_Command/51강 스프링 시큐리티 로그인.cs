┌─────────────────────────────────────────────────────────────────────────────────
│ 스프링 시큐리티 로그인
└─────────────────────────────────────────────────────────────────────────────────
package com.cos.blog.config;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.config.annotation.method.configuration.EnableGlobalMethodSecurity;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;

// 빈 등록 : 스프링 컨테이너에서 객체를 관리할 수 있게 하는 것
@Configuration																			 // 스프링 빈에 등록 ( IoC 관리 )
@EnableWebSecurity																	 // 시큐리티 필터가 등록이 된다. ( 설정 : SecurityConfig )
@EnableGlobalMethodSecurity (prePostEnabled = true) // 특정 주소로 접근을 하면 권한 및 인증을 미리 체크하겠다는 뜻
public class SecurityConfig extends WebSecurityConfigurerAdapter{
	
    @Bean	// IoC가 된다.
    public BCryptPasswordEncoder encodePWD(){
        return new BCryptPasswordEncoder();
    }

    @Override
    protected void configure(HttpSecurity http) throws Exception {
            
        // request(요청)가 들어오면 
        //.antMatchers("/auth/loginForm", "/auth/joinForm")
        http
            // csrf 토큰 비활성화 ( 테스트 시 걸어두는 게 좋음 )
            .csrf().disable()
            // request가 들어오면 
            .authorizeRequests()
                // 경로가 /auth/이하는 누구나 들어올 수 있어요!.																					 
                .antMatchers("/", "/auth/**", "/js/**", "/css/**", "/image/**")			
                // 누구나 다
                .permitAll()																								 
                
                // 경로가 /auth/가 아닌 모든 요청은
                .anyRequest()																							
                // 인증이 되어있어야 해요
                .authenticated()  																					
        
            .and()
                .formLogin()											
                // 처음 경로가 login 경로로 맞춘다.
                .loginPage("/auth/loginForm")
                // 스프링 시큐리티가 해당 주소로 로그인을 가로채서 대신 로그인 해준다															
                .loginProcessingUrl("/auth/loginProc")											
                    // 로그인 성공 시 이동하는 기본 URL
                    .defaultSuccessUrl("/");																			
    }
}

┌─────────────────────────────────────────────────────────────────────────────────
│ PrincipalDetail 클래스
└─────────────────────────────────────────────────────────────────────────────────
// 스프링 시큐리티가 로그인 요청을 가로채서 로그인을 진행하고 완료가 되면 UserDetails 타입의 오브젝트를
// 스프링 시큐리티의 고유한 세션 저장소에 저장을 해준다.
// PrincipalDetail이 저장된다. ( User 가 포함되어 있어야 한다. )
public class PrincipalDetail implements UserDetails {
	
    private User user;				// 콤포지션 ( 클래스에 클래스를 품고 있는 것 )

    public PrincipalDetail(User user) {
            this.user = user;
    };
            
    @Override
    public String getPassword() {
        return user.getPassword();
    }

    @Override
    public String getUsername() {
        return user.getUsername();
    }

    // 계정이 만료되지 않았는지 리턴한다 ( true : 만료 안됨 )
    @Override
    public boolean isAccountNonExpired() {
        return true;
    }

    // 계정이 잠겨있는지 안잠겨있는지 확인 ( true : 안잠겨 있음 )
    @Override
    public boolean isAccountNonLocked() {
        return true;
    }

    // 비밀번호가 만료되지 안았는지 리턴한다 ( true : 만료 안됨 )
    @Override
    public boolean isCredentialsNonExpired() {
        return true;
    }

    // 계정 활성화가 되어 있는지 안되어있는지 리턴 ( true = 활성회 )
    @Override
    public boolean isEnabled() {
        return true;
    }
    
    // 계정의 권한을 리턴한다.
    // GrantedAuthority를 상속한 Collection이여 한다.
    // 계정이 갖고 있는 권한 목록을 리턴한다. ( 권한이 여러개 있을 수 있어서 루프를 돌아야 하는데 우리는 한개만 )
    @Override
    public Collection<? extends GrantedAuthority> getAuthorities() {
        
        // 왜 ArrayList인가?
        // List Collection을 상속을 받는게 있어서
        Collection<GrantedAuthority> collectors = new ArrayList<>();
        
        // 람다식
        collectors.add(()-> { return "ROLL_" + user.getRole(); });
        return collectors;
    }
}

// 시큐리티가 대신 로그인을 해주는데 password가 가로채기를 하는데
// 해당 password가 뭘로 해쉬가 되어서 회원가입이 되었는지 알아야
// 같은 해쉬로 암호화해서 DB에 있는 해쉬랑 비교할 수 있다.
@Override
protected void configure(AuthenticationManagerBuilder auth) throws Exception {
        // principalDetailService를 통해서 우리가 로그인을할 때 패스워드 처리를 encodePWD()를 비교를 해준다.
        auth.userDetailsService(principalDetailService).passwordEncoder(encodePWD());
}

#┌─────────────────────────────────────────────────────────────────────────────────
#│ PrincipalDetailService.class ( 로그인 요청 시 스프링 시큐리티가 대신 처리 )
#└─────────────────────────────────────────────────────────────────────────────────
// 로그인 요청이 들어왔을 때 스프링 시큐리티가 가로채서 로그인 기능 구현
@Service
public class PrincipalDetailService implements UserDetailsService{

	@Autowired
	private UserRepository userRepository;
	
	
	// 스프링이 로그인 요청을 가로챌때 username, password 변수 2개를 가로채는데
	// password 부분처리는 알아서 함.
	// username이 DB에 있는지만 확인해서 리턴해주면 됨.
	// loadUserByUsername을 통해서 로그인을 할 것
	@Override
	public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
		User principal = userRepository.findByUsername(username)
				.orElseThrow(()-> {
					return new UsernameNotFoundException("해당 사용자를 찾을 수 없습니다. : " + username);
				});
		return new PrincipalDetail(principal);   // 시큐리티 세션에 유저 정보가 저장이 된다. 
	}
}