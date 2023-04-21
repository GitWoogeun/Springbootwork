#┌─────────────────────────────────────────────────────────────────────────────────
#│ 회원수정_2 ( Spring Security의 Session 값 변경 )
#└─────────────────────────────────────────────────────────────────────────────────


#┌─────────────────────────────────────────────────────────────────────────────────
#│ AuthenticationPrincipal에 대해서 알아보자.
#└─────────────────────────────────────────────────────────────────────────────────

AuthenticationPrincipal은 Spring Security의 인증된 사용자의 정보를 저장하고 추출하는 객체

"AuthenticationPrincipal의 생성 조직도"

                            ( Token 생성후 UserDetailsService에게 던짐 )
                                UsernamePasswordAuthenticationToken

                            ↗

Http Request  =>   AuthenticationFilter  =>  AuthenticationManager  =>  AuthenticationPrincipal(s)

                            ↓                         ↑(Implements)          ↑          ↓
                                                                         
                ┌──SecurityContentHolder─┐       ProviderManager          UserDetailsService
                │                        │                    (    username을 가지고 DB에 USER가 있는지 찾아봄      )
                │      SecurityContent   │                    ( 없으면 Authentication 객체를 만들고 있으면 안만든다. )            
                │                        │                                    
                │       Authentication   │                                     UserDetails( token이 들어온다 )
                │                        │                                     <Interface>
                └────────────────────────┘                                          ↑
                                                                                  
                                                                                  USER
 

#┌─────────────────────────────────────────────────────────────────────────────────
#│ Authentication 객체를 만들어서 세션에 저장하기 위한 흐름
#└─────────────────────────────────────────────────────────────────────────────────
                                 ( Authentication 객체를 만들기 위해서 )
                  (요청받음)               던진 토큰을 받음 
로그인 요청 -> AuthenticationFilter     AuthenticationManager 
( rnb )               ↓              ↗                      ↘     
(1234 )       유저패스인증토큰 생성              ↓            UserDetailsService
             (username과 password)                    ( 영속성컨텍스트와 DB에게 해당 유저 확인 요청 ) 

                                               ↓       ↖             ↓
                                                     
                                                                 영속성컨텍스트
                                               ↓     ( 체크확인 : username과 암호화된 패스워드 있니? )        
                                                     ( 있으면 AuthenticationManager에게 전달 없으면 DB로 이동)

            Session                                                  ↓
( 세션에 Authentication객체 저장 )              ↓                    
                                                                데이터베이스
                                                     ( 체크확인 : username과 암호화된 패스워드 찾아서 ) 
               ↑(전달)                               ( AuthenticationManager에게 전달 ) 

                                               ↓                        
        시큐리티 컨텍스트     <=      Authentication 객체 만듬               
    (Authentication객체 생김)  (username, encPassword[암호화된 패스워드])

#┌─────────────────────────────────────────────────────────────────────────────────
#│ Authentication 객체를 만들어서 세션에 저장하기 위한 흐름 코드 진행
#└─────────────────────────────────────────────────────────────────────────────────
// 얘는 나중에 웹에도 쓸수 있다.
//데이터만 리턴을 해주는 컨트롤러 이기 때문에 @RestController
@RestController						
public class UserApiController {

    // DI ( Spring이 컴포넌트 스캔할 때 스프링 빈을 통해서 IoC에 띄어준다 )
    @Autowired
    private UserService userService;

    @Autowired
    private AuthenticationManager authenticationManager;

    
    // JSON이니까 @RequestBody로 파라미터 받음
    // 통신상태를 확인하기 위해 HttpStatus.OK 
    // username, password, email
    @PostMapping("/auth/joinProc")
    public ResponseDto<Integer> save(@RequestBody User user) {

        System.out.println("UserApiController : save 호출됨!"); 
        
        // 실제로 여기서 DB에 insert를 하고 아래에서 return이 되면 되요.
        userService.회원가입(user);
        
        // 회원가입이 성공 시 [ 상태값 : 200, 1 ] 호출
        // 자바 오브젝트를 JSON으로 변환해서 리턴 (Jackson)
        return new ResponseDto<Integer>(HttpStatus.OK.value(), 1);
    }

    // 유저의 정상적인 통신이 된다면 상태값 200(성공)과 1을 리턴
    // key = value, x-www-form-urlencoded
    // @RequestBody : HTTP 요청 본문에 있는 데이터를 자동으로 자바의 객체로 변환 하는데 사용
    @PutMapping("/user")
    public ResponseDto<Integer> update(@RequestBody User user)
    {		
        userService.회원수정(user);
        
        // 트랜잭션이 종료되기 때문에 DB의 값은 변경이 되었지만,
        // 하지만 Session 값은 변경되지 않은 상태이기 때문에 우리가 직접 Session값을 변경 해줄 것임.
        // Session 등록
        Authentication authentication = authenticationManager
                .authenticate(new UsernamePasswordAuthenticationToken(user.getUsername(), user.getPassword()));
        SecurityContextHolder.getContext().setAuthentication(authentication);
        
        return new ResponseDto<Integer>(HttpStatus.OK.value(), 1);
    }
}

update: function(){
    let data = {
        id: $("#id").val(),				  // 영속성컨텍스트의 회원정보를 찾기위한 id 셋팅값
        username: $("#username").val(),   // form태그에 있는 태그의 id값을 찾아서 username 변수에 값을 바인딩 한다.
        password: $("#password").val(),	  // form태그에 있는 태그의 id값을 찾아서 password  변수에 값을 바인딩 한다.
        email: $("#email").val()		  // form태그에 있는 태그의 id값을 찾아서 email          변수에 값을 바인딩 한다.
    };
    
    $.ajax({
    type: "put",							// PUT 방식의 전송
        url: "/user",						// UserApiController의 update() 함수 호출					
        data: JSON.stringify(data),			// JSON 문자열로 data ( http body 데이터 ) 변경 (MIME 타입이 필요)
        contentType: "application/json; charset=utf-8",
        dataType: "json"					// 요청을 서버로해서 응답이 왔을 때 기본적으로 모든 것이 버퍼로 오기 때문에 응답값은 String 입니다.
                                            // dataType : JSON => 생긴게 JSON이라면 => javascript로 오브젝트 변경 
    }).done(function(resp){					    
        // 응답의 결과가 정상이면 done을 실행되는 영역
        console.log(resp);
        alert("회원정보가 수정이 완료 되었습나다,");
        location.href = "/";				// 정상적으로 회원가입 후 다시 /blog url로 이동 
        
    }).fail(function(error){
        // 응답의 결과가 실패 하면 fail을 실행
        alert(JSON.spstringify(error));
        });
    },
}

index.init();