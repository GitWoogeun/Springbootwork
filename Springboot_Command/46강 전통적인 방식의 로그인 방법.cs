
# 스프링부트의 전통적인 방식의 로그인 방법
JSTL tutorial 사이트에서 <태그 라이브러리 추가>
<%@ taglib prefix = "c" uri = "http://java.sun.com/jsp/jstl/core" %>

┌─────────────────────────────────────────
│ JSTL을 사용하기 위한 dependency 추가
└─────────────────────────────────────────
<!-- JSTL --> 
<dependency>
    <groupId>javax.servlet</groupId>
    <artifactId>jstl</artifactId>
    <version>1.2</version>
</dependency>
		

┌─────────────────────────────────────────────────────────────
│ 로그인 화면 전후 보여주기 위한 c 태그 사용 header.jsp
└─────────────────────────────────────────────────────────────
# header.jsp에서 
# login 전의 화면과 
# login 후의 화면을 보여주기위 해
<c:choose>
    <c:when test="${empty sessionScope.principal}">
      <ul class="navbar-nav">
        <li class="nav-item"><a class="nav-link" href="/blog/user/loginForm">로그인</a></li>
        <li class="nav-item"><a class="nav-link" href="/blog/user/joinForm">회원가입</a></li>
      </ul>  
    </c:when>
    <c:otherwhise>
      <ul class="navbar-nav">
        <li class="nav-item"><a class="nav-link" href="/blog/board/writeForm">글쓰기</a></li>
        <li class="nav-item"><a class="nav-link" href="/blog/user/userForm">회원정보</a></li>
        <li class="nav-item"><a class="nav-link" href="/blog/user/logout">로그아웃</a></li>
      </ul>
    </c:otherwhise>
</c:choose>

┌───────────────────────────────────────────
│ 로그인 화면 웹 페이지를 전달 UserController 
└───────────────────────────────────────────
@Controller
public class UserController {
	
	@GetMapping("/user/joinForm")
	public String joinForm() {
		return "user/joinForm";
	}
	
	@GetMapping("/user/loginForm")
	public String loginForm() {
		return "user/loginForm";
	}
}

┌───────────────────────────────────────────────────
│ 로그인을 하기 위한 실제 데이터 전송 userApiController
└───────────────────────────────────────────────────
@RestController						
public class UserApiController {

    // DI ( Spring이 컴포넌트 스캔할 때 스프링 빈을 통해서 IoC에 띄어준다 )
    @Autowired
    private UserService userService;

    // 스프링 DI로 의존성에의 해서 호출해서 사용할 수 있다.
    @Autowired
    private HttpSession session;

    // 전통적인 로그인 방식
    // 다음시간에 스프링 시큐리티 이용해서 로그인!!
    @PostMapping("/api/user/login")
    public ResponseDto<Integer> login(@RequestBody User user) {
        System.out.println("UserApiController : login 호출됨");

        // 실제로 여기서 로그인 정보를 담아 principal 변수에 저장
        // Printcipal = 정보주체의
        User principal = userService.로그인(user);		
        
        // 유저 로그인 정보가 null이 아니라면
        if( principal != null) {

            // session 설정 session key = "principal", session value = principal
            session.setAttribute("principal", principal);
        }
        
        // 통신의 상태값을 확인 리턴
        return new ResponseDto<Integer>(HttpStatus.OK.value(), 1);			
    }
}

┌───────────────────────────────────────────────────
│ 로그인 요청을 Repository에게 전송 userService();
└───────────────────────────────────────────────────
// 스프링이 컴포넌트 스캔을 통해서 빈에 등록을 해준다.  ( IoC를 해준다 ) 메모리를 대신 띄어준다.
@Service
public class UserService {
	
    @Autowired
    private UserRepository userRepository;

    // SELECT할 때 트랜잭션이 실행이 된다.
    // 해당 서비스가 종료될 때 트랜잭션가 종료될텐데 이때까지는 데이터의 정합성을 유지할 수 있음
    @Transactional(readOnly = true)
    public User 로그인(User user) {
        return user = userRepository.findByUsernameAndPassword(user.getUsername(), user.getPassword());
    }
}

┌──────────────────────────────────────────────────────────────────────────────────────────
│ 로그인을 하기 위해 영속성 컨텍스트에 접근하는 repository 해당 정보가 없다면 데이터베이스에 접근 
└──────────────────────────────────────────────────────────────────────────────────────────
public interface UserRepository extends JpaRepository<User, Integer>{
    
    // JPA Naming 쿼리 전략
    // 실제로 JPA가 들고 있지 않지만 이런 쿼리가 자동으로 실행이 된다.
    // SELECT * FROM USER WHERE username = ?1 AND password = ?2
    // SELECT * FROM USER WHERE username = username AND password = password
    User findByUsernameAndPassword(String username, String password);

}

┌──────────────────────────────────────────────────────────────────────────────────────────
│ user.js ajax를 통해 DB에 저장된 데이터를 JSON 데이터로 형변환 후 호출 
└──────────────────────────────────────────────────────────────────────────────────────────
// 제이쿼리 사용
let index = {
    init: function() {
        $("#btn-login").on("click", ()=>{
                this.login();		// <= 회원가입 버튼을 클릭 했을 시 save: function()을 호출
        });
    },
    // 로그인 기능 구현
    login: function(){
        alert("로그인이 되었습니다.");
        let data = {
            username: $("#username").val(),		// form태그에 있는 태그의 id값을 찾아서 username 변수에 값을 바인딩 한다.
            password: $("#password").val(),		// form태그에 있는 태그의 id값을 찾아서 password  변수에 값을 바인딩 한다.
        };
        
        $.ajax({
            type: "post",						// POST방식으로 전송
            url: "/blog/api/user/login",		// UserApiController의 save() 함수 호출					
            data: JSON.stringify(data),		    // JSON 문자열로 data ( http body 데이터 ) 변경 (MIME 타입이 필요)
            contentType: "application/json; charset=utf-8",
            dataType: "json"					// 요청을 서버로해서 응답이 왔을 때 기본적으로 모든 것이 버퍼로 오기 때문에 응답값은 String 입니다.
                                                // dataType : JSON => 생긴게 JSON이라면 => javascript로 오브젝트 변경 
        }).done(function(resp){					    
            // 응답의 결과가 정상이면 done을 실행되는 영역
            console.log(resp);
            alert("로그인이 완료 되었습니다.");
            location.href = "/blog";			// 로그인이 정상적으로 성공하면 main페이지로 이동
        }).fail(function(error){
            // 응답의 결과가 실패 하면 fail을 실행
            alert(JSON.spstringify(error));
        });
    }
}

index.init();

┌───────────────────────────────────────────────────
│ HTTP 통신상태의 상태값을 생성 하는 ResponseDto
└───────────────────────────────────────────────────
@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class ResponseDto<T> {
    // 응답할때 사용
    int status;
    T data;
}
