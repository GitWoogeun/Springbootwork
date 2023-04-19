┌─────────────────────────────────────────────────────────────────────────────────
│ XSS와 CSRF 
└─────────────────────────────────────────────────────────────────────────────────

XSS -> 자바스크립트 공격
<script>
    for(var i = 0; i < 50000; i++)
    {
        alert("공격");
        // alert가 버튼을 클릭했을 때 5만번 호출
    }
</script> 


CSRF : 

// 내 아이디에 5만 포인트 충전 
http://naver.com/admin/point?id=ssar&point=50000


// 막는 방법 ( role 권한이 ADMIN만 가능하도록 )
// ADMIN만 가능하게
시큐리티 : /admin/**/ 

하이퍼링크 주소 :
<a href="저 위에 주소로 해놓으면 실행이 된다."><Img src="도라이몽사진"></a>

막는 방법 
첫 번째 : GET방식이 아닌 POST 방식으로

시큐리티가 CSRF 토큰이 없으면 권한이 없는 유저라고 생각하고 막아버린다.

// request(요청)가 들어오면 
//.antMatchers("/auth/loginForm", "/auth/joinForm")
http
   .csrf().disable()    												// csrf 토큰 비활성화 ( 테스트 시 걸어두는 게 좋음 )
    .authorizeRequests()												// request가 들어오면 
    .antMatchers("/", "/auth/**", "/js/**", "/css/**", "/image/**")		// 경로가 /auth/이하는 누구나 들어올 수 있어요!.		
    .permitAll()														// 누구나 다 

   .anyRequest()														// 경로가 /auth/가 아닌 모든 요청은
    .authenticated()  													// 인증이 되어있어야 해요

   .and()
    .formLogin()											
    .loginPage("/auth/loginForm");										// 처음 경로가 login 경로로 맞춘다.